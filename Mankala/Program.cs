using Mankala;

namespace MankalaProject
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string[] gameModes = new string[] { "Mankala" ,"Wari"};
            UIHandler printer = new AsciUI();
            int[] settings = printer.GetSettings(gameModes);

            GameMode gameMode;
            switch (settings[0])
            {
                case 0:
                    if (settings[1] == -1) {
                        gameMode = new Mankala();
                    }
                    else
                    {
                        gameMode= new Mankala(settings[1], settings[2]);
                    }
                    break;
                case 1:
                    if (settings[1] == -1)
                    {
                        gameMode = new Wari();
                    }
                    else
                    {
                        gameMode = new Wari(settings[1], settings[2]);
                    }
                    break;
                default: gameMode = new Mankala();
                    break;
            }

            int player = 1;
            int gameOver = -1;
            int startingPit;
            while (gameOver == -1)
            {
                printer.PrintBoard(gameMode.board);
                startingPit = printer.GetMove(player);
                if (startingPit == -1)
                {   //Go back to the start of the turn, if an invalid move was chosen
                    printer.InvalidMove(player);
                    continue;
                }
                
                
                player = gameMode.DoTurn(player,startingPit);

                if(player < 0)
                {
                    player = -player;
                    //Go back to the start of the turn, if an invalid move was chosen
                    printer.InvalidMove(player);
                    continue;
                }

                gameOver = gameMode.DecideWin(player);
            }

            printer.GameOver(gameOver);

        }

       



       
    }

    




    
}