using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minefield.Display;
using Minefield.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class ConsoleDisplayManagerTest
    {
        [Test]
        public void DisplayInstructions_WritesInstructions()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            displayManager.DisplayInstructions();

            var instructionString = builder.ToString();
            Assert.IsNotEmpty(instructionString);
            Assert.IsTrue(instructionString.Contains("How To Play"));
            Assert.IsTrue(instructionString.Contains("P = Player"));
            Assert.IsTrue(instructionString.Contains("X = A Very Unfortunate Player"));
            Assert.IsTrue(instructionString.Contains("* = Mine"));
            Assert.IsTrue(instructionString.Contains("# = Fog"));

        }

        [Test]
        public void DisplayWin_WritesScore()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            var state = MockRepository.GenerateMock<IGameState>();
            state.Stub(x=>x.CalcScore()).Return(21);
            displayManager.DisplayWin(state);

            var winMessage = builder.ToString();
            Assert.IsTrue(winMessage.Contains("Congratulations, you crossed the battlefield!"));
            Assert.IsTrue(winMessage.Contains("You scored 21 points."));

        }

        [Test]
        public void DisplayGameOver_WritesScore()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            var state = MockRepository.GenerateMock<IGameState>();
            state.Stub(x => x.CalcScore()).Return(6);
            displayManager.DisplayGameOver(state);

            var gameOverMessage = builder.ToString();
            Assert.IsTrue(gameOverMessage.Contains("Medical Science has limits Soldier!"));
            Assert.IsTrue(gameOverMessage.Contains("You scored 6 points."));

        }

        [Test]
        public void PromptPlayAgain_WritesScore()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            displayManager.PromptPlayAgain();

            var playAgainMessage = builder.ToString();
            Assert.IsTrue(playAgainMessage.Contains("Would you like to play again (yes/no)?"));

        }


        [Test]
        public void PromptWaitAnyInput_WritesScore()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            displayManager.PromptWaitAnyInput();

            var waitForAnyMessage = builder.ToString();
            Assert.IsTrue(waitForAnyMessage.Contains("Press <Enter> to Continue."));

        }

        [Test]
        public void DisplayGameState_WritesPlayerStatus()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 6;
            const int maxCols = 5;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);

            var player = new PlayerState {Row = 1, Column = 2, Deaths = 1, Moves = 7};
            var state = new GameState(player, gridList, 3, maxRows, maxCols);


            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains(string.Format("Position: {0}, Lives: {1}, Score {2}", "C2", 2, 6)));

        }

        [Test]
        public void DisplayGameState_WritesColumnHeadings()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 5;
            const int maxCols = 13;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);

            var player = new PlayerState { Row = 0, Column = 0, Deaths = 1, Moves = 2 };
            var state = new GameState(player, gridList, 3, maxRows, maxCols);
            
            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains("ABCDEFGHIJKLM"));

        }

        [Test]
        public void DisplayGameState_WritesInitialGameGrid()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 5;
            const int maxCols = 5;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);

            var player = new PlayerState { Row = 2, Column = 0, Deaths = 1, Moves = 2 };
            var state = new GameState(player, gridList, 3, maxRows, maxCols);

            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains("001 #####"));
            Assert.IsTrue(gameDisplay.Contains("002 #####"));
            Assert.IsTrue(gameDisplay.Contains("003 P####"));
            Assert.IsTrue(gameDisplay.Contains("004 #####"));
            Assert.IsTrue(gameDisplay.Contains("005 #####"));

        }

        [Test]
        public void DisplayGameState_WritesDeadPlayerGrid()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 5;
            const int maxCols = 5;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);

            gridList[2][1].IsMine = true;
            var player = new PlayerState { Row = 2, Column = 1, Deaths = 0, Moves = 0 };
            var state = new GameState(player, gridList, 3, maxRows, maxCols);

            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains("001 #####"));
            Assert.IsTrue(gameDisplay.Contains("002 #####"));
            Assert.IsTrue(gameDisplay.Contains("003 #X###"));
            Assert.IsTrue(gameDisplay.Contains("004 #####"));
            Assert.IsTrue(gameDisplay.Contains("005 #####"));

        }

        [Test]
        public void DisplayGameState_WritesUncoveredGrid()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 5;
            const int maxCols = 5;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);
            gridList[2][0].IsUncovered = true;
            gridList[2][1].IsUncovered = true;
            var player = new PlayerState { Row = 0, Column = 0, Deaths = 0, Moves = 0 };
            var state = new GameState(player, gridList, 3, maxRows, maxCols);

            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains("001 P####"));
            Assert.IsTrue(gameDisplay.Contains("002 #####"));
            Assert.IsTrue(gameDisplay.Contains("003   ###"));
            Assert.IsTrue(gameDisplay.Contains("004 #####"));
            Assert.IsTrue(gameDisplay.Contains("005 #####"));

        }

        [Test]
        public void DisplayGameState_WritesUncoveredMinesGrid()
        {
            var builder = new StringBuilder();
            TextWriter testTextWriter = new StringWriter(builder);
            var displayManager = new ConsoleDisplayManager(testTextWriter);

            const int maxRows = 5;
            const int maxCols = 5;

            IList<IList<Cell>> gridList = TestUtils.MakeEmptyGrid(maxRows, maxCols);

            gridList[2][0].IsUncovered = true;
            gridList[2][1].IsUncovered = true;
            gridList[2][0].IsMine = true;
            gridList[2][1].IsMine = true;
            var player = new PlayerState { Row = 0, Column = 0, Deaths = 0, Moves = 0 };
            var state = new GameState(player, gridList, 3, maxRows, maxCols);

            displayManager.DisplayGameState(state);

            var gameDisplay = builder.ToString();
            Assert.IsTrue(gameDisplay.Contains("001 P####"));
            Assert.IsTrue(gameDisplay.Contains("002 #####"));
            Assert.IsTrue(gameDisplay.Contains("003 **###"));
            Assert.IsTrue(gameDisplay.Contains("004 #####"));
            Assert.IsTrue(gameDisplay.Contains("005 #####"));

        }

    }
}
