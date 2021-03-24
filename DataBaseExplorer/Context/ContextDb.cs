using DataBaseExplorer.Models;
using System.Data.Entity;

namespace DataBaseExplorer.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(string connectionString) : base(connectionString) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
