using Microsoft.VisualStudio.TestTools.UnitTesting;
using MankalaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MankalaProject.Tests
{
    [TestClass()]
    public class GameModeTests
    {

        [TestMethod()]
        public void DoTurnTestHomePit()
        {
            //Arange
            int player = 1;
            int startingPit = 2;
            GameMode mode = new Mankala();

            player = mode.DoTurn(player, startingPit);


            Assert.AreEqual(1,player);
        }
        [TestMethod()]
        public void DoTurnTestOpponent()
        {
            //Arange
            int player = 1;
            int startingPit = 5;
            GameMode mode = new Mankala();

            player = mode.DoTurn(player, startingPit);


            Assert.AreEqual(2, player);
        }

        [TestMethod()]
        public void DecideWinTestDraw()
        {
            //Arange
            int finished;
            GameMode mode = new Mankala(6, 0);

            //Act

            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(finished,0);
        }

        [TestMethod()]
        public void DecideWinTestP1Win()
        {
            //Arange
            int finished;
            GameMode mode = new Mankala(6, 0);
            mode.board.P1Pits[6].AddPebble(1);

            //Act

            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(finished, 1);
        }
    }
}