namespace Ambs.Reporting.DAL.Repository.Interfaces;
public interface IGenericCacheRepository<T> where T : class
{
    bool Exists();
    bool Exists(string cacheKey);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(string customCacheKey);
    T GetByKey(string customCacheKey);
    void Add(List<T> entities);
    void Add(List<T> entities, string customCacheKey);
    void Add(T entitiy, string customCacheKey);
    void Delete();
    void Delete(string cacheKey);

    void DeleteAllConfigurationCache(List<string> cacheKeys);
    bool Ping();
}

