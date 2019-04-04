using Minefield.Model;

namespace Minefield.Game
{
    public interface IGameStateProcessor
    {
        void PlayStep(IGameState state, PlayerDirection direction);
    }
}