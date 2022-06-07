using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!context.Manufacturers.Any() && !context.Products.Any() &&
                !context.Services.Any() &&
                !context.Cities.Any() && !context.Establishments.Any() &&
                !context.EstablishmentProduct.Any())
            {
                var manufacturers = new List<Manufacturer>
                {
                    new Manufacturer
                    {
                        Name = "Coca-Cola Company",
                        Description = "Coca-Cola"
                    },
                    new Manufacturer
                    {
                        Name = "Caspian Beverage Holding JSC",
                        Description = "Caspian Beverage Holding"
                    },
                    new Manufacturer
                    {
                        Name = "Shin Line LLP",
                        Description = "Shin Line"
                    },
                    new Manufacturer
                    {
                        Name = "Rakhat JSC",
                        Description = "Rakhat"
                    },
                    new Manufacturer
                    {
                        Name = "Apple Inc",
                        Description = "Apple"
                    },
                    new Manufacturer
                    {
                        Name = "Samsung Co",
                        Description = "Samsung"
                    },
                    new Manufacturer
                    {
                        Name = "Dosfarm LLP",
                        Description = "Dosfarm"
                    }
                };

                await context.Manufacturers.AddRangeAsync(manufacturers);
            
                var metrics = new List<Metric>
                {
                    new Metric 
                    {
                        Name = "тг/шт",
                        KzName = "тг/дана",
                        RuName = "тг/шт"
                    },
                    new Metric
                    {
                        Name = "тг/кг",
                        KzName = "тг/кг",
                        RuName = "тг/кг"
                    }
                };
                
                await context.Metrics.AddRangeAsync(metrics);

                var products = new List<Product>
                {
                    new Product 
                    {
                        BarCode = "5449000054227",
                        Name = "Coca-Cola Classic 1L",
                        IsCustom = false,
                        Description = "Coca-Cola Classic 1L",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[0]
                    },
                    new Product 
                    {
                        BarCode = "5449000009067",
                        Name = "Coca-Cola Classic 2л",
                        IsCustom = false,
                        Description = "Coca-Cola Classic 2Л",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[0]
                    },
                    new Product 
                    {
                        BarCode = "5449000004840",
                        Name = "Fanta 2л",
                        IsCustom = false,
                        Description = "Fanta 2Л",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[0]
                    },
                    new Product 
                    {
                        BarCode = "4870085000884",
                        Name = "Настоящий Буратино 0,5Л",
                        IsCustom = false,
                        Description = "Настоящий Буратино 0,5Л",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[1]
                    },
                    new Product 
                    {
                        BarCode = "4870085000907",
                        Name = "Настоящий Буратино 1,5Л",
                        IsCustom = false,
                        Description = "Настоящий Буратино 1,5Л",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[1]
                    },
                    new Product 
                    {
                        BarCode = "4870004903876",
                        Name = "MELONA MANGO",
                        IsCustom = false,
                        Description = "MELONA MANGO",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[2]
                    },
                    new Product 
                    {
                        BarCode = "6920238082019",
                        Name = "ЛАПША ШИН РА МЕН 120Г",
                        IsCustom = false,
                        Description = "ЛАПША ШИН РА МЕН 120Г",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[2]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "АЙСҰЛУ ШОК",
                        IsCustom = false,
                        Description = "АЙСҰЛУ ШОК",
                        Metric = metrics[1],
                        Manufacturer = manufacturers[3]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "КӨКТЕМ",
                        IsCustom = false,
                        Description = "КӨКТЕМ",
                        Metric = metrics[1],
                        Manufacturer = manufacturers[3]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "СУФЛЕ СО ВКУСОМ ВИШНИ",
                        IsCustom = false,
                        Description = "СУФЛЕ СО ВКУСОМ ВИШНИ",
                        Metric = metrics[1],
                        Manufacturer = manufacturers[3]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "iPhone 13 128GB Black",
                        IsCustom = false,
                        Description = "iPhone 13 128GB Black",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[4]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "Apple MacBook Air Retina M1 / 8GB / 256SSD / 13 / Mac OS Big Sur",
                        IsCustom = false,
                        Description = "Apple MacBook Air Retina M1 / 8ГБ / 256SSD / 13 / Mac OS Big Sur",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[4]
                    },
                    new Product 
                    {
                        BarCode = null,
                        Name = "iPhone 11 128GB Black б/у",
                        IsCustom = false,
                        Description = "iPhone 11 128GB Black",
                        Metric = metrics[0],
                        Manufacturer = manufacturers[4]
                    },
                };
                
                foreach(var p in products)
                    p.NormalizeName();

                await context.Products.AddRangeAsync(products);

                var services = new List<Service> 
                {
                    new Service
                    {
                        Name = "Замена масла",
                        Description = "Замена масла",
                        Metric = metrics[0]
                    },
                    new Service
                    {
                        Name = "Замена дисплея IPhone(оригинал)",
                        Description = "Замена дисплея IPhone",
                        Metric = metrics[0]
                    },
                    new Service
                    {
                        Name = "Замена дисплея IPhone(китайский)",
                        Description = "Замена дисплея IPhone",
                        Metric = metrics[0]
                    }
                };
                
                foreach(var s in services)
                    s.NormalizeName();

                await context.Services.AddRangeAsync(services);

                var cities = new List<City>
                {
                    new City
                    {
                        Name = "Almaty",
                        IataCode = "ALA",
                        Longitude = 76.889709,
                        Latitude = 43.238949
                    },
                    new City 
                    {
                        Name ="Shymkent",
                        IataCode = "CIT",
                        Longitude = 69.596329,
                        Latitude = 42.340782
                    }
                };

                await context.Cities.AddRangeAsync(cities);

                var establishments = new List<Establishment> 
                {
                    new Establishment
                    {
                        Name = "Любимый",
                        BankCardNumber = "4894894084",
                        Longitude = 76.901663,
                        Latitude = 43.226266,
                        StartWorkingTime = "00:00",
                        EndWorkingTime = "00:00",
                        IsOpen = true,
                        Address = "улица Жарокова, 198",
                        City = cities[0]
                    },
                    new Establishment
                    {
                        Name = "Бахыт",
                        BankCardNumber = "4894894085",
                        Longitude = 76.904924,
                        Latitude = 43.230299,
                        StartWorkingTime = "09:00",
                        EndWorkingTime = "21:00",
                        IsOpen = false,
                        Address = "улица Ауэзова, 165",
                        City = cities[0]
                    },
                    new Establishment
                    {
                        Name = "Technodom.kz",
                        BankCardNumber = "01256138",
                        Longitude = 76.904502,
                        Latitude = 43.232802,
                        StartWorkingTime = "10:00",
                        EndWorkingTime = "21:00",
                        IsOpen = false,
                        Address = "улица Жандосова, 34А",
                        City = cities[0]
                    },
                    new Establishment
                    {
                        Name = "КОЖ-МАСТЕР",
                        BankCardNumber = "4568693120321152",
                        Longitude = 76.956056,
                        Latitude = 43.245477,
                        StartWorkingTime = "09:00",
                        EndWorkingTime = "20:00",
                        IsOpen = false,
                        Address = "улица Курмангазы, 11",
                        City = cities[0]
                    },
                    new Establishment
                    {
                        Name = "Студент",
                        BankCardNumber = "4563020315856333",
                        Longitude = 76.908976,
                        Latitude = 43.225044,
                        StartWorkingTime = "09:00",
                        EndWorkingTime = "22:00",
                        IsOpen = false,
                        Address = "улица Тимирязева, 42к3",
                        City = cities[0]
                    },
                    new Establishment
                    {
                        Name = "Iron",
                        BankCardNumber = "4568693120386152",
                        Longitude = 76.906721,
                        Latitude = 43.229958,
                        StartWorkingTime = "09:00",
                        EndWorkingTime = "18:00",
                        IsOpen = false,
                        Address = "улица Ауэзова, 122Б",
                        City = cities[0]
                    },
                };

                await context.Establishments.AddRangeAsync(establishments);

                var establishmentsProducts = new List<EstablishmentProduct>
                {
                    new EstablishmentProduct
                    {
                        Price = 735000.00,
                        Amount = 60,
                        IsPresent = true,
                        Establishment = establishments[2],
                        Product = products[10]
                    },
                    new EstablishmentProduct
                    {
                        Price = 800000.00,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[2],
                        Product = products[11]
                    },
                    new EstablishmentProduct
                    {
                        Price = 350,
                        Amount = 30,
                        IsPresent = true,
                        Establishment = establishments[1],
                        Product = products[0]
                    },
                    new EstablishmentProduct
                    {
                        Price = 390,
                        Amount = 50,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[0]
                    },
                    new EstablishmentProduct
                    {
                        Price = 580,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[1],
                        Product = products[1]
                    },
                    new EstablishmentProduct
                    {
                        Price = 600,
                        Amount = 30,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[1]
                    },
                    new EstablishmentProduct
                    {
                        Price = 580,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[1],
                        Product = products[2]
                    },
                    new EstablishmentProduct
                    {
                        Price = 600,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[2]
                    },
                    new EstablishmentProduct
                    {
                        Price = 230,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[3]
                    },
                    new EstablishmentProduct
                    {
                        Price = 340,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[4]
                    },
                    new EstablishmentProduct
                    {
                        Price = 200,
                        Amount = 10,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[5]
                    },
                    new EstablishmentProduct
                    {
                        Price = 190,
                        Amount = 10,
                        IsPresent = true,
                        Establishment = establishments[1],
                        Product = products[5]
                    },
                    new EstablishmentProduct
                    {
                        Price = 230,
                        Amount = 20,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[6]
                    },
                    new EstablishmentProduct
                    {
                        Price = 500,
                        Amount = 0,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[7]
                    },
                    new EstablishmentProduct
                    {
                        Price = 430,
                        Amount = 0,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[8]
                    },
                    new EstablishmentProduct
                    {
                        Price = 500,
                        Amount = 0,
                        IsPresent = true,
                        Establishment = establishments[0],
                        Product = products[9]
                    },
                    new EstablishmentProduct
                    {
                        Price = 180000,
                        Amount = 1,
                        IsPresent = true,
                        Establishment = establishments[4],
                        Product = products[12]
                    }
                };

                await context.EstablishmentProduct.AddRangeAsync(establishmentsProducts);

                var establishmentServices = new List<EstablishmentService>
                {
                    new EstablishmentService
                    {
                        Price = 5000,
                        IsAvailable = true,
                        Establishment = establishments[5],
                        Service = services[0]
                    },
                    new EstablishmentService
                    {
                        Price = 55000,
                        IsAvailable = true,
                        Establishment = establishments[4],
                        Service = services[1]
                    },
                    new EstablishmentService
                    {
                        Price = 35000,
                        IsAvailable = true,
                        Establishment = establishments[4],
                        Service = services[2]
                    },
                };
                await context.EstablishmentService.AddRangeAsync(establishmentServices);
                await context.SaveChangesAsync();
            }
        }
    }
}