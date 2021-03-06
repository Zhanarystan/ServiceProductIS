using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _context.Products
                .AsQueryable()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByNameMatching(string queryString)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9А-Яа-яәӘіІңҢғҒүҮұҰқҚөӨһҺ]");
            queryString = rgx.Replace(queryString, "").ToLower();
            return await _context.Products.Where(p => p.NormalizedName.StartsWith(queryString))
                .Include(p => p.Metric)
                .Include(p => p.Manufacturer)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            product.NormalizeName();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.NormalizeName();
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<int> RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateProductList(List<Product> products)
        {   
            foreach (var p in products)
                p.NormalizeName();
            _context.Products.AddRange(products);
            return await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}