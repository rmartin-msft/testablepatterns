using ExampleSalesApp.Controllers;
using ExampleSalesApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Views
{
    public interface ISalesFormView
    {
        void SetController(ISalesFormController controller);
        void AddUserToList(Customer c);
        void ClearCustomerList();
        void ShowCustomerInfo(Customer c);
        void ClearCustomerInfo();
        void EnableSaveChanges(bool enable);
        void EnableDeleteCustomer(bool enable);
        void DisplayErrorMessage(string errorMessage);
        void FocusCustomerEditFields();

        string CustomerFirstname { get; }
        string CustomerLastname { get; }

        bool DisplayQuestion(string question);
    }
}
