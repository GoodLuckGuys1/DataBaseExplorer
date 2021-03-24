using DataBaseExplorer.Context;
using DataBaseExplorer.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataBaseExplorer.Repositories
{
    public class CustomerRepository
    {
        ContextDb context;
        public CustomerRepository(ContextDb context)
        {
            this.context = context;
        }

        public ObservableCollection<Customer> GetCustomers()
        {
            return new ObservableCollection<Customer>(context.Customers.ToList());
        }
    }
}
