using AutoMapper;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerceAPI.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepo _clientRepo;
        private readonly IOrderRepo _orderRepo;
        public ClientController(IClientRepo repo, IOrderRepo orderRepo)
        {
            _clientRepo = repo;
            _orderRepo = orderRepo;
        }


        /// <summary>
        /// Returns the list of clients available in the database;
        /// </summary>
        /// <returns>Returns a list of clients</returns>
        /// <remarks>
        ///     Sample Request 
        ///     GET /api/clients
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var clients = await _clientRepo.GetAllClients();

            return Ok(clients);

        }

        /// <summary>
        /// Returns the client with corresponding ID number
        /// </summary>
        /// <param name="id">Requests the id to search with</param>
        /// <returns>Retruns a client specified by the ID parameter</returns>
        [HttpGet("{id}", Name = "GetClientById")]   
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var clientModel = await _clientRepo.GetClientById(id);
            if (clientModel == null) { return NotFound(); }
            
            clientModel.Orders = (List<Order>?)await _orderRepo.GetRelatedOrders(id);

            return Ok(clientModel);
        }


        /// <summary>
        /// Creates a new Client with the specififed data
        /// </summary>
        /// <param name="clt">Requests Full Name and Email. All other data will be added automaticly or later.</param>
        /// <returns>Nothing but a 201 code</returns>
        /// <response code="201">Client created succesfully</response>\   
        /// <remarks>
        ///     Sample Request 
        ///     POST /api/clients
        ///     {
        ///       "fullName": "string",
        ///       "email": "string"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client clt)
        {
            await _clientRepo.CreateClient(clt);

            await _clientRepo.SaveChanges();
            
            Console.WriteLine($"Model state is: {ModelState.IsValid}");

            return CreatedAtRoute(nameof(GetClientById), new { id = clt.Id }, clt);
        }


        /// <summary>
        /// Updates a client with new information
        /// </summary>
        /// <param name="id">The ID of the client we want to update</param>
        /// <param name="clt">The new information to update</param>
        /// <returns>No Content</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, Client clt)
        {
            var clientFromDb = await _clientRepo.GetClientById(id);

            if(clientFromDb == null) return NotFound();

            clientFromDb.FullName = clt.FullName;
            clientFromDb.Email = clt.Email;

            await _clientRepo.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Deletes a client who's selecte via ID 
        /// </summary>
        /// <param name="id">The ID of the client to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var clientFromDb = await _clientRepo.GetClientById(id);

            if(clientFromDb == null) return NotFound();

            _clientRepo.DeleteClient(clientFromDb);

            await _clientRepo.SaveChanges();

            return NoContent();
        }
    }
}
