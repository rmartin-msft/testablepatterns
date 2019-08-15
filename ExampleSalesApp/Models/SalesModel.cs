using ExampleSalesApp.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Models
{
    public class SalesModel : ISalesModel
    {
        #region Protected
        protected IRepository<Customer> _customerData;

        protected Customer _currentCustomer;
        
        public Customer CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }
            set
            {
                _currentCustomer = value;

                NotifyDataChanged();
            }
        }

        protected void NotifyDataChanged([CallerMemberName] string propertyName = "")
        {
            ModelPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public event PropertyChangedEventHandler ModelPropertyChanged;
        
        public SalesModel(IRepository<Customer> customerRepo)
        {
            _customerData = customerRepo;
        }

        public IList<Customer> GetAllCustomers()
        {
            return _customerData.GetAll();
        }        

        public void UpdateCustomer(Customer c)
        {            
            Customer existingCustomer = _customerData.GetById(c.Id);

            if (null != existingCustomer)
            {
                existingCustomer = c;
                _customerData.Update(c);
            }
            else
            {
                _customerData.Insert(c);
            }

            NotifyDataChanged();                      
        }

        public Customer FindCustomerById(int id)
        {
            return _customerData.Query(x => x.Id == id).SingleOrDefault();
        }

        public IList<Customer> FindCustomersBySurname(string surname)
        {
            return _customerData.Query(x => x.Surname == surname).ToList();
        }

        public bool CanSaveChanges()
        {
            return CurrentCustomer != null;
        }

        public void DeleteCustomer(Customer c)
        {
            _customerData.Delete(c);

            if (c == CurrentCustomer)
            {
                CurrentCustomer = null;
            }

            NotifyDataChanged();
        }
    }
}
