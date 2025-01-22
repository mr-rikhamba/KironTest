using System;
using System.Collections.Concurrent;
using System.Data;
using Microsoft.Data.SqlClient;

namespace KironTest.Logic.Helpers;

public class ConnectionManager
{

    private ConcurrentBag<IDbConnection> _connections = new ConcurrentBag<IDbConnection>();
    private static string? _connectionString;
    public int Count { get; set; }

    public IDbConnection GetConnection()
    {
        lock (_connections)
        {
            if (_connections.Count <= 10)
            {
                var connection = new SqlConnection(_connectionString);
                _connections.Add(connection); Count++;
                return connection;
            }
        }
        throw new InvalidOperationException("Application has exceeded is attempting to create more than 10 connections");
    }

    public static void InitialiseConnectionString(string? connectionString)
    {
        _connectionString = connectionString ?? throw new Exception("Connection string cannot be empty.");
    }

    public void CloseAndDiscard()
    {
        IDbConnection? connection;
        _connections.TryTake(out connection);
        if (connection is not null)
        {
            Count--;
            connection?.Close();
        }
    }

}
