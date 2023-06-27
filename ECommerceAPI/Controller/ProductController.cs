using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace ECommerceAPI.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo repo)
        {
            _productRepo = repo;
        }

        /// <summary>
        /// Returns a list of products available in the database
        /// </summary>
        /// <returns>Retruns a list of products</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productRepo.GetAllProducts();

            return Ok(products);
        }


        /// <summary>
        /// Returns a specific product with correspondig identification number
        /// </summary>
        /// <param name="id">Requests the product ID we want to search</param>
        /// <returns>Returns a specific product according to the given ID</returns>
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepo.GetProductById(id);

            if(product == null) { return NotFound(); }

            return Ok(product);
        }


        /// <summary>
        /// Creates a new Product with the specified data
        /// </summary>
        /// <param name="product">Requesta all the data the product needs</param>
        /// <returns>Nothing but a 201 code</returns>
        /// <response code="201">Product created succesfully</response>\   
        /// <remarks>
        ///     Sample Request 
        ///     POST /api/product
        ///     {
        ///        "name": "string",
        ///        "price": 0.00
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepo.CreateProduct(product);

            await _productRepo.SaveChanges();

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id }, product);
        }



        /// <summary>
        /// Updates a product with new information
        /// </summary>
        /// <param name="id">The ID of the product to update</param>
        /// <param name="prod">The new information to update along with the old data</param>
        /// <returns>No Content</returns>
        /// <remarks>
        ///     Sample Request
        ///     PUT /api/product/{id}
        ///     {
        ///        "name": "string",
        ///        "price": 0.00
        ///     }
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product prod)
        {
            var productFromDb = await _productRepo.GetProductById(id);

            if(productFromDb == null) return NotFound();

            productFromDb.Name = prod.Name;
            productFromDb.Price = prod.Price;

            await _productRepo.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Deletes a product from the database via ID
        /// </summary>
        /// <param name="id">The ID of the product we want to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productFormDb = await _productRepo.GetProductById(id);
            
            if(productFormDb== null) return NotFound();

            _productRepo.DeleteProduct(productFormDb);

            await _productRepo.SaveChanges();

            return NoContent();
        }
    }
}
