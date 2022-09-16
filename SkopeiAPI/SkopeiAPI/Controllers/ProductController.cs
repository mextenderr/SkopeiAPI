using Microsoft.AspNetCore.Mvc;
using SkopeiAPI.Models;
using SkopeiAPI.Models.Dto;
using SkopeiAPI.UnitOfWorks;
using System.Threading.Tasks;

namespace SkopeiAPI.Controllers
{
    // Register products endpoint
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Insertion of UnitOfWork by the use of Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
            // Retrieving productDto from json body of request
            // Should be using Automappers here but timebox to small to do this.
        {
            Product newProduct = new Product
            {
                Name = createProductDto.Name,
                Quantity = createProductDto.Quantity,
                Price = createProductDto.Price
            };

            bool success = await _unitOfWork.ProductRepo.Add(newProduct);

            if (success)
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetProductById", new { newProduct.Id }, newProduct);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _unitOfWork.ProductRepo.All());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepo.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
            // Should be using Dto object here to extract properties from user, timebox to small for this.
        {
            Product product = await _unitOfWork.ProductRepo.GetById(id);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepo.Update(product, updateProductDto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            Product productToDelete = await _unitOfWork.ProductRepo.GetById(id);

            if (productToDelete == null)
                return BadRequest();

            await _unitOfWork.ProductRepo.Delete(id);
            await _unitOfWork.SaveAsync();

            return Ok(productToDelete);
        }
    }
}
