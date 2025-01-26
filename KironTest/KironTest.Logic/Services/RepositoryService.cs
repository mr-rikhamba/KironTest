using System;
using System.Data;
using Dapper;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;

namespace KironTest.Logic.Services;

public class RepositoryService(ConnectionManager connectionManager) : IRepositoryContract
{
    public async Task ExecuteTvp(DataTable dTbl, string spName, DynamicParameters parameters)
    {
        using (var connection = await connectionManager.GetConnectionAsync())
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(spName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connectionManager.CloseAndDiscard();
                }
            }
        }
    }
    public async Task<List<T>> Execute<T>(string spName, DynamicParameters parameters)
    {

        using (var connection = await connectionManager.GetConnectionAsync())
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(spName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return result.ToList();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connectionManager.CloseAndDiscard();
                }
            }
        }
    }
    public async Task<List<T>> Execute<T>(string spName)
    {
        using (var connection = await connectionManager.GetConnectionAsync())
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(spName, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return result.ToList();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connectionManager.CloseAndDiscard();
                }
            }
        }
    }

    public async Task<T> ExecuteSingle<T>(string spName, DynamicParameters parameters)
    {
        using (var connection = await connectionManager.GetConnectionAsync())
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(spName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return result.FirstOrDefault();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connectionManager.CloseAndDiscard();
                }
            }
        }
    }
}
