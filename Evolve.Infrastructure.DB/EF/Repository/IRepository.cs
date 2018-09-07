using Evolve.Infrastructure.DB.EF.LinqSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool IsNoCommit { get; set; }

        void Commit();

        TEntity Insert(TEntity entity);

        void Delete(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity GetBySpec(QueryParams<TEntity> queryParams);

        Task<TEntity> GetBySpecAsync(QueryParams<TEntity> queryParams);

        List<TEntity> GetList(QueryParams<TEntity> queryParams = null);

        Task<List<TEntity>> GetListAsync(QueryParams<TEntity> queryParams = null);
    }
}
