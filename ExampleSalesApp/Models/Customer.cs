//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Models
{
    public class Customer : IEntityBase
    {
        public Customer()
        {
            _id = GetHashCode();
        }
        public Customer(string firstname, string surname) : this()
        {
            Surname = surname;
            Firstname = firstname;
        }
        [Key]
        private int _id;
        public int Id { get => _id; set => _id = value; }

        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Fullname { get => Firstname + "," + Surname; }
    }
}
