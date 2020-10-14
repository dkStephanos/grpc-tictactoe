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

        public override Task<CurrentGameBoard> NewGame(PlayerToken request, ServerCallContext context)
        {
            _currGame.selectPlayerToken(char.Parse(request.PlayerToken_));  

            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard()
            });
        }
    }
}
