using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Models
{
    public delegate void ModelChangedEventHandler(object sender, EventArgs e);

    public interface ISalesModel
    {
        event PropertyChangedEventHandler ModelPropertyChanged;
        Customer CurrentCustomer { get; set; }
        IList<Customer> GetAllCustomers();
        void UpdateCustomer(Customer c);
        void DeleteCustomer(Customer c);
        bool CanSaveChanges();

        Customer FindCustomerById(int id);
        IList<Customer> FindCustomersBySurname(string surname);
    }
}
