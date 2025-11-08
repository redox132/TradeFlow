namespace LatestEcommAPI.Helpers;

using Microsoft.Data.Sqlite;


class SqliteDataBase
{
    public static void GetOne(int id)
    {
        var connection = new SqliteConnection("Data source=Data/db.db");
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM user WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
    }
}

