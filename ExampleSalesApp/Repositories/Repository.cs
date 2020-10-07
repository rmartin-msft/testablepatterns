//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

using ExampleSalesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntityBase
    {
        protected List<T> _data = new List<T>();
        public void Delete(T entity)
        {
            _data.Remove(entity);
        }

        public T[] GetAll()
        {
            return _data.ToArray();
        }

        public T GetById(int id)
        {
            return _data.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Insert(T entity)
        {
            _data.Add(entity);
        }

        public void Update(T entity)
        {
            T e = _data.FirstOrDefault(x => x.Id == entity.Id);
            e = entity;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            return _data.AsQueryable().Where(expression);
        }
    }
}
