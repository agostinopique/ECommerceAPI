using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int IdentificationNumber { get; set; }

        public double? TotalPrice { get; set; }
        public int ClientId { get; set; }

        public Client? Client { get; set; }
        public List<Product>? Products { get; set; }

        public Order()
        {
            Random rnd = new Random();
            IdentificationNumber = rnd.Next();
        }
        public Order(int id, double? totalPrice, int clientId)
        {
            Id = id;
            IdentificationNumber = new Random().Next();
            TotalPrice = totalPrice;
            ClientId = clientId;
        }
    }
}
