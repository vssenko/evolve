using Evolve.Infrastructure.DB.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Evolve.Infrastructure.DB.EF.LinqSpec;

namespace Evolve.Infrastructure.DB.EF.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public bool IsNoCommit
        {
            get
            {
                return UnitOfWork.IsNoCommit;
            }
            set
            {
                UnitOfWork.IsNoCommit = value;
            }

        }

        protected IUnitOfWork<TEntity> UnitOfWork { get; private set; }

        public Repository(IUnitOfWork<TEntity> unitOfWork)
        {
            UnitOfWork = unitOfWork;
            IsNoCommit = false;
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }

        public TEntity Insert(TEntity entity)
        {
            var result = UnitOfWork.Insert(entity);
            if (!IsNoCommit)
            {
                UnitOfWork.Commit();
            }
            return result;
        }

        public void Delete(TEntity entity)
        {
            UnitOfWork.Delete(entity);
            if (!IsNoCommit)
            {
                UnitOfWork.Commit();
            }
        }

        public TEntity Update(TEntity entity)
        {
            var result = UnitOfWork.Update(entity);
            if (!IsNoCommit)
            {
                UnitOfWork.Commit();
            }
            return result;
        }

        public TEntity GetBySpec(QueryParams<TEntity> queryParams)
        {
            return DataFilter<TEntity>.Apply(GetQueryable(), queryParams.Specification, queryParams.IncludeSpec).FirstOrDefault();
        }

        public Task<TEntity> GetBySpecAsync(QueryParams<TEntity> queryParams)
        {
            return DataFilter<TEntity>.Apply(GetQueryable(), queryParams.Specification, queryParams.IncludeSpec).FirstOrDefaultAsync();
        }

        public List<TEntity> GetList(QueryParams<TEntity> queryParams = null)
        {
            if (queryParams != null)
            {
                var data = DataFilter<TEntity>.Apply(GetQueryable(), queryParams.Specification, queryParams.IncludeSpec);
                if (queryParams.Pagination != null)
                {
                    data = data.OrderBy(queryParams.Pagination.FuckingEFHackExprToIntKey).Skip(queryParams.Pagination.Skip).Take(queryParams.Pagination.Take);
                }
                return data.ToList();
            }
            else
            {
                return DataFilter<TEntity>.Apply(GetQueryable(), null, null).ToList();
            }
            
        }

        public Task<List<TEntity>> GetListAsync(QueryParams<TEntity> queryParams = null)
        {
            if (queryParams != null)
            {
                var data = DataFilter<TEntity>.Apply(GetQueryable(), queryParams.Specification, queryParams.IncludeSpec);
                if (queryParams.Pagination != null)
                {
                    data = data.OrderBy(queryParams.Pagination.FuckingEFHackExprToIntKey).Skip(queryParams.Pagination.Skip).Take(queryParams.Pagination.Take);
                }
                return data.ToListAsync();
            }
            else
            {
                return DataFilter<TEntity>.Apply(GetQueryable(), null, null).ToListAsync();
            }
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return UnitOfWork.AsQueryable();
        }


    }
}
