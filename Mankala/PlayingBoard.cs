using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MankalaProject
{
    public class PlayingBoard
    {
        public Pit[] P1Pits;
        public Pit[] P2Pits;
        public Pit[] PitList;
        public int RegularPitAmount;
        public bool HasHomePits;

        public PlayingBoard(int pits, bool homePits, int startingPebbles)
        {
            HasHomePits = homePits;
            RegularPitAmount = pits;
            if (homePits)
            {
                pits++;
            }
            P1Pits = new Pit[pits];
            P2Pits = new Pit[pits];
            PitList = new Pit[pits*2];

            for (int i = 0; i < pits; i++)
            {
                P1Pits[i] = new NormalPit(startingPebbles);
                P2Pits[i+pits] = new NormalPit(startingPebbles);
            }

            if (homePits)
            {
                P1Pits[pits - 1] = new HomePit();
                P2Pits[pits - 1] = new HomePit();
            }
        
        }

        public void printBoard()
        {
            string firstline = "";
            string secondline = "";
            string thirdline = "";

            if (this.HasHomePits)
            {
                firstline += "[ ]";
                secondline += "[" + this.P2Pits.Last().PebbleAmount + "]";
                thirdline += "[ ]";
            }

            for (int i = 0; this.RegularPitAmount > i; i++)
            {
                firstline += "[" + this.P2Pits[this.RegularPitAmount - i - 1].PebbleAmount + "]";
                secondline += "   ";
                thirdline += "[" + this.P1Pits[i].PebbleAmount + "]";
            }

            if (this.HasHomePits)
            {
                firstline += "[ ]";
                secondline += "[" + this.P1Pits.Last().PebbleAmount + "]";
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

        abstract public int RemovePebbles();
        abstract public void AddPebble(int amount);
    }

    class HomePit : Pit
    {
        public override int RemovePebbles()
        {
            //This should never be called, i think?
            int previousPebbles = PebbleAmount; 
            this.PebbleAmount = 0;
            return previousPebbles;
        }
        public override void AddPebble(int amount)
        {
            this.PebbleAmount += amount;
        }

    }

    class NormalPit : Pit
    {
        public NormalPit(int initalPebbles)
        {
            PebbleAmount = initalPebbles;
        }
        public override int RemovePebbles()
        {
            int previousPebbles = PebbleAmount;
            this.PebbleAmount = 0;
            return previousPebbles;
        }

        public override void AddPebble(int amount)
        {
            this.PebbleAmount+=amount;
        }

    }


}
