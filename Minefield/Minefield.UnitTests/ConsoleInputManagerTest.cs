using System.IO;
using Minefield.Inputs;
using Minefield.Model;
using NUnit.Framework;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class ConsoleInputManagerTest
    {
        [TestCase("up", PlayerDirection.Up)]
        [TestCase("UP", PlayerDirection.Up)]
        [TestCase("u", PlayerDirection.Up)]
        [TestCase("down", PlayerDirection.Down)]
        [TestCase("d", PlayerDirection.Down)]
        [TestCase("left", PlayerDirection.Left)]
        [TestCase("l", PlayerDirection.Left)]
        [TestCase("L", PlayerDirection.Left)]
        [TestCase("right", PlayerDirection.Right)]
        [TestCase("r", PlayerDirection.Right)]
        public void GetDirection_PositiveCases(string inputVal, PlayerDirection expectedDirection)
        {
            TextReader testTextReader = new StringReader(inputVal);
            var inputManager = new ConsoleInputManager(testTextReader);
            var directionValue = inputManager.GetDirection();

            Assert.IsTrue(directionValue.HasValue);
            Assert.AreEqual(expectedDirection, directionValue.Value);
        }

        [TestCase("")]
        [TestCase("\t\t\t\t")]
        [TestCase("UpAndLeftABit")]
        public void GetDirection_NegativeCases(string inputValue)
        {
            TextReader testTextReader = new StringReader(inputValue);
            var inputManager = new ConsoleInputManager(testTextReader);
            var directionValue = inputManager.GetDirection();

            Assert.IsFalse(directionValue.HasValue);
        }

        [TestCase("Y")]
        [TestCase("Yes")]
        [TestCase("yes")]
        public void GetYesNoResponse_PositiveCases(string inputValue)
        {
            TextReader testTextReader = new StringReader(inputValue);
            var inputManager = new ConsoleInputManager(testTextReader);

            Assert.IsTrue(inputManager.GetYesNoResponse());
        }

        [TestCase("N")]
        [TestCase("no")]
        [TestCase("\t\t\t\t")]
        [TestCase("UpAndLeftABit")]
        public void GetYesNoResponse_NegativeCases(string inputValue)
        {
            TextReader testTextReader = new StringReader(inputValue);
            var inputManager = new ConsoleInputManager(testTextReader);
            
            Assert.IsFalse(inputManager.GetYesNoResponse());
        }

    }
}
