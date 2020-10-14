﻿using System;
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
            _currGame.selectPlayerToken(char.Parse(request.PlayerTokenMsg));  

            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard(),
                GameMsg = "\nWhere would you like to go?"
            });
        }

        public override Task<CurrentGameBoard> PlayNextTurn(PlayerMove request, ServerCallContext context)
        {
            string gameMsg = _currGame.playTurn(int.Parse(request.BoardLocation));

            if (gameMsg == "Not Finished") gameMsg = "Where would you like to go next?";

            return Task.FromResult(new CurrentGameBoard
            {
                Board = _currGame.board.drawBoard(),
                GameMsg = gameMsg
            });
        }
    }
}
