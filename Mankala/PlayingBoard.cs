using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MankalaProject
{
    public class PlayingBoard
    {
        public Pit[] PitList;
        public int RegularPitAmount;
        public bool HasHomePits;
        public int PitIndex;

        public PlayingBoard(int pits, bool homePits, int startingPebbles)
        {
            HasHomePits = homePits;
            RegularPitAmount = pits;
            if (homePits)
            {
                pits++;
            }
            PitList = new Pit[pits*2];

            for (int i = 0; i < pits; i++)
            {
                PitList[i] = new NormalPit(startingPebbles,1);
                PitList[i+pits] = new NormalPit(startingPebbles,2);
            }

            if (homePits)
            {
                PitList[pits - 1] = new HomePit(1);
                PitList[pits *2 - 1] = new HomePit(2);
            }
        }

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
        
        public void printBoard()
        {
            string firstline = "";
            string secondline = "";
            string thirdline = "";
            int homePitOffset = 0;

            if (this.HasHomePits)
            {
                homePitOffset = 2;
                firstline += "[ ]";
                secondline += "[" + this.PitList.Last().PebbleAmount + "]";
                thirdline += "[ ]";
            }

            for (int i = 0; i < this.RegularPitAmount; i++)
            {
                firstline += "[" + this.PitList[this.RegularPitAmount * 2 -i ].PebbleAmount + "]";
                secondline += "   ";
                thirdline += "[" + this.PitList[i].PebbleAmount + "]";
            }

            if (this.HasHomePits)
            {
                firstline += "[ ]";
                secondline += "[" + this.PitList[RegularPitAmount].PebbleAmount + "]";
                thirdline += "[ ]";
            }

            Console.WriteLine(firstline);
            Console.WriteLine(secondline);
            Console.WriteLine(thirdline);
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
