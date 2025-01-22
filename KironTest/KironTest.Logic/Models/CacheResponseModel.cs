using System;

namespace KironTest.Logic.Models;

public class CacheResponseModel<T>
{
    public bool HasData { get; set; }
    public T Data { get; set; }
}
