using System;
using System.Collections.Generic;
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
        private int _id;
        public int Id { get => _id; set => _id = value; }

        public string Surname { get; set; }
        public string  Firstname { get; set; }
        public string Fullname { get => Firstname + "," + Surname; }
    }
}
