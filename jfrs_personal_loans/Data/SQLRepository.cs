﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly JFRSPersonalLoansDBContext _context;
        private DbSet<T> _entities;

        public virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        public SQLRepository(JFRSPersonalLoansDBContext context)
        {
            _context = context;
        }

        public T Insert(T entity)
        {
            try
            {
                Entities.Add(entity);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);

            }
            return entity;
        }

        public IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            try
            {
                Entities.AddRange(entities);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
            return entities;

        }
        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public T Update(T entity)
        {
            try
            {
                Entities.Update(entity);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
            return entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            try
            {
                Entities.UpdateRange(entities);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
            return entities;
        }
        public T Delete(T entity)
        {
            try
            {
                Entities.Remove(entity);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);

            }
            return entity;
        }

        public IEnumerable<T> Delete(IEnumerable<T> entities)
        {
            try
            {
                Entities.RemoveRange(entities);
                //_context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
            return entities;
        }


    }
}
