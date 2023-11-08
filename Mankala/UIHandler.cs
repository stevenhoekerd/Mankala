using MankalaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public abstract class UIHandler
    {
        public abstract void PrintBoard(PlayingBoard board);
        public abstract int GetMove(int player);
        public abstract void InvalidMove(int player);
        public abstract void GameOver(int result);
        public abstract int[] GetSettings(string[] gameModes);
    }

    public class AsciUI : UIHandler
    {
        public override void PrintBoard(PlayingBoard board)
        {
            string firstline = "";
            string secondline = "";
            string thirdline = "";
            int homePitOffset = 0;

            if (board.HasHomePits)
            {
                homePitOffset = 1;
                firstline += "[ ]";
                secondline += "[" + board.PitList.Last().PebbleAmount + "]";
                thirdline += "[ ]";
            }

            for (int i = 0; i < board.RegularPitAmount; i++)
            {
                firstline += "[" + board.PitList[board.PitList.Length - 1 - i - homePitOffset].PebbleAmount + "]";
                secondline += "   ";
                thirdline += "[" + board.PitList[i].PebbleAmount + "]";
            }
            Console.WriteLine("--------------------------------");
            if (board.HasHomePits)
            {
                firstline += "[ ]";
                secondline += "[" + board.PitList[board.RegularPitAmount].PebbleAmount + "]";
                thirdline += "[ ]";
            }
            else
            {
                Console.WriteLine("Player 2 Score: " + board.P2Collection );
            }
            
            Console.WriteLine(firstline);
            Console.WriteLine(secondline);
            Console.WriteLine(thirdline);

            if (!board.HasHomePits)
            {
                Console.WriteLine("Player 1 Score: " + board.P1Collection);
            }
        }

        public override int GetMove(int player)
        {
            Console.WriteLine("choose your move, player" + player);
            int startingPit;
                
            if(int.TryParse(Console.ReadLine(),out startingPit))
            {
                return startingPit;
            }
            else
            {
                return -1;
            }
        }

        public override void InvalidMove(int player)
        {
            Console.WriteLine("That's an invalid move, Player " + player);
        }

        public override void GameOver(int result)
        {
            if (result == 0)
            {
                Console.WriteLine("its a tie!");
            }
            else
            {
                //Tell users who won
                string victory = "player " + result + "has won the Game!";
                Console.WriteLine(victory);
            }
        }
        /// <summary>
        /// Asks the player for what game they want to play, and with what settings
        /// Returns a list which contains, in order, the gamemode, the amount of pits per player, the number of pebbles in each pit.
        /// -1 for the latter 2 settings is returned if the player wishes to use default settings
        /// </summary>
        /// <param name="gameModes"></param>
        /// <returns></returns>
        public override int[] GetSettings(string[] gameModes)
        {
            int[] settings = new int[3];
            Console.WriteLine("What game would you like to play? choose the corresponding number to select a variant");
            for (int i = 0; i < gameModes.Length; i++)
            {
                Console.WriteLine(i + ":" + gameModes[i]);
            }
            bool succes = false;
            int setting = 0;
            //Get the gamemode type, check its a number, and the number is between 0 and the amount of modes
            while (!succes)
            {
                succes = int.TryParse(Console.ReadLine(), out setting);
                if (!succes)
                {
                    Console.WriteLine("Invalid choice! type a number!");
                    continue;
                }
                else if(setting < 0 || setting >= gameModes.Length)
                {
                    Console.WriteLine("Invalid choice! choose a number between 0 and " + gameModes.Length);
                    succes = false;
                    continue;
                }
            }
            settings[0] = setting;
            //Choose between default and custom settings
            succes = false;
            while (!succes)
            {
                Console.WriteLine("Would you like to play with default settings, or with custom settings? type 0 for standard, or 1 for custom");
                succes = int.TryParse(Console.ReadLine(),out setting);
                if (setting < 0 || setting >= 2)
                {
                    Console.WriteLine("Invalid choice! type 0 or 1!");
                    succes = false; 
                    continue;
                }
            }
            if(setting == 0)
            {//-1 Indicates that the factory should use default values for a gamemode
                settings[1] =-1;
                settings[2] =-1;
                return settings;
            }
            //Choose the number of pits
            succes = false;
            while (!succes)
            {
                Console.WriteLine("How many standard pits do you want? choose a number above 0");
                succes = int.TryParse(Console.ReadLine(), out setting);
                if (setting < 0)
                {
                    Console.WriteLine("Invalid choice! type a number above 0!");
                    succes = false;
                    continue;
                }
            }
            settings[1] = setting;
            //Choose the number of starting pebbles
            succes = false;
            while (!succes)
            {
                Console.WriteLine("How many pebbles do you want in each pit, at game start? choose a number above 0");
                succes = int.TryParse(Console.ReadLine(), out setting);
                if (setting < 0)
                {
                    Console.WriteLine("Invalid choice! type a number above 0!");
                    succes = false;
                    continue;
                }
            }
            settings[2] = setting;


            return settings;

        }
    }
}
