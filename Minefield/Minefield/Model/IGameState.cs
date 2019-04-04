using System.Collections.Generic;

namespace Minefield.Model
{
    public interface IGameState
    {
        IPlayerState Player { get; }
        IList<IList<Cell>> GameGrid { get; }
        int MaxLives { get; }
        int MaxRows { get; }
        int MaxCols { get; }

        bool HasWon();
        bool HasLost();
        bool PlayerIsStandingOnMine();
        int GetLivesRemaining();
        int CalcScore();
    }
}