using Microsoft.Data.Sqlite;

namespace Tradeflow.Helpers;

public static class UserHelper
{
    public static async Task<int?> GetRequestCallerUserId(string apiKey)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id FROM users WHERE X_API_KEY = $X_API_KEY LIMIT 1";
            command.Parameters.AddWithValue("$X_API_KEY", apiKey);

            var result = await command.ExecuteScalarAsync();

            // If no user found → return null
            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToInt32(result);
        }
    }   


    
}
