using System;
using System.Collections.Generic;

namespace SynchronousServer
{
    public class TicTacToeGame
    {

        public char playerToken;
        public char cpuToken;
        public TicTacToeBoard board;

        public TicTacToeGame(char playerToken)
        {
            this.playerToken = playerToken;
            if(playerToken == 'X')
            {
                this.cpuToken = 'O';
            } else
            {
                this.cpuToken = 'X';
            }
            this.board = new TicTacToeBoard();
        }

        public string playTurn(int position)
        {
            if(board.boardCells[position] != ' ')
            {
                return "Invalid";
            }

            board.boardCells[position] = playerToken;

            var random = new Random();
            List<int> openCells = getOpenCells();
            board.boardCells[openCells[random.Next(openCells.Count)]] = cpuToken;

            return checkForResult(board.boardCells);
        }

        public static string checkForResult(char[] boardCells)
        {
            //Check for win in first row
            if (boardCells[0] != ' ' && boardCells[0] == boardCells[1] && boardCells[1] == boardCells[2])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second row
            else if (boardCells[3] != ' ' && boardCells[3] == boardCells[4] && boardCells[4] == boardCells[5])
            {
                return boardCells[3] + " won!";
            }

            //Check for win in third row
            else if (boardCells[6] != ' ' && boardCells[6] == boardCells[7] && boardCells[7] == boardCells[8])
            {
                return boardCells[6] + " won!";
            }

            //Check for win in first col
            else if (boardCells[0] != ' ' && boardCells[0] == boardCells[3] && boardCells[3] == boardCells[6])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second col
            else if (boardCells[1] != ' ' && boardCells[1] == boardCells[4] && boardCells[4] == boardCells[7])
            {
                return boardCells[1] + " won!";
            }

            //Check for win in third col
            else if (boardCells[2] != ' ' && boardCells[2] == boardCells[5] && boardCells[5] == boardCells[8])
            {
                return boardCells[2] + " won!";
            }

            //Check for win in first diagonal
            else if (boardCells[0] != ' ' && boardCells[0] == boardCells[4] && boardCells[4] == boardCells[8])
            {
                return boardCells[0] + " won!";
            }

            //Check for win in second diagonal
            else if (boardCells[2] != ' ' && boardCells[2] == boardCells[4] && boardCells[4] == boardCells[6])
            {
                return boardCells[2] + " won!";
            }

            //Finally, check for draw 
            else if (boardCells[0] != ' ' && boardCells[1] != ' ' && boardCells[2] != ' ' && boardCells[3] != ' ' && boardCells[4] != ' ' && boardCells[5] != ' ' && boardCells[6] != ' ' && boardCells[7] != ' ' && boardCells[8] != ' ' )
            {
                return "Draw";
            }

            //If we get this far, there is no outcome yet, return Not Finished
            return "Not Finished";
        }

        public List<int> getOpenCells()
        {
            List<int> openCells = new List<int>();

            for (int i = 0; i < board.boardCells.Length; i++)
            {
                if(board.boardCells[i] == ' ')
                {
                    openCells.Add(i);
                }
            }

            return openCells;
        }
    }
}
