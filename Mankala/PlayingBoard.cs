using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MankalaProject
{
    public static class BoardFactory
    {
        public static PlayingBoard CreateMankalaBoard(int pitAmount, int startingPebbles)
        {
            MankalaBoard board = new MankalaBoard();
            board.RegularPitAmount = pitAmount;
            pitAmount++;
            board.PitList = new Pit[pitAmount * 2];

            for (int i = 0; i < pitAmount; i++)
            {
                board.PitList[i] = new NormalPit(startingPebbles, 1);
                board.PitList[i + pitAmount] = new NormalPit(startingPebbles, 2);
            }

            board.PitList[pitAmount - 1] = new HomePit(1);
            board.PitList[pitAmount * 2 - 1] = new HomePit(2);

            return board;
        }

        public static PlayingBoard CreateWariBoard(int pitAmount, int startingPebbles)
        {
            WariBoard board = new WariBoard();
            board.RegularPitAmount = pitAmount;
            board.PitList = new Pit[pitAmount * 2];

            for (int i = 0; i < pitAmount; i++)
            {
                board.PitList[i] = new NormalPit(startingPebbles, 1);
                board.PitList[i + pitAmount] = new NormalPit(startingPebbles, 2);
            }

            return board;
        }
    }
    public class PlayingBoard
    {
        public bool HasHomePits;
        public Pit[] PitList;
        public int RegularPitAmount;
        public int PitIndex;
        public int P1Collection = 0;
        public int P2Collection = 0;

        public Pit GetFirstPit(int pitIndex) 
        {
            PitIndex = pitIndex;
            return PitList[PitIndex]; 
        }
        public Pit GetNextPit()
        {
            PitIndex++;
            PitIndex %= PitList.Length;
            return PitList[PitIndex];
        }
        //Default case is for a board without homePits, override for boards with.
        public virtual Pit GetOppositePit()
        {
            return PitList[PitList.Length - 1 - PitIndex];
        }
        
    }

    public class MankalaBoard : PlayingBoard
    {
        public MankalaBoard()
        {
            HasHomePits = true;
        }
        public override Pit GetOppositePit()
        {
            return PitList[PitList.Length - 2 - PitIndex];
        }

    }

    public class WariBoard : PlayingBoard
    {
        
        public WariBoard()
        {
            HasHomePits = false;
            
        }
    }

    abstract public class Pit
    {
        public int PebbleAmount = 0;
        public int Owner;

        abstract public int RemovePebbles();
        abstract public int AddPebble(int player, int amount);
    }

    class HomePit : Pit
    {
        public HomePit(int owner)
        {
            Owner = owner;
        }

        public override int RemovePebbles()
        {
            //This should never be called, i think?
            int previousPebbles = PebbleAmount; 
            this.PebbleAmount = 0;
            return previousPebbles;
        }
        public override int AddPebble(int player, int amount)
        {
            if(player == 0)
            {   //Use this if a large amount of pebbles has to be added, due to game-specific rules
                this.PebbleAmount += amount;
                return 0;
            }
            else if(Owner  == player)
            {
                this.PebbleAmount++;
                return amount-1;
            }
            else
            {
                return amount;
            }
            
        }

    }

    class NormalPit : Pit
    {
        public NormalPit(int initalPebbles, int player)
        {
            Owner = player;
            PebbleAmount = initalPebbles;
        }
        public override int RemovePebbles()
        {
            int previousPebbles = PebbleAmount;
            this.PebbleAmount = 0;
            return previousPebbles;
        }

        public override int AddPebble(int player, int amount)
        {
            this.PebbleAmount++;
            return amount-1;
        }

    }


}
