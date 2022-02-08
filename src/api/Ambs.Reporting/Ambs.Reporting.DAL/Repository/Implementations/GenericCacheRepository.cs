using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Ambs.Reporting.DAL.Repository.Implementations;
public class GenericCacheRepository<T> : IGenericCacheRepository<T> where T : class
{
    private static object locked = new object();

    private readonly string _cacheKey;
    private readonly string _host;
    private readonly int _timeout;
    //private readonly RedisEndPoint _redisEndpoint;


    public GenericCacheRepository(string host)
    {
        _host= string.IsNullOrEmpty(host) ? "localhost" : host;
    }
    public GenericCacheRepository(string host, string cacheKey)
    {
        _cacheKey = cacheKey;
        _host = string.IsNullOrEmpty(host) ? "localhost" : host;
        _timeout = 5000;
        //_redisEndpoint=new RedisEndpoint();
    }

    public GenericCacheRepository(string host, int timeout)
    {
        _host = string.IsNullOrEmpty(host) ? "localhost" : host;
        _timeout = timeout;
        //_redisEndpoint = new RedisEndpoint
        //{
        //    ConnectTimeout = timeout,
        //    Host = host
        //};
    }

    public GenericCacheRepository(string cacheKey, string host, int timeout)
    {
        _cacheKey = cacheKey;
        _host = string.IsNullOrEmpty(host) ? "localhost" : host;
        _timeout = timeout;
        //_redisEndpoint = new RedisEndpoint
        //{
        //    Host = host,
        //    ConnectTimeout = timeout
        //};
    }

    public bool Ping()
    {
        try
        {
            using (var redisClient = new RedisClient(_host))
            {
                return redisClient.Ping();
            }
        }
        catch
        {
            return false;
        }
    }

    public bool Exists()
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        return redisClient.ContainsKey(_cacheKey);
    }

    public bool Exists(string cacheKey)
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        return redisClient.ContainsKey(cacheKey);
    }
    public void Add(List<T> entities)
    {
        lock (locked)
        {
            using var redisClient = new RedisClient(_host);
            redisClient.ConnectTimeout = _timeout;
            IRedisTypedClient<T> redis = redisClient.As<T>();
            if (redisClient.ContainsKey(_cacheKey)) return;

            IRedisList<T> list = redis.Lists[_cacheKey];
            list.AddRange(entities);
        }
    }

    public void Add(List<T> entities, string customCacheKey)
    {
        lock (locked)
        {
            using var redisClient = new RedisClient(_host);
            redisClient.ConnectTimeout = _timeout;
            IRedisTypedClient<T> redis = redisClient.As<T>();
            if (redisClient.ContainsKey(customCacheKey)) return;

            IRedisList<T> list = redis.Lists[customCacheKey];
            list.AddRange(entities);
        }
    }
    public void Add(T entitiy, string customCacheKey)
    {
        lock (locked)
        {
            using var redisClient = new RedisClient(_host);
            redisClient.ConnectTimeout = _timeout;
            IRedisTypedClient<T> redis = redisClient.As<T>();
            if (redisClient.ContainsKey(customCacheKey)) return;

            IRedisList<T> list = redis.Lists[customCacheKey];
            list.Add(entitiy);
        }
    }

    public void Delete()
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        redisClient.Remove(_cacheKey);
    }

    public void Delete(string cacheKey)
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        redisClient.Remove(cacheKey);
    }

    public void DeleteAllConfigurationCache(List<string> cacheKeys)
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        List<string> allKeys = redisClient.GetAllKeys();

        foreach (string key in allKeys)
        {
            foreach (var cachekey in cacheKeys)
            {
                if (key.Contains(cachekey.Split('*').First()))
                    redisClient.Remove(key);
            }
        }
    }

    public IEnumerable<T> GetAll()
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        var redis = redisClient.As<T>();
        var list = redis.Lists[_cacheKey];
        return list;
    }

    public IEnumerable<T> GetAll(string customCacheKey)
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        var redis = redisClient.As<T>();
        var list = redis.Lists[customCacheKey];
        return list;
    }
    public T GetByKey(string customCacheKey)
    {
        using var redisClient = new RedisClient(_host);
        redisClient.ConnectTimeout = _timeout;
        var redis = redisClient.As<T>();
        var data = redis.Lists[customCacheKey];
        return data.Count>0? data[0]:null;
    }


}

