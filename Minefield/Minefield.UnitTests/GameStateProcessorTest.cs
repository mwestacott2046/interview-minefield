using System.Collections.Generic;
using Minefield.Game;
using Minefield.Model;
using NUnit.Framework;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class GameStateProcessorTest
    {
        [Test]
        public void PlayStep_WhenPlayerIsMoved_IncreasesPlayerMovesCounter()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState {Column = 0, Row = 0, Moves = 0};

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5,maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Right);

            Assert.AreEqual(1, player.Moves);
        }

        [Test]
        public void PlayStep_WhenPlayerIsMoved_PreviousPositionIsUncovered()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 0, Row = 0, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);
            state.GameGrid[0][0].IsUncovered = false;

            processor.PlayStep(state, PlayerDirection.Right);

            Assert.IsTrue(state.GameGrid[0][0].IsUncovered);
        }

        [Test]
        public void PlayStep_PlayerCanMoveLeft()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 1, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Left);

            Assert.AreEqual(0, player.Column);
        }

        [Test]
        public void PlayStep_PlayerCanMoveRight()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 1, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Right);

            Assert.AreEqual(2, player.Column);
        }

        [Test]
        public void PlayStep_PlayerCanMoveUp()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 1, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Up);

            Assert.AreEqual(0, player.Row);
        }

        [Test]
        public void PlayStep_PlayerCanMoveDown()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 1, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Down);

            Assert.AreEqual(2, player.Row);
        }

        [Test]
        public void PlayStep_WhenPlayerAtTopOfGrid_PlayerDoesNotMoveUp()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 2, Row = 0, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Up);

            Assert.AreEqual(0, player.Row);
        }

        [Test]
        public void PlayStep_WhenPlayerAtBottomOfGrid_PlayerDoesNotMoveDown()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 2, Row = maxRows-1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Down);

            Assert.AreEqual(maxRows - 1, player.Row);
        }

        [Test]
        public void PlayStep_WhenPlayerAtLeftmostOfGrid_PlayerDoesNotMoveLeft()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = 0, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Left);

            Assert.AreEqual(0, player.Column);
        }

        [Test]
        public void PlayStep_WhenPlayerAtRightmostOfGrid_PlayerDoesNotMoveRight()
        {
            const int maxRows = 4;
            const int maxColumns = 4;

            var processor = new GameStateProcessor();
            var player = new PlayerState { Column = maxColumns-1, Row = 1, Moves = 0 };

            IList<IList<Cell>> grid = TestUtils.MakeEmptyGrid(maxRows, maxColumns);
            IGameState state = new GameState(player, grid, 5, maxRows, maxColumns);

            processor.PlayStep(state, PlayerDirection.Right);

            Assert.AreEqual(maxColumns-1, player.Column);
        }

    }
}