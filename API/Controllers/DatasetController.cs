using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Data;
using API.DTOs;
using API.Models;
using Extreme.Mathematics;
using Extreme.Statistics.TimeSeriesAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers;

[AllowAnonymous]
public class DatasetController : BaseApiController
{
    private readonly DataContext _context;

    public DatasetController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("sales-for-period/{establishmentId}")]
    public async Task<ActionResult<IEnumerable<DailySales>>> GetSalesForPeriod(int establishmentId, [FromQuery] Period period)
    {
        var estimates = _context
                            .Estimates
                            .Where(e =>
                                e.EstablishmentId == establishmentId &&
                                e.CreatedAt >= period.Start &&
                                e.CreatedAt <= period.End)
                            .ToList();

        var result = estimates
                        .GroupBy(e => e.CreatedAt.Date)
                        .Select(e =>
                            new DailySales
                            {
                                Date = e.Key,
                                Sales = e.Sum(x => x.TotalSum)
                            });

        return HandleResult(Result<IEnumerable<DailySales>>.Success(result));
    }

    [HttpGet("predict-for-days/{establishmentId}")]
    public async Task<ActionResult<IEnumerable<DailySales>>> PredictForDays(int establishmentId, [FromQuery] int days)
    {
        Extreme.License.Verify("38403-06913-12378-30790");

        var estimates = _context
                            .Estimates
                            .Where(e => e.EstablishmentId == establishmentId)
                            .ToList();

        var result = estimates
                        .GroupBy(e => e.CreatedAt.Date)
                        .Select(e =>
                            new DailySales
                            {
                                Date = e.Key,
                                Sales = e.Sum(x => x.TotalSum)
                            });

        var dataForPrediction = Vector.Create(result.Select(r => r.Sales).ToArray());

        ArimaModel model = new ArimaModel(dataForPrediction, 2, 1);

        model.Fit();

        var nextValues = model.Forecast(days);
        var forecastingResult = nextValues
                                    .Select((sales, index) =>
                                        new DailySales
                                        {
                                            Date = DateTime.Now.Date.AddDays(index + 1),
                                            Sales = sales
                                        });

        return HandleResult(Result<IEnumerable<DailySales>>.Success(forecastingResult));
    }

    [HttpGet("parsed-data")]
    public IActionResult ParsedData([FromQuery] string query)
    {

        string chromeDriverPath = @"C:\\\Program Files\\\Google\\\Chrome\\\Application\\chromedriver.exe";
        var options = new ChromeOptions();
        options.AddArgument("--headless"); // Запуск в безголовом режиме
        var driver = new ChromeDriver(chromeDriverPath, options);
        int sleepTime = 2000;
        int pageNumber = 1;
        var products = new List<WildberriesProduct>();
        while (pageNumber < 10)
        {
            Console.WriteLine("QUery " + query);
            driver.Navigate().GoToUrl($"https://www.wildberries.ru/catalog/0/search.aspx?search={query}&page={pageNumber}");

            System.Threading.Thread.Sleep(sleepTime);

            var productElements = driver.FindElements(By.CssSelector(".product-card"));
            Console.WriteLine("Element amount: " + productElements.Count + ", Page: " + pageNumber);
            if (productElements.Count == 0)
            {
                if (sleepTime == 10000)
                    break;
                sleepTime += 1000;
                continue;
            }
            else
            {
                sleepTime = 2000;
            }

            foreach (var element in productElements)
            {
                try
                {
                    var nameElement = element.FindElement(By.CssSelector(".product-card__name"));
                    var priceElement = element.FindElement(By.CssSelector(".price__lower-price"));
                    var urlElement = element.FindElement(By.CssSelector(".product-card__link"));
                    var ratingElement = element.FindElement(By.CssSelector(".address-rate-mini.address-rate-mini--sm"));
                    var gradeAmountElement = element.FindElement(By.CssSelector(".product-card__count"));

                    if (!nameElement.Text.Replace(" ", "").ToLower().Contains(query))
                        continue;
                    var product = new WildberriesProduct
                    {
                        Name = nameElement.Text,
                        Price = int.Parse(new string(priceElement.Text.Where(char.IsDigit).ToArray())) * 5,
                        Url = urlElement.GetAttribute("href"),
                        Rating = double.Parse(ratingElement.Text.Replace(".", ",")),
                        GradeAmount = int.Parse(new string(gradeAmountElement.Text.Where(char.IsDigit).ToArray()))
                    };

                    products.Add(product);
                    Console.WriteLine(product.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    continue;
                }
            }
            pageNumber++;
        }
        products = products.OrderByDescending(p => p.GradeAmount).ToList();
        return HandleResult(Result<IEnumerable<WildberriesProduct>>.Success(products));
    }
}

public class WildberriesProduct
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Url { get; set; }
    public double Rating { get; set; }
    public int? GradeAmount { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Price} - {Rating} - {GradeAmount}";
    }
}