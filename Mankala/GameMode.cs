namespace MankalaProject
{
    public abstract class GameMode
    {
        public PlayingBoard board;

        public abstract int DoTurn(int player, int startingPit);
        public abstract int DecideWin(int player);

    }

    public class Mankala : GameMode
    {
        //Constructor when no parameters defined, calls version with parameters, with default values
        public Mankala() : this(6, 4) { }

        public Mankala(int pitAmount, int startingPebbles)
        {
            this.board = BoardFactory.CreateMankalaBoard(pitAmount, startingPebbles);

        }

        public override int DoTurn(int player, int startingPit)
        {

            if (startingPit >= board.RegularPitAmount)
            {
                return -player; //-player indicates a move was invalid, and the same player should try a valid move
            }

            if (player == 2) { startingPit = board.RegularPitAmount + 1 + startingPit; }

            Pit currentPit = board.GetFirstPit(startingPit);

            if (currentPit.PebbleAmount == 0)
            {
                return -player;
            }
            var pickedPebbles = currentPit.RemovePebbles();

            while (pickedPebbles > 0)
            {
                currentPit = board.GetNextPit();
                pickedPebbles = currentPit.AddPebble(player, pickedPebbles);

            }

            if (currentPit.Owner == player) //Finish on starting player side
            {
                if (currentPit is HomePit) { return player; }                                                       //Finish in homepit                             Another Turn
                if (currentPit.PebbleAmount > 0) { return DoTurn(player, (board.PitIndex % 7)); }                      //Finish in non-empty,non-Homepit               Another Turn, starting from here
                else if (board.GetOppositePit().PebbleAmount == 0)                                                  //Finish in empty pit, opposite an empty pit    Turn ends
                { return (player % 2) + 1; }
                else
                {                                                                                                   //Finsih in empty pit, opposite an non-empty pit: take opposite pits pebbles, and the last strewn pebble, and adds them to homepit. Turn ends
                    int homePitIndex = (board.RegularPitAmount * player) - 1 + player; //This should always get the homePit for the activePlayer. Maybe make this a function in PlayingBoard?
                    Pit homePit = board.GetFirstPit(homePitIndex);
                    homePit.AddPebble(0, board.GetOppositePit().RemovePebbles());
                    return (player % 2) + 1;
                }
            }
            else  //Finish on opposite side
            {
                return (player % 2) + 1;                                                           //Finish in opponent Pit                Turn ends


            }

        }

        public override int DecideWin(int player)
        {
            var currentPit = board.GetFirstPit(board.RegularPitAmount * (player - 1) + (player - 1));

            for (int i = 0; i < board.RegularPitAmount; i++)
            {
                if (currentPit.PebbleAmount > 0)
                {
                    return -1;
                }
                currentPit = board.GetNextPit();
            }

            int player1Score = board.GetFirstPit(board.RegularPitAmount).PebbleAmount;
            int player2Score = board.GetFirstPit(board.RegularPitAmount * 2 + 1).PebbleAmount;

            if (player1Score > player2Score) { return 1; } //Player1Wins
            if (player1Score < player2Score) { return 2; } //Player2Wins
            return 0; //Tie
        }
    }

    public class Wari : GameMode
    {
        public Wari() : this(6, 4) { }
        public Wari(int pitAmount, int startingPebbles)
        {
            this.board = BoardFactory.CreateWariBoard(pitAmount, startingPebbles);
        }

        public override int DoTurn(int player, int startingPit)
        {
            if (startingPit >= board.RegularPitAmount)
            {
                return -player; //-player indicates a move was invalid, and the same player should try a valid move
            }

            if (player == 2) { startingPit = board.RegularPitAmount + startingPit; }

            Pit currentPit = board.GetFirstPit(startingPit);

            if (currentPit.PebbleAmount == 0)
            {
                return -player;
            }
            var pickedPebbles = currentPit.RemovePebbles();

            while (pickedPebbles > 0)
            {
                currentPit = board.GetNextPit();
                pickedPebbles = currentPit.AddPebble(player, pickedPebbles);
            }

            if (currentPit.Owner != player && (currentPit.PebbleAmount == 2 || currentPit.PebbleAmount == 3))
            {//If the turn ends in an opponents pit, with 2 or 3 pebbles
                pickedPebbles = currentPit.RemovePebbles();
                if (player == 1)
                {
                    board.P1Collection += pickedPebbles;
                }
                else
                {
                    board.P2Collection += pickedPebbles;
                }
            }

            return (player % 2) + 1;

        }

        public override int DecideWin(int player)
        {
            var currentPit = board.GetFirstPit(board.RegularPitAmount * (player - 1));

            for (int i = 0; i < board.RegularPitAmount; i++)
            {
                if (currentPit.PebbleAmount > 0)
                {
                    return -1;
                }
                currentPit = board.GetNextPit();
            }

            if (board.P1Collection > board.P2Collection) { return 1; } //Player1Wins
            if (board.P1Collection < board.P2Collection) { return 2; } //Player2Wins
            return 0; //Tie
        }
    }

    public class Mankari : GameMode
    {
        public Mankari() : this(6, 4) { }
        public Mankari(int pitAmount, int startingPebbles)
        {
            this.board = BoardFactory.CreateMankalaBoard(pitAmount, startingPebbles);
        }

        public override int DoTurn(int player, int startingPit)
        {

            if (startingPit >= board.RegularPitAmount)
            {
                return -player; //-player indicates a move was invalid, and the same player should try a valid move
            }

            if (player == 2) { startingPit = board.RegularPitAmount + startingPit; }

            Pit currentPit = board.GetFirstPit(startingPit);

            if (currentPit.PebbleAmount == 0)
            {
                return -player;
            }
            var pickedPebbles = currentPit.RemovePebbles();

            while (pickedPebbles > 0)
            {
                currentPit = board.GetNextPit();
                pickedPebbles = currentPit.AddPebble(player, pickedPebbles);
            }

            if (currentPit.Owner != player && (currentPit.PebbleAmount == 2 || currentPit.PebbleAmount == 3))
            {//If the turn ends in an opponents pit, with 2 or 3 pebbles
                pickedPebbles = currentPit.RemovePebbles();
                int homePitIndex = (board.RegularPitAmount * player) - 1 + player; //This should always get the homePit for the activePlayer. Maybe make this a function in PlayingBoard?
                Pit homePit = board.GetFirstPit(homePitIndex);
                homePit.AddPebble(player, pickedPebbles);
            }

            return (player % 2) + 1;
        }

        public override int DecideWin(int player)
        {
            var currentPit = board.GetFirstPit(board.RegularPitAmount * (player - 1) + (player - 1));

            for (int i = 0; i < board.RegularPitAmount; i++)
            {
                if (currentPit.PebbleAmount > 0)
                {
                    return -1;
                }
                currentPit = board.GetNextPit();
            }

            int player1Score = board.GetFirstPit(board.RegularPitAmount).PebbleAmount;
            int player2Score = board.GetFirstPit(board.RegularPitAmount * 2 + 1).PebbleAmount;

            if (player1Score > player2Score) { return 1; } //Player1Wins
            if (player1Score < player2Score) { return 2; } //Player2Wins
            return 0; //Tie
        }
    }
}
