using FlashCards.Data.DataModel;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FlashCards.Services.UnitOfWork.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private FlashcardsDataModel _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(FlashcardsDataModel context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }  
    }
}
