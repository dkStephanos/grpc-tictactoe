using System;
using System.Collections.Generic;

namespace GrpcServer
{
    public class TicTacToeGame
    {

        public char playerToken;
        public char cpuToken;
        public TicTacToeBoard board;
        public const string greeting  = "Welcome to the Tic-Tac-Server. Are you playing X or O?  ";

        public TicTacToeGame()
        {
          this.board = new TicTacToeBoard();
        }

        public string GetGreetingPrompt()
        {
            return greeting;
        }

        public void resetBoard()
        {
            this.board = new TicTacToeBoard();
        }

        public void selectPlayerToken(char playerToken)
        {
            this.playerToken = playerToken;
            if (playerToken == 'X')
            {
                this.cpuToken = 'O';
            }
            else
            {
                this.cpuToken = 'X';
                playCpuTurn();
            }
        }

        public string playTurn(int position)
        {
            if(board.boardCells[position] != ' ')
            {
                return "Invalid";
            }

            board.boardCells[position] = playerToken;

            playCpuTurn();

            return checkForResult(board.boardCells);
        }

        public void playCpuTurn()
        {
            var random = new Random();
            List<int> openCells = getOpenCells();
            if(openCells.Count > 0)
                board.boardCells[openCells[random.Next(openCells.Count)]] = cpuToken;
        }

        public string checkForResult(char[] boardCells)
        {
            string status = "";

            //Check for win in first row
            if (boardCells[0] != ' ' && boardCells[0] == boardCells[1] && boardCells[1] == boardCells[2])
            {
                status = boardCells[0] + " won!";
            }

            //Check for win in second row
            else if (boardCells[3] != ' ' && boardCells[3] == boardCells[4] && boardCells[4] == boardCells[5])
            {
                status = boardCells[3] + " won!";
            }

            //Check for win in third row
            else if (boardCells[6] != ' ' && boardCells[6] == boardCells[7] && boardCells[7] == boardCells[8])
            {
                status = boardCells[6] + " won!";
            }

            //Check for win in first col
            else if (boardCells[0] != ' ' && boardCells[0] == boardCells[3] && boardCells[3] == boardCells[6])
            {
                status = boardCells[0] + " won!";
            }

            //Check for win in second col
            else if (boardCells[1] != ' ' && boardCells[1] == boardCells[4] && boardCells[4] == boardCells[7])
            {
                status = boardCells[1] + " won!";
            }

            //Check for win in third col
            else if (boardCells[2] != ' ' && boardCells[2] == boardCells[5] && boardCells[5] == boardCells[8])
            {
                status = boardCells[2] + " won!";
            }

            //Check for win in first diagonal
            else if (boardCells[0] != ' ' && boardCells[0] == boardCells[4] && boardCells[4] == boardCells[8])
            {
                status = boardCells[0] + " won!";
            }

            //Check for win in second diagonal
            else if (boardCells[2] != ' ' && boardCells[2] == boardCells[4] && boardCells[4] == boardCells[6])
            {
                status = boardCells[2] + " won!";
            }

            //Finally, check for draw 
            else if (boardCells[0] != ' ' && boardCells[1] != ' ' && boardCells[2] != ' ' && boardCells[3] != ' ' && boardCells[4] != ' ' && boardCells[5] != ' ' && boardCells[6] != ' ' && boardCells[7] != ' ' && boardCells[8] != ' ' )
            {
                status = "Draw";
            }

            //If we get this far, there is no outcome yet, return Not Finished
            if(status == "")
            {
                status = "Not Finished";
            }

            return status;
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
