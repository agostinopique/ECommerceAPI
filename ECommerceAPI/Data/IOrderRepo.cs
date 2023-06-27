using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetRelatedOrders(int id);
        Task CreateOrder(Order ord);
        Task SaveChanges();
        void DeleteOrder(Order ord);
        double CalculatePrice(List<Product> prods);
    }
}
