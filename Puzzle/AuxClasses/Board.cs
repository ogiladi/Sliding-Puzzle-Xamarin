using System;

namespace Puzzle
{
    /// <summary>
    /// A Class that contains the game board and can generate new games.
    /// </summary>
    public class Board
    {
        readonly int size;
        int emptyCellRow, emptyCellCol;
        readonly int[,] board;

        /// <summary>
        /// Constructor. Creates board as a 2D array.
        /// </summary>
        /// <param name="size">Size.</param>
        public Board(int size)
        {
            this.size = size;
            board = new int[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    board[row, col] = row * size + col;
                }
            }

        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void CreateGame()
        {
            while (true)
            {
                ShuffleBoard();
                MakeSolvable();
                if (!IsSolved())
                    break;
            }
        }

        /// <summary>
        /// Checks if the game board is solved.
        /// </summary>
        /// <returns><c>true</c>, if game is solved,
        ///  <c>false</c> otherwise.</returns>
        public bool IsSolved()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[row, col] != row * size + col)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Chooses a cell from the board. If the empty cell is adjacent to 
        /// the chosen cell, move the chosen cell to the empty cell.
        /// </summary>
        /// <returns><c>true</c> if a cell has been moved,
        /// <c>false</c> otherwise.</returns>
        /// <param name="row">Cell row.</param>
        /// <param name="col">Cell column.</param>
        public bool ChooseCell(int row, int col)
        {            
            var temp = board[row, col];
            var oldEmptyCell = emptyCellRow * size + emptyCellCol;

            if (row == emptyCellRow && Math.Abs(col - emptyCellCol) == 1 ||
                col == emptyCellCol && Math.Abs(row - emptyCellRow) == 1)
            {                
                board[row, col] = board[emptyCellRow, emptyCellCol];
                board[emptyCellRow, emptyCellCol] = temp;
                emptyCellRow = row;
                emptyCellCol = col;
                return true;
            }

            return false;
        }

        /// <summary>
        /// String representation of the cell.
        /// </summary>
        /// <returns>A string representing the value of the cell.</returns>
        /// <param name="row">Cell row</param>
        /// <param name="col">Cell column</param>
        public string CellValue(int row, int col)
        {            
            if (row == emptyCellRow && col == emptyCellCol)
                return "";
            return Convert.ToString(board[row, col] + 1);
        }

        /// <summary>
        /// Updates the location of the empty cell.
        /// </summary>
        void UpdateEmptyCell()
        {
            for (int row = 0; row < size; row++)
            {                
                for (int col = 0; col < size; col++)
                {
                    if (board[row, col] == size * size - 1)
                    {
                        emptyCellRow = row;
                        emptyCellCol = col;
                        return;
                    }
                        
                }
            }

        }

        /// <summary>
        /// Counts inversions for a given cell.
        /// </summary>
        /// <returns>The cell inversions.</returns>
        /// <param name="row">Cell row.</param>
        /// <param name="col">Cell column</param>
        int CountCellInversions(int row, int col)
        {
            var inv = 0;
            for (int ind = row * size + col + 1; ind < size * size; ind++)
            {
                var otherRow = ind / size;
                var otherCol = ind % size;
                if (board[otherRow, otherCol] < board[row, col] && 
                    board[row, col] != size * size - 1)
                    inv++;
            }
            return inv;
        }

        /// <summary>
        /// Counts the total number of inversions in a board.
        /// </summary>
        /// <returns>Total number of inversions.</returns>
        int CountInversions()
        {
            var count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                    count += CountCellInversions(row, col);
            }
            return count;
        }

        /// <summary>
        /// In case the board is not solvable, switch two cells to make it 
        /// solvable.
        /// </summary>
        void MakeSolvable()
        {            
            if (!IsSolvable())
            {
                if (emptyCellRow == 0 && emptyCellCol <= 1)
                    SwapTiles(size - 1, size - 2, size - 1, size - 1);
                else
                    SwapTiles(0, 0, 0, 1);
            }
            UpdateEmptyCell();
        }

        /// <summary>
        /// Check whether board is solvable.
        /// </summary>
        /// <returns><c>true</c>, if board is solvable, 
        /// <c>false</c> otherwise.</returns>
        bool IsSolvable()
        {            
            if (size % 2 == 1)
                return (CountInversions() % 2 == 0);
            return ((CountInversions() + size - 1 - emptyCellRow) % 2 == 0);
        }

        /// <summary>
        /// Shuffles the board randomly using the Fisher-Yates shuffling 
        /// algorithm.
        /// </summary>
        void ShuffleBoard()
        {
            var n = size * size - 1;
            Random random = new Random();
            while (n > 0)
            {
                var k = random.Next(n);
                var x_i = n / size;
                var y_i = n % size;
                var x_k = k / size;
                var y_k = k % size;
                SwapTiles(x_i, y_i, x_k, y_k);
                n--;
            }
            UpdateEmptyCell();
        }

        /// <summary>
        /// Swaps two tiles in the board.
        /// </summary>
        /// <param name="i">Row of first cell.</param>
        /// <param name="j">Column of first cell.</param>
        /// <param name="k">Row of second cell.</param>
        /// <param name="l">Column of second cell.</param>
        void SwapTiles(int i, int j, int k, int l)
        {
            var temp = board[i, j];
            board[i, j] = board[k, l];
            board[k, l] = temp;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the 
        /// current <see cref="T:Puzzle.Board"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the 
        /// current <see cref="T:Puzzle.Board"/>.</returns>
        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == size * size - 1)
                        result += "\t";
                    else
                        result += board[i, j] + "\t";
                }
                result += "\n";
            }
            return result;
        }
    }
}
