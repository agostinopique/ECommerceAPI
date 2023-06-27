﻿namespace ECommerceAPI.Models
{
    public class Client
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public List<Order>? Orders { get; set; }
        public Client() { }

        public Client(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}
