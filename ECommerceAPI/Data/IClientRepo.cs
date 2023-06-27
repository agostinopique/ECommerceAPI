using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public interface IClientRepo
    { 
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task CreateClient(Client clt);
        Task SaveChanges();
        void DeleteClient(Client clt);
    }
}
