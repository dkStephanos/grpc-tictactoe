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
        private TicTacToeGame _currGame;

        public TicTacToeService(ILogger<TicTacToeService> logger)
        {
            _logger = logger;
        }

        public override Task<CurrentGameBoard> NewGame(PlayerToken request, ServerCallContext context)
        {
            _currGame = new TicTacToeGame(char.Parse(request.PlayerToken_));

            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard()
            });
        }
    }
}
