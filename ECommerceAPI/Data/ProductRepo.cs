using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly ECommerceContext _context;
        public ProductRepo(ECommerceContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product prod)
        {
            if (prod == null) { throw new ArgumentNullException(nameof(prod)); }

            await _context.AddAsync(prod);
        }

        public void DeleteProduct(Product prod)
        {
            if (prod == null) throw new ArgumentNullException(nameof(prod));

            _context.Remove(prod);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Product>> GetRelatedProducts(int id)
        {

            var productsRelated = await _context.Orders.Include("Products").FirstAsync(x => x.Id == id);

            IEnumerable<Product> products = productsRelated.Products;

            return products;
        }
    }
}
