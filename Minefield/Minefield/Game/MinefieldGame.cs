using Minefield.Display;
using Minefield.Inputs;
using Minefield.Model;

namespace Minefield.Game
{
    public class MinefieldGame : IMinefieldGame
    {
        private readonly IGameBuilder _gameBuilder;
        private readonly IGameStateProcessor _gameStateProcessor;
        private readonly IInputManager _inputManager;
        private readonly IDisplayManager _displayManager;

        private const int MAX_GRID_ROWS = 12;
        private const int MAX_GRID_COLUMNS = 18;
        private const int MAX_PLAYER_LIVES = 5;
        private const int MAX_MINES = 30;

        public MinefieldGame(IGameBuilder gameBuilder, IGameStateProcessor gameStateProcessor, 
                            IInputManager inputManager, IDisplayManager displayManager)
        {
            _gameBuilder = gameBuilder;
            _gameStateProcessor = gameStateProcessor;
            _inputManager = inputManager;
            _displayManager = displayManager;
        }

        public void RunGame()
        {
            var keepPlaying = true;

            _displayManager.DisplayInstructions();
            _displayManager.PromptWaitAnyInput();
            _inputManager.WaitForAnyInput();

            while (keepPlaying)
            {
                var gameState = _gameBuilder.Build(MAX_GRID_ROWS, MAX_GRID_COLUMNS, MAX_PLAYER_LIVES, MAX_MINES);
                _displayManager.DisplayGameState(gameState);

                var gameEnded = false;
                while (!gameEnded)
                {
                    _displayManager.PromptControlGuidance();
                    var playerDirection = EnsurePlayerDirection();
                    _gameStateProcessor.PlayStep(gameState, playerDirection);
                    _displayManager.DisplayGameState(gameState);

                    if (gameState.HasWon())
                    {
                        gameEnded = true;
                        _displayManager.DisplayWin(gameState);
                    }

                    if (gameState.HasLost())
                    {
                        gameEnded = true;
                        _displayManager.DisplayGameOver(gameState);
                    }

                }
                _displayManager.PromptPlayAgain();
                keepPlaying = _inputManager.GetYesNoResponse();
            }
        }

        private PlayerDirection EnsurePlayerDirection()
        {
            var playerDirection = _inputManager.GetDirection();
            while (!playerDirection.HasValue)
            {
                playerDirection = _inputManager.GetDirection();
            }
            return playerDirection.Value;
        }
    }
}