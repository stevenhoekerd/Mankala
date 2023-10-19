using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MankalaProject
{
    abstract public class GameMode
    {
        public PlayingBoard board;

        abstract public void CreatePlayingBoard(int pitAmount, int startingPebbles);
        abstract public int DoTurn(int player, int startingPit);
        abstract public int DecideWin(int player);

    }

    public class Mankala : GameMode
    {
        //Constructor when no parameters defined, calls version with parameters, with default values
        public Mankala() : this(6, 4) { }

        public Mankala(int pitAmount, int startingPebbles)
        {
            CreatePlayingBoard(pitAmount, startingPebbles);

        }


        public override void CreatePlayingBoard(int pitAmount, int startingPebbles)
        {
            this.board = new PlayingBoard(pitAmount, true, startingPebbles);
        }

        public override int DoTurn(int player, int startingPit)
        {
            
            if (startingPit >= board.RegularPitAmount)
            {
                return -player; //-player indicates a move was invalid, and the same player should try a valid move
            }

            int pickedPebbles;
            int activePit = startingPit;
            Pit[] activeList; Pit[] startingList; Pit[] opponentList;

            if (player == 1)
            {
                activeList = board.P1Pits;
                startingList = board.P1Pits;
                opponentList = board.P2Pits;
            }
            else
            {
                activeList = board.P2Pits;
                startingList = board.P2Pits;
                opponentList = board.P1Pits;
            }
            if (activeList[activePit].PebbleAmount == 0)
            {
                return -player;
            }
            pickedPebbles = activeList[startingPit].RemovePebbles();

            while (pickedPebbles > 0)
            {
                activePit++;
                //If we have run over this entire pitlist, go to the other side. take into account we should skip opponent homepit.
                {
                    if ((activePit >= activeList.Length && activeList == startingList) || (activePit >= activeList.Length -1 && activeList == opponentList))
                    {
                        if (activeList == board.P1Pits)
                        {
                            activeList = board.P2Pits;
                        }
                        else
                        {
                            activeList = board.P1Pits;
                        }
                        activePit = 0;
                    }
                }
                activeList[activePit].AddPebble(1);
                pickedPebbles--;
            }

            if (startingList == activeList) //Finish on starting player side
            {
                if (activePit == board.RegularPitAmount) { return player; }                     //Finish in homepit                     Another Turn
                if (activeList[activePit].PebbleAmount > 0) 
                {
                    return DoTurn(player, activePit); }   //Finish in non-empty,non-Homepit       Another Turn, starting from here
                else if (opponentList[(board.RegularPitAmount) - 1 - activePit].PebbleAmount == 0)
                { return (player % 2) + 1; }                                                        //Finish in empty pit, opposite an empty pit    Turn ends
                else
                {                                                                                   //Finsih in empty pit, opposite an non-empty pit: take opposite pits pebbles, and the last strewn pebble, and adds them to homepit. Turn ends
                    activeList[activePit].AddPebble(-1);
                    startingList[board.RegularPitAmount].AddPebble(opponentList[(board.RegularPitAmount) - 1 - activePit].RemovePebbles() + 1);
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
            Pit[] activeList;
            if (player == 1) { activeList = board.P1Pits; }
            else { activeList = board.P2Pits; }

            for (int i = 0; i < activeList.Length - 1; i++) //Length -1 because homepit isnt a valid move
            {
                if (activeList[i].PebbleAmount > 0)
                {
                    return -1;  //If the active player still has a non-empty pit, the game isnt over
                }
            }

            int player1Score = board.P1Pits.Last().PebbleAmount;
            int player2Score = board.P2Pits.Last().PebbleAmount;

            if (player1Score > player2Score) { return 1; } //Player1Wins
            if (player1Score < player2Score) { return 2; } //Player2Wins
            return 0; //Tie
        }
    }
}
