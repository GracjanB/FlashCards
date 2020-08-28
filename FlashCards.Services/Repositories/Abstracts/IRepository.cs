using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlashCards.Services.UnitOfWork.Abstracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
