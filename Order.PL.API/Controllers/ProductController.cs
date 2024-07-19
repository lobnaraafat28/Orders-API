using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.PL.API.DTOs;
using Orders.Core.Interfaces;
using Orders.Core.Models;
using System.Data;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> repo;

        public ProductController(IGenericRepository<Product> repo)
        {
            this.repo = repo;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await repo.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ProductDTO> GetProductById(int id)
        {
            var product  = await repo.GetByIdAsync(id);
            var productDto = new ProductDTO()
            {
                Stock = product.Stock,
                Name = product.Name,
                Price = product.Price
            };
            return productDto;
        }
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin")]

        public async Task CreateNewProduct(ProductDTO productdto)
        {
            var product = new Product()
            {
                Stock = productdto.Stock,
                Name = productdto.Name,
                Price = productdto.Price
            };
            await repo.Add(product);
        }
        [HttpPut("UpdateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task UpdateProductDetails(int id, ProductDTO productdto)
        {
            var product = await repo.GetByIdAsync(id);
            product.Name = productdto.Name;
            product.Price = productdto.Price;
            product.Stock = productdto.Stock;
            repo.Update(product);
        }
    
    }
}
