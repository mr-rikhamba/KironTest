using System;
using System.Collections.Concurrent;
using System.Data;
using Microsoft.Data.SqlClient;

namespace KironTest.Logic.Helpers;

public class ConnectionManager
{

    private readonly ConcurrentBag<IDbConnection> _connections = new ConcurrentBag<IDbConnection>();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private static string? _connectionString;
    public int Count { get; set; }

    public async Task<IDbConnection> GetConnectionAsync()
    {
        try
        {
            await _semaphore.WaitAsync();
            if (_connections.Count <= 10)
            {
                var connection = new SqlConnection(_connectionString);
                _connections.Add(connection);
                Count++;
                await connection.OpenAsync();
                return connection;
            }

        }
        finally
        {
            _semaphore.Release();
        }
        throw new InvalidOperationException("Application has exceeded is attempting to create more than 10 connections");
    }
    public static void InitialiseConnectionString(string? connectionString)
    {
        _connectionString = connectionString ?? throw new InvalidDataException("Connection string cannot be empty.");
    }

    public void CloseAndDiscard()
    {
        _connections.TryTake(out IDbConnection? connection);
        if (connection is not null)
        {
            Count--;
            connection?.Close();
        }
    }

}
