using ExampleSalesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository()
        {
            //_data.Add(new Customer() { Firstname = "Test", Surname = "Customer" });
            //_data.Add(new Customer() { Firstname = "Example", Surname = "Customer" });
        }
    }
}
