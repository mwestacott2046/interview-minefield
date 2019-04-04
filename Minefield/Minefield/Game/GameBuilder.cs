using System;
using System.Collections.Generic;
using System.Linq;
using Minefield.Model;

namespace Minefield.Game
{
    public class GameBuilder : IGameBuilder
    {
        
        public IGameState Build(int maxGridRows, int maxGridCols, int maxPlayerLives, int maxMines)
        {
            var gameGrid = BuildGameGrid(maxGridRows, maxGridCols);
            var player = new PlayerState {Column = 0, Row = maxGridRows / 2, Deaths = 0, Moves = 0};
            SetMines(gameGrid, maxGridRows, maxGridCols, maxMines, player);
            
            return new GameState(player, gameGrid, maxPlayerLives, maxGridRows, maxGridCols);
        }

        private static void SetMines(IList<IList<Cell>> gameGrid, 
            int maxRows, 
            int maxCols, 
            int maxMines,
            IPlayerState player)
        {
            var rand = new Random();
            var minesPlaced = 0;
            while (minesPlaced < maxMines)
            {
                var mineRow = rand.Next(maxRows);
                var mineCol = rand.Next(maxCols);

                if (!gameGrid[mineRow][mineCol].IsMine && 
                    !(mineCol == player.Column && mineRow == player.Row))
                {
                    gameGrid[mineRow][mineCol].IsMine = true;
                    minesPlaced += 1;
                }
            }
        }

        private static IList<IList<Cell>> BuildGameGrid(int maxRows, int maxCols)
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
