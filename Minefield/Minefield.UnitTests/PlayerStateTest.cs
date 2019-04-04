using System;
using System.Data;
using System.Text;
using Minefield.Model;
using NUnit.Framework;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class PlayerStateTest
    {
        [TestCase(0, 0, "A1")]
        [TestCase(8, 10, "K9")]
        [TestCase(12, 4, "E13")]
        [TestCase(26, 25, "Z27")]
        public void GetChessNotation(int row, int col, string expectedNotation)
        {
            var player = new PlayerState {Row = row, Column = col};

            Assert.AreEqual(expectedNotation,player.GetChessNotation());
        }
    }
}