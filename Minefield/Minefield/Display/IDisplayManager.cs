using Minefield.Model;

namespace Minefield.Display
{
    public interface IDisplayManager
    {
        /// <summary>
        /// Display instructions on how to play and what inputs to use
        /// </summary>
        void DisplayInstructions();

        /// <summary>
        /// Display the Current Game State to the Player
        /// </summary>
        /// <param name="state"></param>
        void DisplayGameState(IGameState state);

        /// <summary>
        /// Display the Game over state and message
        /// </summary>
        /// <param name="state"></param>
        void DisplayGameOver(IGameState state);

        /// <summary>
        /// Display the Game over state and message
        /// </summary>
        /// <param name="state"></param>
        void DisplayWin(IGameState state);

        /// <summary>
        /// Ask the Player if they would like to play again?
        /// </summary>
        void PromptPlayAgain();
        
        /// <summary>
        /// Notify the User to press/click something
        /// </summary>
        void PromptWaitAnyInput();

        /// <summary>
        /// Prompt The user to select a direction to move in
        /// </summary>
        void PromptControlGuidance();


    }
}