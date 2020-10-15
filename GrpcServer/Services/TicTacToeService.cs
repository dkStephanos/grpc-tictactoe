using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer
{
    public class TicTacToeService : TicTacToe.TicTacToeBase
    {
        private readonly ILogger<TicTacToeService> _logger;
        private readonly TicTacToeGame _currGame;

        public TicTacToeService(TicTacToeGame currGame, ILogger<TicTacToeService> logger)
        {
            _logger = logger;
            _currGame = currGame;
        }

        public override Task<GreetResponse> GreetPlayer(GreetRequest request, ServerCallContext context)
        {
            // Sends the greet message to the client, prompting for token selection
            return Task.FromResult(new GreetResponse
            {
                GreetResponseMsg = _currGame.GetGreetingPrompt()
            });
        }

        public override Task<CurrentGameBoard> NewGame(PlayerToken request, ServerCallContext context)
        {
            // Initialize standard new game prompt
            string gameMsg = "\nWhere would you like to go?";
            char playerToken;

            // Check to make sure the token provided is of type char and matches either X or O, otherwise send an error msg and reprompt for valid input
            if(Char.TryParse(request.PlayerTokenMsg, out playerToken) && (playerToken == 'X' || playerToken == 'O'))
            {
                _currGame.selectPlayerToken(char.Parse(request.PlayerTokenMsg));
            } else
            {
                gameMsg = "Invalid Selection: Must be X or O. Try again: ";
            }
            
            // Sends the starting game board and prompt for move or error msg depending on valid token selection
            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard(),
                GameMsg = gameMsg
            });
        }

        public override Task<CurrentGameBoard> PlayNextTurn(PlayerMove request, ServerCallContext context)
        {
            // Set up game variables and initialize the error message if user entered invalid board location
            string gameMsg;
            string errorMsg = "Invalid Selection: Must be a number between 0-9. Try again: ";
            int boardLocation;

            // Attempt to parse the boardLocation sent by the user
            bool validBoardLocation = Int32.TryParse(request.BoardLocation, out boardLocation);

            // Confirm the boardLocation is an integer in the proper range, if so, attempt to play the turn, if not set the error message
            if (validBoardLocation && boardLocation <= 9 && boardLocation >= 0)
            {
                gameMsg = _currGame.playTurn(boardLocation);
            } else
            {
                gameMsg = errorMsg;
            }
            
            // Get the current state of the game board to send to user, storing it in case we need to reset the board in case of end game
            string gameBoard = _currGame.board.drawBoard();

            // If the game is over, clear the board for the next session, otherwise if the game is still going and we have no errors, set the gameMsg to prompt for next move
            if (gameMsg.Contains("won!") || gameMsg.Contains("Draw"))
            {
                _currGame.resetBoard();
            } else if (gameMsg != errorMsg)
            {
                gameMsg = "Where would you like to go next?";
            }

            // Finally, send the message with the current game board and gameMsg (either containing final result, prompt or error)
            return Task.FromResult(new CurrentGameBoard
            {
                Board = gameBoard,
                GameMsg = gameMsg
            });
        }
    }
}
