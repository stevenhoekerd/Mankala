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
    public class MankalaTests
    {

        [TestMethod()]
        public void DoTurnTestHomePit()
        {
            //Arange
            int player = 1;
            int startingPit = 2;
            GameMode mode = new Mankala();

            player = mode.DoTurn(player, startingPit);


            Assert.AreEqual(1, player);
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
            Assert.AreEqual(finished, 0);
        }

        [TestMethod()]
        public void DecideWinTestP1Win()
        {
            //Arange
            int finished;
            GameMode mode = new Mankala(6, 0);
            mode.board.PitList[6].AddPebble(1, 1);

            //Act

            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(1, finished);


        }
        [TestMethod()]
        public void DecideWinTestP2Win()
        {
            //Arange
            int finished;
            GameMode mode = new Mankala(6, 0);
            mode.board.PitList[13].AddPebble(2, 1);

            //Act

            finished = mode.DecideWin(2);

            //Assert
            Assert.AreEqual(2, finished);
        }
        [TestMethod()]
        public void DecideWinTestNotFinished()
        {
            //Arange
            int finished;
            GameMode mode = new Mankala(6, 4);

            //Act

            finished = mode.DecideWin(2);

            //Assert
            Assert.AreEqual(-1, finished);
        }
    }

    [TestClass()]
    public class WariTests
    {
        [TestMethod()]
        public void DecideWinTestWinP1() 
        {
            //Arrange
            int finished;
            GameMode mode = new Wari(6, 0);
            mode.board.P1Collection++;

            //Act
            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(1,finished);
        }
        [TestMethod()]
        public void DecideWinTestWinP2()
        {
            //Arrange
            int finished;
            GameMode mode = new Wari(6, 0);
            mode.board.P2Collection++;

            //Act
            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(2, finished);
        }
        [TestMethod()]
        public void DecideWinTestNotFinished()
        {
            //Arange
            int finished;
            GameMode mode = new Wari();

            //Act

            finished = mode.DecideWin(2);

            //Assert
            Assert.AreEqual(-1, finished);
        }
        [TestMethod()]
        public void DecideWinTestTie()
        {
            //Arrange
            int finished;
            GameMode mode = new Wari(6, 0);

            //Act
            finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(0, finished);
        }
        [TestMethod()]
        public void TakePebbleTest()
        {

            //Arrange
            int score;
            GameMode mode = new Wari(1, 1);

            //Act
            mode.DoTurn(1, 0);
            score = mode.board.P1Collection;

            //Assert
            Assert.AreEqual(2, score);

        }
    }


}