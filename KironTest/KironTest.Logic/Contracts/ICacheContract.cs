using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface ICacheContract
{
    void Set<T>(string key, T data, int ttlMins = 1);
    CacheResponseModel<T> Get<T>(string key);
}
