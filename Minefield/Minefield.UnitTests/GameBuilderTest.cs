using System.Linq;
using Minefield.Game;
using Minefield.Model;
using NUnit.Framework;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class GameBuilderTest
    {
        [Test]
        public void Build_CreatesPlayer()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.IsNotNull(state.Player);
            Assert.IsInstanceOf<PlayerState>(state.Player);
        }

        [TestCase(5, 5, 5)]
        [TestCase(15, 15, 50)]
        [TestCase(10, 26, 100)]
        public void Build_CreatesGrid(int rows, int columns,int mines)
        {
            var builder = new GameBuilder();
            var state = builder.Build(rows, columns, 3, mines);

            Assert.IsNotNull(state.GameGrid);
            Assert.AreEqual(rows,state.GameGrid.Count);
            Assert.AreEqual(columns, state.GameGrid.First().Count);
            Assert.AreEqual(mines, GetMineCount(state));
        }

        private int GetMineCount(IGameState state)
        {
            return state.GameGrid.Sum(row => row.Count(cell => cell.IsMine));
        }

        [Test]
        public void Build_PopulatesMaxLives()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.AreEqual(3, state.MaxLives);
        }

        [Test]
        public void Build_InitializePlayerDeaths()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.AreEqual(0, state.Player.Deaths);
        }

        [Test]
        public void Build_InitializePlayerMoves()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.AreEqual(0, state.Player.Moves);
        }

        [Test]
        public void Build_InitializePlayerColumn()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.AreEqual(0, state.Player.Column);
        }

        [Test]
        public void Build_InitializePlayerRow()
        {
            var builder = new GameBuilder();
            var state = builder.Build(5, 6, 3, 5);

            Assert.AreEqual(2, state.Player.Row);
        }

    }
}