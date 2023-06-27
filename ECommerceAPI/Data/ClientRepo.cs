using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerceAPI.Data
{
    public class ClientRepo : IClientRepo
    {
        private readonly ECommerceContext _context;
        public ClientRepo(ECommerceContext context)
        {
            _context = context;
        }
        public async Task CreateClient(Client clt)
        {
            if(clt == null) throw new ArgumentNullException(nameof(clt));
            await _context.Clients.AddAsync(clt);
        }

        public void DeleteClient(Client clt)
        {
            if(clt == null) { throw new ArgumentNullException(nameof(clt)); } 
            _context.Clients.Remove(clt);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
           return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
