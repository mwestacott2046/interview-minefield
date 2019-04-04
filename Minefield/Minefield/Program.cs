using Minefield.Display;
using Minefield.Game;
using Minefield.Inputs;
using TinyIoC;

namespace Minefield
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigureContainer();
            StartGame(container);
        }

        private static void StartGame(TinyIoCContainer container)
        {
            var game = container.Resolve<IMinefieldGame>();
            game.RunGame();
        }

        private static TinyIoCContainer ConfigureContainer()
        {
            var container = TinyIoCContainer.Current;
            container.Register<IInputManager, ConsoleInputManager>();
            container.Register<IDisplayManager, ConsoleDisplayManager>();
            container.Register<IGameBuilder, GameBuilder>();
            container.Register<IGameStateProcessor, GameStateProcessor>();
            container.Register<IMinefieldGame, MinefieldGame>();
            
            return container;
        }
    }
}
