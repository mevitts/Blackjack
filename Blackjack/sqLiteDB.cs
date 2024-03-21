using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Globalization;

namespace Blackjack
{
    internal class sqLiteDB
    {
        public static string connectionString;

        static sqLiteDB()
        {
            connectionString = @"Data Source=C:\Users\marte\OneDrive\Desktop\Nueva_carpeta\c#\Blackjack\Blackjack.db";
        }

        public static void Connect()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Perform database operations using 'connection'
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to SQLite database: {ex.Message}");
                }
            }
        }
        public static void CreateTable()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS blackjack_result (
                 Game INTEGER PRIMARY KEY AUTOINCREMENT,
                 Date TEXT,
                 W_L TEXT,
                 RESULT TEXT,
                 WAGER INTEGER)";
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating table: {ex.Message}");
                }
            }
        }
        public static void AddHand(Transaction transaction)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    string wl = helpers.WinOrLoss(transaction.GameResult);

                    tableCmd.CommandText =
                        $@"INSERT INTO blackjack_result(date, w_l, result, wager) 
                    VALUES(@Date, @WL, @Result, @Amount)";
                    tableCmd.Parameters.AddWithValue("@Date", transaction.Date);
                    tableCmd.Parameters.AddWithValue("@WL", wl);
                    tableCmd.Parameters.AddWithValue("@Result", transaction.GameResult);
                    tableCmd.Parameters.AddWithValue("@Amount", transaction.Amount);

                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding hand: {ex.Message}");
                }
            }
        }

        public static void GetAllRecords()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    string query = $"SELECT * FROM blackjack_result";

                    tableCmd.CommandText = query;

                    List<TableEntry> tableData = new();
                    using (SqliteDataReader reader = tableCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Process reader data
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving records: {ex.Message}");
                }
            }
            
        }
    }

    //have to create completely new class to add to table because has a buch of differnt types
    public class TableEntry
    {
        public int Id { get; set; }
        public string W_L { get; set; }
        internal Transaction transaction { get; set; }
    }

}
