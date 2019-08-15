using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ExampleSalesApp.Models;
using ExampleSalesApp.Views;

namespace ExampleSalesApp.Controllers
{
    public class SalesFormController : ISalesFormController
    {
        private ISalesModel _model;
        private ISalesFormView _view;

        public SalesFormController(ISalesFormView view, ISalesModel model)
        {
            _view = view;
            _model = model;

            _model.ModelPropertyChanged += _model_ModelPropertyChanged;

            _view.SetController(this);
            LoadCustomers();
        }        

        private void _model_ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "CurrentCustomer")
                LoadCustomers();

            _view.ClearCustomerInfo();
            if (_model.CurrentCustomer != null)
            {
                _view.ShowCustomerInfo(_model.CurrentCustomer);
            }

            _view.EnableSaveChanges(_model.CanSaveChanges());
            _view.EnableDeleteCustomer(_model.CanSaveChanges());
        }

        public void NewCustomer()
        {
            Customer c = new Customer() { Firstname = "New", Surname = "Customer" };

            _model.CurrentCustomer = c;

            _view.FocusCustomerEditFields();            
        }

        public ISalesFormView GetView()
        {
            return _view;
        }

        public void LoadCustomers()
        {
            _view.ClearCustomerList();
            foreach (Customer c in _model.GetAllCustomers())
            {
                _view.AddUserToList(c);
            }

            if (_model.CurrentCustomer != null)
            {
                _view.ShowCustomerInfo(_model.CurrentCustomer);
            }

            _view.EnableSaveChanges(_model.CanSaveChanges());
            _view.EnableDeleteCustomer(_model.CanSaveChanges());
        }

        public void SelectedCustomerChanged(int id)
        {
            _model.CurrentCustomer = _model.FindCustomerById(id);
            _view.ShowCustomerInfo(_model.CurrentCustomer);            
        }

        public void SaveChanges()
        {
            Customer customer = _model.CurrentCustomer;
            customer.Firstname = _view.CustomerFirstname;
            customer.Surname = _view.CustomerLastname;
            _model.UpdateCustomer(customer);            
        }

        public void DeleteCustomer()
        {
            Customer customer = _model.CurrentCustomer;

            if (_view.DisplayQuestion("Are you sure you want to delete this customer?"))
            {
                _model.DeleteCustomer(customer);
            }
        }
    }
}
