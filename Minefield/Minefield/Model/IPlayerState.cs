namespace Minefield.Model
{
    public interface IPlayerState
    {
        int Column { get; set; }
        int Row { get; set; }
        int Deaths { get; set; }
        int Moves { get; set; }

        /// <summary>
        /// Returns the current player position in Chess Notation
        /// Assumes that the Column Value will not go beyond 25 as this will give an unexpected output.
        /// </summary>
        /// <returns>string</returns>
        string GetChessNotation();
    }
}