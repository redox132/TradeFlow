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

            // USERS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    email TEXT NOT NULL UNIQUE,
                    name TEXT NOT NULL,
                    password TEXT NOT NULL,
                    x_api_key TEXT UNIQUE
                );
            """);

            // PRODUCTS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS products (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    name TEXT NOT NULL,
                    catalog_number TEXT,
                    ean TEXT,
                    symbol TEXT,
                    location TEXT,
                    stock INTEGER NOT NULL DEFAULT 0,
                    price REAL NOT NULL,
                    is_active INTEGER NOT NULL DEFAULT 1,
                    FOREIGN KEY(user_id) REFERENCES users(id) ON DELETE CASCADE
                );
            """);

            // VARIANTS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS product_variants (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    product_id INTEGER NOT NULL,
                    catalog_number TEXT,
                    ean TEXT,
                    symbol TEXT,
                    FOREIGN KEY(product_id) REFERENCES products(id) ON DELETE CASCADE
                );
            """);

            // ORDERS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS orders (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    currency TEXT NOT NULL DEFAULT 'pln',
                    payment_status TEXT NOT NULL DEFAULT 'pending',
                    paid REAL DEFAULT 0,
                    status INTEGER NOT NULL DEFAULT 1,
                    email TEXT,
                    date TEXT DEFAULT CURRENT_TIMESTAMP,
                    shipment_price REAL DEFAULT 0,
                    comment TEXT,
                    invoice INTEGER DEFAULT 0,
                    document_number TEXT,
                    bill_address_id INTEGER,
                    ship_address_id INTEGER,
                    pickup_point_json TEXT,
                    FOREIGN KEY(user_id) REFERENCES users(id),
                    FOREIGN KEY(bill_address_id) REFERENCES addresses(id),
                    FOREIGN KEY(ship_address_id) REFERENCES addresses(id)
                );
            """);

            // ORDER ITEMS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS order_items (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    order_id INTEGER NOT NULL,
                    product_id INTEGER,
                    variant_id INTEGER,
                    quantity INTEGER NOT NULL,
                    price REAL NOT NULL,
                    tax REAL,
                    catalog_number TEXT,
                    ean TEXT,
                    name TEXT,
                    weight REAL,
                    additional_information TEXT,
                    FOREIGN KEY(order_id) REFERENCES orders(id) ON DELETE CASCADE,
                    FOREIGN KEY(product_id) REFERENCES products(id),
                    FOREIGN KEY(variant_id) REFERENCES product_variants(id)
                );
            """);

            // ADDRESSES
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS addresses (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    name TEXT,
                    surname TEXT,
                    street TEXT NOT NULL,
                    home_number TEXT,
                    flat_number TEXT,
                    description TEXT,
                    postcode TEXT,
                    city TEXT NOT NULL,
                    state TEXT,
                    phone TEXT,
                    company_name TEXT,
                    company_nip TEXT,
                    country_id INTEGER,
                    FOREIGN KEY(user_id) REFERENCES users(id),
                    FOREIGN KEY(country_id) REFERENCES supported_countries(id)
                );
            """);

            // SUPPORTED COUNTRIES
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS supported_countries (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    code TEXT NOT NULL UNIQUE,
                    name TEXT NOT NULL,
                    is_active INTEGER NOT NULL DEFAULT 1
                );
            """);

            // CARRIERS
            ExecuteCommand(connection, """
                CREATE TABLE IF NOT EXISTS carriers (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    name TEXT NOT NULL,
                    code TEXT NOT NULL,
                    tracking_url TEXT,
                    supports_tracking INTEGER DEFAULT 1,
                    supports_international INTEGER DEFAULT 0,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    updated_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY(user_id) REFERENCES users(id) ON DELETE CASCADE
                );
            """);

            Console.WriteLine("Database migration completed.");
        }

        private static void ExecuteCommand(SqliteConnection connection, string sql)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}
