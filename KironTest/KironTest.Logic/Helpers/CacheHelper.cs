using System;
using KironTest.Logic.Contracts;
using KironTest.Logic.Models;
using Microsoft.Extensions.Caching.Memory;

namespace KironTest.Logic.Helpers;
public class CacheHelper(IMemoryCache _cache) : ICacheContract
{
    public void Set<T>(string key, T data, int ttlMins = 1)
    {
        _cache.Set<T>(key, data, DateTime.Now.AddMinutes(ttlMins));
    }

    public CacheResponseModel<T> Get<T>(string key)
    {
        var data = _cache.Get<T>(key);
        return new CacheResponseModel<T>
        {
            Data = data,
            HasData = data != null
        };
    }
}
