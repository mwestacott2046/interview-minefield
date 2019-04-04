using Minefield.Model;

namespace Minefield.Game
{
    public interface IGameBuilder
    {
        IGameState Build(int maxGridRows, int maxGridCols, int maxPlayerLives, int maxMines);
    }
}