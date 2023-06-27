namespace ECommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Order>? Orders { get; set; }
        public Product(string name, double price) 
        {
            Name = name;
            Price = price;
        }

        public Product() { }
    }
}
