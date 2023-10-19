namespace MankalaProject
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            GameMode gameMode = new Mankala(6,4);

            int player = 1;
            int gameOver = -1;
            int startingPit;
            while (gameOver == -1)
            {
                Console.WriteLine("--------------------------------");
                gameMode.board.printBoard();
                Console.WriteLine("choose your move, player" + player);
                startingPit = int.Parse(Console.ReadLine());
                Console.WriteLine("--------------------------------");
                //Obtain startingpit through UI
                player = gameMode.DoTurn(player,startingPit);

                if(player < 0)
                {
                    //Tell user their move is invalid
                    Console.WriteLine("Thats an invalid move! pick a non-homepit with at least 1 pebble");
                    player = -player;
                    continue;
                }

                gameOver = gameMode.DecideWin(player);
            }

            if (gameOver == 0)
            {
                //Tell users its a tie
                Console.WriteLine("its a tie!");
            }
            else
            {
                //Tell users who won
                string victory = "player " + gameOver + "has won the Game!";
                Console.WriteLine(victory);
            }

        }

       



       
    }

    




    
}