using System;

public interface IDatabase
{
    bool Connect(string connectionString);
    bool Query(string query);
}
