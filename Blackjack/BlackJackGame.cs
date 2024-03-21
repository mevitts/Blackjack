using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {

            var game = new GameProcess();
            var date = DateTime.Now;

            sqLiteDB.CreateTable();          
            game.GameMenu();
        }
    }
}







