using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.Infrastructure.MongoDB.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity obj);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    void Update(TEntity obj);
    void Remove(TEntity obj);
    void Upsert(TEntity obj);
    IMongoQueryable<TEntity> QueryAll();
    Task<BulkWriteResult<TEntity>> BulkUpsert(List<TEntity> records);
}
