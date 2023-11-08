using System;
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
            return new MankalaBoard(pitAmount, startingPebbles);
        }

        public static PlayingBoard CreateWariBoard(int pitAmount, int startingPebbles)
        {
            return new WariBoard(pitAmount, startingPebbles);
        }
    }
    public class PlayingBoard
    {
        public bool HasHomePits;
        public Pit[] PitList;
        public int RegularPitAmount;
        public int PitIndex;

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

        public Pit GetOppositePit()
        {
            return PitList[PitList.Length - PitIndex -2];
        }
        
    }

    public class MankalaBoard : PlayingBoard
    {
        public MankalaBoard(int pits, int startingPebbles)
        {
            HasHomePits = true;
            RegularPitAmount = pits;
            //add another pit, for the homepit
            pits++;
            PitList = new Pit[pits * 2];

            for (int i = 0; i < pits; i++)
            {
                PitList[i] = new NormalPit(startingPebbles, 1);
                PitList[i + pits] = new NormalPit(startingPebbles, 2);
            }

            PitList[pits - 1] = new HomePit(1);
            PitList[pits * 2 - 1] = new HomePit(2);
        }
    }

    public class WariBoard : PlayingBoard
    {
        int P1Collection = 0;
        int P2Collection = 0;
        public WariBoard(int pits, int startingPebbles)
        {
            HasHomePits = false;
            RegularPitAmount= pits;
            PitList = new Pit[pits*2];
            for (int i = 0; i < pits; i++)
            {
                PitList[i] = new NormalPit(startingPebbles, 1);
                PitList[i + pits] = new NormalPit(startingPebbles, 2);
            }
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
