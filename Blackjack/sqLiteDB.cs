using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Globalization;

namespace Blackjack
{
    internal class sqLiteDB
    {
        private static string connectionString;

        public sqLiteDB()
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

                    tableCmd.CommandText =
                        @"CREATE TABLE IF NOT EXISTS blackjack_result (
                        Game INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        W/L TEXT,
                        RESULT TEXT,
                        WAGER INTEGER
                    )";

                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating table: {ex.Message}");
                }
            }
        }
        internal static void AddHand(Transaction transaction)
        {
            Connect();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    var tableCmd = connection.CreateCommand();
                    string wl = helpers.WinOrLoss(transaction.GameResult);

                    //"parameterized query to prevent sql injection"
                    tableCmd.CommandText =
                        $@"INSERT INTO blackjack_result(date, w/l, result, wager) 
                    VALUES(@Date, @WL, @Result, @Amount)";
                    tableCmd.Parameters.AddWithValue("@Date", transaction.Date);
                    tableCmd.Parameters.AddWithValue("@WL", wl);
                    tableCmd.Parameters.AddWithValue("@Result", transaction.GameResult);
                    tableCmd.Parameters.AddWithValue("@Amount", transaction.Amount);

                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex) { Console.WriteLine("$Error adding hand: {ex.Message}"); }

                finally
                {
                    connection.Close();
                }
            }
        }
        internal static void GetAllRecords()
        {
            Console.Clear();
            Connect();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var tableCmd = connection.CreateCommand();
                string query = $"SELECT * FROM blackjack_result";

                tableCmd.CommandText = query;

                List<TableEntry> tableData = new();
                SqliteDataReader reader = tableCmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //get values from reaeder
                        int id = reader.GetInt32(0);
                        string w_l = reader.GetString(2);
                        Transaction transaction = new Transaction
                        {
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            GameResult = (Result)Enum.Parse(typeof(Result), reader.GetString(3)),
                            Amount = reader.GetInt32(4)
                        };

                        //then add instead of all at once
                        tableData.Add(new TableEntry
                        {
                            Id = id,
                            W_L = w_l,
                            transaction = transaction
                        });
                    }
                }
                else { Console.WriteLine("No rows found"); }
                connection.Close();

                Console.WriteLine("-------------------------------------------------------------\n");
                foreach (var bj in tableData)
                {
                    Console.WriteLine($"{bj.Id} - {bj.transaction.Date.ToString("dd-MMM-yyyy")} - {bj.W_L} - Result: {bj.transaction.GameResult.ToString()} - Amount Wagered: {bj.transaction.Amount.ToString()}");
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
