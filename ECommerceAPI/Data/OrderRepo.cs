using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ECommerceAPI.Data
{
    public class OrderRepo : IOrderRepo
    {

        private readonly ECommerceContext _context;
        public OrderRepo(ECommerceContext context)
        {
            _context = context;
        }
        public async Task CreateOrder(Order ord)
        {
            if(ord == null) throw new ArgumentNullException(nameof(ord));

            //List<Product> products = new List<Product>();

            //foreach (Product prod in ord.Products)
            //{
            //    products.Add(_context.Products.Find(prod.Id));
            //}

            //ord.Products = products;
            ord.TotalPrice = CalculatePrice(ord.Products);

            await _context.AddAsync(ord);
        }

        public void DeleteOrder(Order ord)
        {
            if (ord == null) throw new ArgumentNullException(nameof(ord));

            _context.Remove(ord);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _context.Orders.ToListAsync();
            foreach(Order ord in orders)
            {
                ord.Client = await _context.Clients.FindAsync(ord.ClientId);
            }

            return orders;
            //return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveChanges()
        {
           await _context.SaveChangesAsync();
        }

        public double CalculatePrice(List<Product> prods)
        {
            double totalPrice = 0;
            foreach (Product prod in prods)
            {
                totalPrice += prod.Price;
            }

            return Math.Round(totalPrice, 2);
        }

        public async Task<IEnumerable<Order>> GetRelatedOrders(int id)
        {

            var ordersRelated = await _context.Clients.Include("Orders").FirstAsync(x => x.Id == id);

            IEnumerable<Order> orders = ordersRelated.Orders;

            return orders;
        }
    }
}
