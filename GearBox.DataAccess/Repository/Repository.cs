﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GearBox.DataAccess.Data;
using GearBox.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GearBox.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // For dependency Injection:
        private readonly ApplicationDbContext _db;
        // Db of the class (which could be different ones)
        internal DbSet<T> dbSet;
        // Constructor
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        
        
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        // T any class, here we are working with Category class
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

    }
}
