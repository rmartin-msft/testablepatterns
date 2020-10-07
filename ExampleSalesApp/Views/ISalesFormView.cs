//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

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
