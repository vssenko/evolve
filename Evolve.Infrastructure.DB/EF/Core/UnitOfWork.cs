using Evolve.Infrastructure.DB.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Evolve.Infrastructure.DB.EF.Core
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        public bool IsNoCommit
        {
            get
            {
                return _context.IsNoCommit;
            }
            set
            {
                _context.IsNoCommit = value;
            }
        }

        protected DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null.");
            return Set().Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TEntity updated = Set().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();

            return updated;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Set().Remove(entity);
        }

        public void Delete<TId>(TId id)
        {
            Delete(Find<TId>(id));
        }

        public TEntity Find<TId>(TId id)
        {
            return Set().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Set().ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            var dbSet = Set();
            return dbSet.ToListAsync();
        }

        public IQueryable<TEntity> AsQueryable(bool withTracking = false)
        {
            IQueryable<TEntity> set = Set();
            /*if (!withTracking)
                set = set.AsNoTracking();*/
            return set;
        }

        public void Commit()
        {
            DetectChanges();
            SaveChanges();
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IDbSet<TEntity> Set()
        {
            return _context.Set<TEntity>();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_context == null)
                return;

            Commit();
            _context.Dispose();
            _context = null;
        }

        private void DetectChanges()
        {
            _context.ChangeTracker.DetectChanges();
        }

        private void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                throw new DbUpdateConcurrencyException("Optimistic concurrency violation occurs.", concurrencyException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new DbUpdateException("UnitOfWork failed to commit changes.", dbUpdateException);
            }
            /*catch validation fields exception*/
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        var property = validationError.PropertyName;
                        var message = validationError.ErrorMessage;
                    }
                }
            }
        }

    }
}