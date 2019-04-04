using System;
using System.IO;
using Minefield.Model;

namespace Minefield.Inputs
{
    public class ConsoleInputManager : IInputManager
    {
        private readonly TextReader _inputReader;

        public ConsoleInputManager(): this(Console.In)
        {
            
        }

        public ConsoleInputManager(TextReader inputReader)
        {
            _inputReader = inputReader;
        }


        public PlayerDirection? GetDirection()
        {

            PlayerDirection? direction;
            var input = _inputReader.ReadLine();
            if (input == null)
            {
                return null;
            }

            switch (input.ToLower())
            {
                case "up":
                case "u":
                    direction = PlayerDirection.Up;
                    break;
                case "down":
                case "d":
                    direction = PlayerDirection.Down;
                    break;

                case "left":
                case "l":
                    direction = PlayerDirection.Left;
                    break;

                case "right":
                case "r":
                    direction = PlayerDirection.Right;
                    break;

                default:
                    direction = null;
                    break;
            }
            return direction;
        }

        public bool GetYesNoResponse()
        {
            var input = _inputReader.ReadLine();
            if (input == null)
            {
                return false;
            }

            bool affirmative;
            switch (input.ToLower())
            {
                case "y":
                case "yes":
                    affirmative = true;
                    break;
                default:
                    affirmative = false;
                    break;
            }

            return affirmative;
        }

        public void WaitForAnyInput()
        {
            _inputReader.ReadLine();
        }
    }
}