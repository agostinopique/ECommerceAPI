using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetRelatedProducts(int id);
        Task CreateProduct(Product prod);
        Task SaveChanges();
        void DeleteProduct(Product prod);
    }
}
