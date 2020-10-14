using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
   class Program
   {
      static async Task Main(string[] args)
      {
         using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TicTacToe.TicTacToeClient(channel);

            var reply = await client.NewGameAsync(
            new PlayerToken { PlayerToken_ = "X" });
         
            Console.WriteLine($"{reply.Board}");
         
            
         Console.WriteLine("Press any key to exit...");
         Console.ReadKey();
      }
   }
}
