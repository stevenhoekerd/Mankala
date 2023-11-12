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
            GameMode mode = new Mankala(6, 0);

            //Act
            var finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(finished, 0);
        }

        [TestMethod()]
        public void DecideWinTestP1Win()
        {
            //Arange
            GameMode mode = new Mankala(6, 0);
            mode.board.PitList[6].AddPebble(1, 1);

            //Act
            var finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(1, finished);


        }
        [TestMethod()]
        public void DecideWinTestP2Win()
        {
            //Arange
            GameMode mode = new Mankala(6, 0);
            mode.board.PitList[13].AddPebble(2, 1);

            //Act
            var finished = mode.DecideWin(2);

            //Assert
            Assert.AreEqual(2, finished);
        }
        [TestMethod()]
        public void DecideWinTestNotFinished()
        {
            //Arange
            GameMode mode = new Mankala(6, 4);

            //Act
            var finished = mode.DecideWin(2);

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
            GameMode mode = new Wari(6, 0);
            mode.board.P1Collection++;

            //Act
            var finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(1, finished);
        }
        [TestMethod()]
        public void DecideWinTestWinP2()
        {
            //Arrange
            GameMode mode = new Wari(6, 0);
            mode.board.P2Collection++;

            //Act
            var finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(2, finished);
        }
        [TestMethod()]
        public void DecideWinTestNotFinished()
        {
            //Arange
            GameMode mode = new Wari();

            //Act
            var finished = mode.DecideWin(2);

            //Assert
            Assert.AreEqual(-1, finished);
        }
        [TestMethod()]
        public void DecideWinTestTie()
        {
            //Arrange
            GameMode mode = new Wari(6, 0);

            //Act
            var finished = mode.DecideWin(1);

            //Assert
            Assert.AreEqual(0, finished);
        }
        [TestMethod()]
        public void TakePebbleTest()
        {

            //Arrange
            GameMode mode = new Wari(1, 1);

            //Act
            mode.DoTurn(1, 0);
            var score = mode.board.P1Collection;

            //Assert
            Assert.AreEqual(2, score);

        }
    }


}