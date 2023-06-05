namespace Arahk.CMS.Application.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);

    Task<T?> GetAsync(Guid id);

    Task<List<T>> ListAsync();

    Task CommitChangedAsync();
}