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
            // Establish the gRPC channel and intialize a new instance of the TicTacToe client on that channel
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TicTacToe.TicTacToeClient(channel);
            
            // Initiate the greeting
            var greetResponse = await client.GreetPlayerAsync(
                new GreetRequest { GreetRequestMsg = "Hello, I would like to play!" });

            // Print greeting for user, collecting player token from user
            Console.WriteLine(greetResponse.GreetResponseMsg);
            string playerToken = Console.ReadLine(); 

            // Initialize a new game, passing along the token collected from the user
            CurrentGameBoard reply = await client.NewGameAsync(
                new PlayerToken { PlayerTokenMsg = playerToken });

            // Set up a loop to confirm we get a valid token, repeating the new game request until a valid choice is provided
            while(reply.GameMsg.Contains("Invalid"))
            {
                Console.WriteLine(reply.GameMsg);
                playerToken = Console.ReadLine();
                reply = await client.NewGameAsync(
                    new PlayerToken { PlayerTokenMsg = playerToken });
            }

            // Then enter the main game loop, continuing until a final outcome is reached
            string nextMove;
            while(!reply.GameMsg.Contains("won!") && !reply.GameMsg.Contains("Draw"))
            {
                Console.WriteLine($"{reply.Board}\n{reply.GameMsg}\n");

                // Collect the move from the user, passing along to the next turn method (error checking provided on server)
                nextMove = Console.ReadLine();
                reply = await client.PlayNextTurnAsync(
                    new PlayerMove { BoardLocation = nextMove });
            }
         
           // Print the final game board with message
           Console.WriteLine($"{reply.Board}\n{reply.GameMsg}\n");
         
           // Wait for input to exit client
           Console.WriteLine("Press any key to exit...");
           Console.ReadKey();
      }
   }
}
