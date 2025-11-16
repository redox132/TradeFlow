using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SqliteHelper
{
    public static async Task<List<Dictionary<string, object>>> GetAllRowsAsync(string tableName, int key, string connectionString = "Data Source=Data/db.db")
    {
        var results = new List<Dictionary<string, object>>();

        using (var connection = new SqliteConnection(connectionString))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName} WHERE id = $key";  // dynamically select all columns
            command.Parameters.AddWithValue("$key", key);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object? value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        row[columnName] = value;
                    }

                    results.Add(row);
                }
            }
        }

        return results;
    }
}
