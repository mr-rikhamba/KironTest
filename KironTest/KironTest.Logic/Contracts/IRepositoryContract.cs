using System;
using System.Data;
using Dapper;

namespace KironTest.Logic.Contracts;

public interface IRepositoryContract
{
    Task ExecuteTvp(DataTable dTbl, string spName, DynamicParameters parameters);
    Task<List<T>> Execute<T>(string spName, DynamicParameters parameters);
    Task<T> ExecuteSingle<T>(string spName, DynamicParameters parameters);
    Task<List<T>> Execute<T>(string spName);
}
