﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IReository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? IncludeProperties = null);
        T GetById(Expression<Func<T, bool>> filter, string? IncludeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);




    }
}