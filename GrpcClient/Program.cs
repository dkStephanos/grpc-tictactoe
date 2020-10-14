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
            var greetResponse = await client.GreetPlayerAsync(
                new GreetRequest { GreetRequestMsg = "Hello, I would like to play!" });

            Console.WriteLine(greetResponse.GreetResponseMsg);
            string playerToken = Console.ReadLine(); 

            CurrentGameBoard reply = await client.NewGameAsync(
                new PlayerToken { PlayerTokenMsg = playerToken });


         

            Console.WriteLine($"{reply.Board} {reply.GameMsg}");
         
            
         Console.WriteLine("Press any key to exit...");
         Console.ReadKey();
      }
   }
}
