using System;
namespace SynchronousServer
{
    public class TicTacToeBoard
    {
        public char[] boardCells;

        public TicTacToeBoard()
        {
            this.boardCells = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        }

        public string drawBoard()
        {
            string currBoard = "\n";

            currBoard += "     |     |      \n";

            currBoard += String.Format("  {0}  |  {1}  |  {2}\n", boardCells[0], boardCells[1], boardCells[2]);

            currBoard += "_____|_____|_____ \n";

            currBoard += "     |     |      \n";

            currBoard += String.Format("  {0}  |  {1}  |  {2}\n", boardCells[3], boardCells[4], boardCells[5]);

            currBoard += "_____|_____|_____ \n";

            currBoard += "     |     |      \n";

            currBoard += String.Format("  {0}  |  {1}  |  {2}\n", boardCells[6], boardCells[7], boardCells[8]);

            currBoard += "     |     |      \n";

            return currBoard;

        }
    }
}
