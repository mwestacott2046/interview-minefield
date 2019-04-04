using System.Collections.Generic;
using System.Linq;

namespace Minefield.Model
{
    public class GameState : IGameState
    {
        public IPlayerState Player { get; private set; }
        public IList<IList<Cell>> GameGrid { get; private set; }
        public int MaxLives { get; private set; }
        public int MaxRows { get; private set; }
        public int MaxCols { get; private set; }

        public GameState(IPlayerState player, IList<IList<Cell>> grid, int maxLives, int maxRows, int maxCols)
        {
            Player = player;
            GameGrid = grid;
            MaxLives = maxLives;
            MaxRows = maxRows;
            MaxCols = maxCols;
        }

        public bool HasWon()
        {
            return Player.Column == (MaxCols-1);
        }

        public bool HasLost()
        {
            return Player.Deaths >= MaxLives;
        }

        public bool PlayerIsStandingOnMine()
        {
            return GameGrid[Player.Row][Player.Column].IsMine;
        }

        public int GetLivesRemaining()
        {
            return MaxLives - Player.Deaths;
        }

        public int CalcScore()
        {
            return Player.Moves - Player.Deaths;
        }
    }
}
