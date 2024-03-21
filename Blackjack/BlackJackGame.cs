using Blackjack;

var game = new GameProcess();

var date  = DateTime.Now;

sqLiteDB.CreateTable();
game.GameMenu();



