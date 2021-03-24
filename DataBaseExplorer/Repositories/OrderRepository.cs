using DataBaseExplorer.Context;
using DataBaseExplorer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseExplorer.Repositories
{
    public class OrderRepository
    {
        ContextDb context;
        public OrderRepository(ContextDb context)
        {
            this.context = context;
        }
        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }
    }

}
