using AutoMapper;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public OrderController(IOrderRepo orderRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }


        /// <summary>
        /// Returns a list with all the orders available in the database
        /// </summary>
        /// <returns>Returna a list of orders</returns>
        /// <remarks>
        ///     Sample Request
        ///     GET api/orders
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrders();

            return Ok(orders);
        }


        /// <summary>
        /// Returns the order with the corresponding id
        /// </summary>
        /// <param name="id">Requests the id of the desired order</param>
        /// <returns>Returns a specific order</returns>
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var orderFromDb = await _orderRepo.GetOrderById(id);
            
            if(orderFromDb == null) { return NotFound(); }

            orderFromDb.Products = (List<Product>?)await _productRepo.GetRelatedProducts(orderFromDb.Id);

            return Ok(orderFromDb);
        }


        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="ord">Only the ClientID is needed for the creation of a new order</param>
        /// <returns>Returns the newly created order</returns>
        /// <remarks>
        ///     Sample Request
        ///     {
        ///         "ClientId" : int
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order ord)
        {
            if (ord == null) return BadRequest();

            await _orderRepo.CreateOrder(ord);

            await _orderRepo.SaveChanges();

            return CreatedAtRoute(nameof(GetOrderById), new { id = ord.Id }, ord);
        }


        /// <summary>
        /// Updates a specific order identified by the given ID
        /// </summary>
        /// <param name="id">Specify the id of the order you want to update</param>
        /// <param name="ord">Give new parameters</param>
        /// <returns>No Content</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Order ord)
        {
            var orderFromDb = await _orderRepo.GetOrderById(id);

            if(orderFromDb == null) return NotFound();

            

            orderFromDb.Products = (List<Product>?)await _productRepo.GetRelatedProducts(orderFromDb.Id);

            orderFromDb.Products.Clear();

            List<Product> products = new List<Product>(); 

            foreach(Product prod in ord.Products)
            {
                products.Add(await _productRepo.GetProductById(prod.Id));
            }

            orderFromDb.ClientId = ord.ClientId;
            orderFromDb.Products = products;
            orderFromDb.TotalPrice = _orderRepo.CalculatePrice(orderFromDb.Products);

            await _orderRepo.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Deletes an order who's selecte via ID 
        /// </summary>
        /// <param name="id">The ID of the order to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var orderFromDb = await _orderRepo.GetOrderById(id);

            if (orderFromDb == null) return NotFound();

            _orderRepo.DeleteOrder(orderFromDb);

            await _orderRepo.SaveChanges();

            return NoContent();
        }

    }
}
