using System;
using System.IO;
using System.Linq;
using System.Text;
using Minefield.Model;

namespace Minefield.Display
{
    public class ConsoleDisplayManager : IDisplayManager
    {
        private readonly TextWriter _consoleWriter;
        private const string PLAYER = "P";
        private const string PLAYER_DEAD = "X";
        private const string FOG = "#";
        private const string MINE = "*";
        private const string UNCOVERED = " ";

        public ConsoleDisplayManager(): this (Console.Out){}

        public ConsoleDisplayManager(TextWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public void DisplayInstructions()
        {
            WriteEmptyLine();
            WriteEmptyLine();
            _consoleWriter.WriteLine("How To Play");
            WriteEmptyLine();
            _consoleWriter.WriteLine("The aim of the game is to get from one side of the battlefield to the opposite side without standing on any mine.");
            _consoleWriter.WriteLine("Because of the fog and the slyness of your foe you cannot know where the mines are hiding.");
            _consoleWriter.WriteLine("However at the edge of the battlefield is a hospital that will patch you up and put you back on the battlefield.");
            _consoleWriter.WriteLine("Remember that modern medicine has limits and you can only be patched up a few times.");
            _consoleWriter.WriteLine("Your score will be based on how far you have traveled and how many hospital visits you've had.");
            WriteEmptyLine();
            _consoleWriter.WriteLine("Game Icons");
            _consoleWriter.WriteLine("\t" + PLAYER + " = Player");
            _consoleWriter.WriteLine("\t" + PLAYER_DEAD + " = A Very Unfortunate Player");
            _consoleWriter.WriteLine("\t" + MINE + " = Mine") ;
            _consoleWriter.WriteLine("\t" + FOG + " = Fog");
            WriteEmptyLine();
            WriteEmptyLine();
        }

        private void WriteEmptyLine()
        {
            _consoleWriter.WriteLine("");
        }

        public void DisplayGameState(IGameState state)
        {
            const int padWidth = 3;

            WriteEmptyLine();

            foreach (var sp in Enumerable.Repeat(0, padWidth + 1))
            {
                _consoleWriter.Write(" ");
            }

            foreach (var col in Enumerable.Range(0, state.MaxCols))
            {
                _consoleWriter.Write(char.ConvertFromUtf32('A' + col));
            }
            _consoleWriter.Write(_consoleWriter.NewLine);

            for (var rowIndex = 0; rowIndex < state.MaxRows; rowIndex++)
            {
                var rowText = (rowIndex+1).ToString().PadLeft(padWidth, '0');
                _consoleWriter.Write(rowText);
                _consoleWriter.Write(' ');
                for (var columnIndex = 0; columnIndex < state.MaxCols; columnIndex++)
                {
                    _consoleWriter.Write(DecodeCellValue(state, columnIndex, rowIndex));
                }
                _consoleWriter.Write(_consoleWriter.NewLine);
            }

            WriteEmptyLine();

            _consoleWriter.WriteLine(string.Format("Position: {0}, Lives: {1}, Score {2}",
                state.Player.GetChessNotation(), 
                state.GetLivesRemaining(), 
                state.CalcScore()));

            WriteEmptyLine();
        }

        private static string DecodeCellValue(IGameState state, int columnIndex, int rowIndex)
        {
            var cell = state.GameGrid[rowIndex][columnIndex];

            if (state.Player.Column == columnIndex && state.Player.Row == rowIndex)
            {
                return cell.IsMine ? PLAYER_DEAD : PLAYER;
            }

            if (cell.IsUncovered)
            {
                return cell.IsMine ? MINE : UNCOVERED;
            }

            return FOG;
                
        }

        public void DisplayGameOver(IGameState state)
        {
            WriteEmptyLine();
            _consoleWriter.WriteLine("Medical Science has limits Soldier! You scored {0} points.", state.CalcScore());
            WriteEmptyLine();

        }

        public void DisplayWin(IGameState state)
        {
            WriteEmptyLine();
            _consoleWriter.WriteLine("Congratulations, you crossed the battlefield! You scored {0} points.", state.CalcScore());
            WriteEmptyLine();
        }

        public void PromptPlayAgain()
        {
            WriteEmptyLine();
            _consoleWriter.WriteLine("Would you like to play again (yes/no)?");
            WriteEmptyLine();
        }

        public void PromptWaitAnyInput()
        {
            _consoleWriter.WriteLine("Press <Enter> to Continue.");
        }

        public void PromptControlGuidance()
        {
            _consoleWriter.WriteLine("Enter a Direction: (u)p, (d)own, (l)eft or (r)ight.");
        }

    }
}