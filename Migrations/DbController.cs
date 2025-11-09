using System;
using Microsoft.Data.Sqlite;

namespace LatestEcommAPI.Migrations
{
    public class DbController
    {
        public static void Migrate()
        {
            using var connection = new SqliteConnection("Data Source=Data/db.db");
            connection.Open();

            // Create user table
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS user (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    email TEXT NOT NULL UNIQUE,
                    password VARCHAR NOT NULL,
                    name TEXT NOT NULL
                );
            """);

            // Create product table
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS product (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    price REAL NOT NULL
                );
            """);

            // Create orders table
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS orders (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    shipper_id INTEGER,
                    status TEXT NOT NULL DEFAULT 'pending' CHECK(status IN ('pending', 'shipped', 'delivered', 'cancelled')),
                    order_date timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY(user_id) REFERENCES user(id),
                    FOREIGN KEY(shipper_id) REFERENCES shipper(id)
                );
            """);

            // Create order_item table
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS order_item (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    order_id INTEGER NOT NULL,
                    product_id INTEGER NOT NULL,
                    quantity INTEGER NOT NULL,
                    FOREIGN KEY(order_id) REFERENCES orders(id),
                    FOREIGN KEY(product_id) REFERENCES product(id)
                );
            """);

            // Create inventory table
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS inventory (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    product_id INTEGER NOT NULL,
                    stock_quantity INTEGER NOT NULL,
                    FOREIGN KEY(product_id) REFERENCES product(id)
                );
            """);

            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS address (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    street TEXT NOT NULL,
                    city TEXT NOT NULL,
                    state TEXT NOT NULL,
                    zip_code TEXT NOT NULL,
                    country TEXT NOT NULL,
                    FOREIGN KEY(user_id) REFERENCES user(id)
                );
            """);

            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS cart (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    FOREIGN KEY(user_id) REFERENCES user(id)
                );
            """);

            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS cart_item (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    cart_id INTEGER NOT NULL,
                    product_id INTEGER NOT NULL,
                    quantity INTEGER NOT NULL,
                    FOREIGN KEY(cart_id) REFERENCES cart(id),
                    FOREIGN KEY(product_id) REFERENCES product(id)
                );
            """);


            ExecuteCommand(connection, """
            CREATE TABLE IF NOT EXISTS Shipper (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                phone TEXT NOT NULL
            );
            """ );

            
            Console.WriteLine("Database migration completed.");
        }

        private static void ExecuteCommand(SqliteConnection connection, string sql)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        public static void Seed()
        {
            using var connection = new SqliteConnection("Data Source=Data/db.db");
            connection.Open();

            // Seed users
            ExecuteCommand(connection, "INSERT INTO user (name) VALUES ('Alice'), ('Bob'), ('Charlie');");

            // Seed products
            ExecuteCommand(connection, "INSERT INTO product (name, price) VALUES ('Laptop', 999.99), ('Smartphone', 499.99), ('Tablet', 299.99);");

            // Seed inventory
            ExecuteCommand(connection, "INSERT INTO inventory (product_id, stock_quantity) VALUES (1, 50), (2, 100), (3, 75);");

            Console.WriteLine("Database seeding completed.");
        }
    }
}

