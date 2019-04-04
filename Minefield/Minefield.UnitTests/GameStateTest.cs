using System.Collections.Generic;
using System.Linq;
using Minefield.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace Minefield.UnitTests
{
    public class GameStateTest
    {
        [Test]
        public void PlayerIsStandingOnMine_ReturnsTrue()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5,5), 5, 5,5);

            state.Player.Column = 3;
            state.Player.Row =1;
            state.GameGrid[1][3].IsMine = true;

            Assert.IsTrue(state.PlayerIsStandingOnMine());
        }

        [Test]
        public void PlayerIsStandingOnMine_ReturnsFalse()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            state.Player.Column = 3;
            state.Player.Row = 1;

            Assert.IsFalse(state.PlayerIsStandingOnMine());
        }

        [Test]
        public void HasLost_ReturnsFalse()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            state.Player.Deaths = 4;

            Assert.IsFalse(state.HasLost());
        }

        [Test]
        public void HasLost_ReturnsTrue()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            state.Player.Deaths = 5;

            Assert.IsTrue(state.HasLost());
        }

        [Test]
        public void HasWon_ReturnsTrue()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            state.Player.Column = 4;

            Assert.IsTrue(state.HasWon());
        }

        [Test]
        public void HasWon_ReturnsFalse()
        {
            var state = new GameState(new PlayerState(), TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            state.Player.Column = 3;

            Assert.IsFalse(state.HasWon());
        }

        [TestCase(10, 2, 8)]
        [TestCase(2, 2, 0)]
        public void CalcScore(int moves, int deaths, int expectedScore)
        {
            var player = new PlayerState {Moves = moves, Deaths = deaths};
            var state = new GameState(player, TestUtils.MakeEmptyGrid(5, 5), 5, 5, 5);

            Assert.AreEqual(expectedScore, state.CalcScore());
        }

        [TestCase(2, 5, 3)]
        [TestCase(5, 5, 0)]
        public void GetLivesRemaining(int deaths, int maxLives, int expectedLives)
        {
            var player = new PlayerState { Deaths = deaths };
            var state = new GameState(player, TestUtils.MakeEmptyGrid(5, 5), maxLives, 5, 5);

            Assert.AreEqual(expectedLives, state.GetLivesRemaining());
        }


    }
}