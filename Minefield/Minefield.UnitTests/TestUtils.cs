using System.Collections.Generic;
using System.Linq;
using Minefield.Model;

namespace Minefield.UnitTests
{
    public static class TestUtils
    {
        public static List<IList<Cell>> MakeEmptyGrid(int maxRows, int maxCols)
        {
            var gameGrid = new List<IList<Cell>>();
            foreach (var i in Enumerable.Range(0, maxRows))
            {
                var row = Enumerable.Range(0, maxCols).Select(i1 => new Cell()).ToList();
                gameGrid.Add(row);
            }
            return gameGrid;
        }
    }
}