using System.Collections.Generic;
using System.Linq;
using Minefield.Model;

namespace Minefield.Game
{
    public class GameStateProcessor : IGameStateProcessor
    {
        /// <summary>
        /// Play a single step of the game:
        ///     move the player,
        ///         uncover the tile,
        ///     increase the score,
        ///     check for deaths
        /// </summary>
        /// <param name="state"></param>
        /// <param name="direction"></param>
        public void PlayStep(IGameState state, PlayerDirection direction)
        {
            if (MovePlayer(state, direction))
            {
                state.Player.Moves += 1;
                if (state.PlayerIsStandingOnMine())
                {
                    state.Player.Deaths += 1;
                }
            }
        }

        private static bool MovePlayer(IGameState state, PlayerDirection direction)
        {
            var playerHasMoved = false;

            if (direction == PlayerDirection.Up && state.Player.Row > 0)
            {
                UncoverTile(state.GameGrid, state.Player.Row, state.Player.Column);
                state.Player.Row -= 1;
                playerHasMoved = true;
            }

            if (direction == PlayerDirection.Down && state.Player.Row < state.MaxRows - 1)
            {
                UncoverTile(state.GameGrid, state.Player.Row, state.Player.Column);
                state.Player.Row += 1;
                playerHasMoved = true;
            }

            if (direction == PlayerDirection.Left && state.Player.Column > 0)
            {
                UncoverTile(state.GameGrid, state.Player.Row, state.Player.Column);
                state.Player.Column -= 1;
                playerHasMoved = true;
            }

            if (direction == PlayerDirection.Right && state.Player.Column < state.MaxCols - 1 )
            {
                UncoverTile(state.GameGrid, state.Player.Row, state.Player.Column);
                state.Player.Column += 1;
                playerHasMoved = true;
            }

            return playerHasMoved;
        }

        private static void UncoverTile(IList<IList<Cell>> stateGameGrid, int playerRow, int playerColumn)
        {
            stateGameGrid[playerRow][playerColumn].IsUncovered = true;
        }

    }
}