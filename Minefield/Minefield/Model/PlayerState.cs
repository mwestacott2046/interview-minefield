using System;

namespace Minefield.Model
{
    public class PlayerState : IPlayerState
    {
        public int Column{ get; set; }
        public int Row { get; set; }

        public int Deaths { get; set; }
        public int Moves { get; set; }

        /// <summary>
        /// Returns the current player position in Chess Notation
        /// Assumes that the Column Value will not go beyond 25 as this will give an unexpected output.
        /// </summary>
        /// <returns>string</returns>
        public string GetChessNotation()
        {
            return $"{char.ConvertFromUtf32('A' + Column)}{Row + 1}";
        }
    }
}
