using Minefield.Display;
using Minefield.Game;
using Minefield.Inputs;
using Minefield.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace Minefield.UnitTests
{
    [TestFixture]
    public class MinefieldGameTest
    {
        private IGameBuilder _gameBuilder;
        private IGameStateProcessor _gameStateProcessor;
        private IInputManager _inputManager;
        private IDisplayManager _displayManager;
        private IGameState _testGameState;
        private MinefieldGame _game;

        [SetUp]
        public void SetUp()
        {
            _gameBuilder = MockRepository.GenerateMock<IGameBuilder>();
            _gameStateProcessor = MockRepository.GenerateMock<IGameStateProcessor>();
            _inputManager = MockRepository.GenerateMock<IInputManager>();
            _displayManager = MockRepository.GenerateMock<IDisplayManager>();
            _testGameState = MockRepository.GenerateStub<IGameState>();

            _gameBuilder.Stub(x => x.Build(
                Arg<int>.Is.Anything,
                Arg<int>.Is.Anything,
                Arg<int>.Is.Anything,
                Arg<int>.Is.Anything)).Return(_testGameState);

            _game = new MinefieldGame(_gameBuilder, _gameStateProcessor, _inputManager, _displayManager);
        }

        [Test]
        public void RunGame_WhenGameIsWon_CallsDisplayWin()
        {
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            _testGameState.Stub(x => x.HasWon()).Return(true);
            
            _game.RunGame();

            _displayManager.AssertWasCalled(x=>x.DisplayWin(_testGameState));
        }

        [Test]
        public void RunGame_WhenGameIsLost_CallsDisplayGameOver()
        {
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _displayManager.AssertWasCalled(x => x.DisplayGameOver(_testGameState));
        }

        [Test]
        public void RunGame_WhenGameIsStarted_CallsDisplayInstructions()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _displayManager.AssertWasCalled(x => x.DisplayInstructions());
        }

        [Test]
        public void RunGame_WhenGameIsRunning_CallsDisplayGameState()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _displayManager.AssertWasCalled(x => x.DisplayGameState(_testGameState));
        }

        [Test]
        public void RunGame_WhenGameIsRunning_CallsPromptControlGuidance()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _displayManager.AssertWasCalled(x => x.PromptControlGuidance());
        }

        [Test]
        public void RunGame_WhenGameIsRunning_CallsGetDirection()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _inputManager.AssertWasCalled(x => x.GetDirection());
        }

        [Test]
        public void RunGame_WhenGameIsRunning_CallsGetYesNoResponse()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _inputManager.AssertWasCalled(x => x.GetYesNoResponse());
        }

        [Test]
        public void RunGame_WhenGameIsRunning_CallsGameBuilder()
        {
            // Always go left
            _inputManager.Stub(x => x.GetDirection()).Return(PlayerDirection.Right);
            _inputManager.Stub(x => x.GetYesNoResponse()).Return(false);
            // have to ensure a win or loss or it will run infinitely 
            _testGameState.Stub(x => x.HasLost()).Return(true);

            _game.RunGame();

            // Check
            _gameBuilder.AssertWasCalled(x => x.Build(Arg<int>.Is.Anything, 
                Arg<int>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything));
        }

    }
}