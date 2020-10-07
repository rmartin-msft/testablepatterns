//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

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
