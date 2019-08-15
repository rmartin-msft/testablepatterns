using ExampleSalesApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Controllers
{
    public interface ISalesFormController
    {
        ISalesFormView GetView();
        void SelectedCustomerChanged(int id);
        void LoadCustomers();
        void NewCustomer();
        void SaveChanges();

        void DeleteCustomer();
    }
}
