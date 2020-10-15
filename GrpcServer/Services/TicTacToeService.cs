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

            return Task.FromResult(new GreetResponse
            {
                GreetResponseMsg = _currGame.GetGreetingPrompt()
            });
        }

        public override Task<CurrentGameBoard> NewGame(PlayerToken request, ServerCallContext context)
        {
            string gameMsg = "\nWhere would you like to go?";
            char playerToken;

            if(Char.TryParse(request.PlayerTokenMsg, out playerToken) && (playerToken == 'X' || playerToken == 'O'))
            {
                _currGame.selectPlayerToken(char.Parse(request.PlayerTokenMsg));
            } else
            {
                gameMsg = "Invalid Selection: Must be X or O. Try again: ";
            }
               
            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard(),
                GameMsg = gameMsg
            });
        }

        public override Task<CurrentGameBoard> PlayNextTurn(PlayerMove request, ServerCallContext context)
        {
            string gameMsg;
            string errorMsg = "Invalid Selection: Must be a number between 0-9. Try again: ";
            int boardLocation;
            bool validBoardLocation = Int32.TryParse(request.BoardLocation, out boardLocation);
            if (validBoardLocation && boardLocation <= 9 && boardLocation >= 0)
            {
                gameMsg = _currGame.playTurn(boardLocation);
            } else
            {
                gameMsg = errorMsg;
            }
                
            string gameBoard = _currGame.board.drawBoard();

            if (gameMsg.Contains("won!") || gameMsg.Contains("Draw"))
            {
                _currGame.resetBoard();
            } else if (gameMsg != errorMsg)
            {
                gameMsg = "Where would you like to go next?";
            }

            return Task.FromResult(new CurrentGameBoard
            {
                Board = gameBoard,
                GameMsg = gameMsg
            });
        }
    }
}
