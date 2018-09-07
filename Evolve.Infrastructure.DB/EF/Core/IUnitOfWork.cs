using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.Core
{
    public interface IUnitOfWork<TEntity> where TEntity : class
    {
        bool IsNoCommit { get; set; }

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete<TId>(TId id);

        TEntity Find<TId>(TId id);

        IEnumerable<TEntity> GetAll();

        Task<List<TEntity>> GetAllAsync();

        IQueryable<TEntity> AsQueryable(bool withTracking = false);

        void Commit();
    }
}
