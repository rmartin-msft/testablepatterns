//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

using ExampleSalesApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Repositories
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
    }
    public class CustomerEFRepository : IRepository<Customer>
    {
        DataContext _context = new DataContext();

        public void Delete(Customer entity)
        {
            _context.customers.Remove(entity);
            _context.SaveChanges();
        }

        public Customer[] GetAll()
        {
            return _context.customers.ToArray();

        }

        public Customer GetById(int id)
        {
            return _context.customers.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Insert(Customer entity)
        {
            _context.customers.Add(entity);
            _context.SaveChanges();
        }

        public IQueryable<Customer> Query(Expression<Func<Customer, bool>> expression)
        {
            return _context.customers.Where(expression);
        }

        public void Update(Customer entity)
        {
            Customer c = _context.customers.Where(x => x.Id == entity.Id).First();
            c = entity;
            _context.SaveChanges();
        }
    }
}
