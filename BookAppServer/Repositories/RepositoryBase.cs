using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookAppServer.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        readonly protected RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
            => _repositoryContext = repositoryContext;

        public IQueryable<T> FindAll() =>
              _repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
              _repositoryContext.Set<T>().Where(expression);

        public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);

        public async Task Update(T entity) => _repositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);
    }
}
