using ServiceStack.Redis;

namespace Ambs.Reporting.DAL.Repository.Implementations;
public class InvalidateCacheRepository : IInvalidateCacheRepository
{
    private string _host;
    public InvalidateCacheRepository(string host)
    {
        _host = string.IsNullOrEmpty(host) ? "localhost" : host;
    }

    public void Invalidate(string cacheKey)
    {
        using (var redisClient = new RedisClient(_host))
        {
            redisClient.Remove(cacheKey);
        }

    }

    public void Invalidate()
    {
        using (var redisClient = new RedisClient(_host))
        {
            redisClient.FlushAll();
        }
    }
}
