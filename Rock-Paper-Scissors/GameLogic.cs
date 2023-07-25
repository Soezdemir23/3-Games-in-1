using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1.Rock_Paper_Scissors
{

    public enum RPS
    {
        Rock,
        Paper,
        Scissors
    }

    internal class GameLogic
    {
        private int maxRounds = 3;
        private int rounds = 0;
        private bool vsCPU = false;
        private Player[] players = new Player[2];


        public void Main()
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors!");
            Console.Write("Are you playing alone, or with a friend?\n1. Player vs CPU\n2. Player vs Player\n3. Go back to the main menu\nChoose 1,2 or 3:");
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("You have chosen to play against the CPU!");
                    Console.WriteLine("Get Ready!");
                    vsCPU = true;
                    Console.Write("Please enter your name, Player1: ");

                    string? player1Name = Console.ReadLine();
                    if (player1Name == null || player1Name.Length == 0)
                    {
                        Console.WriteLine("Invalid name! Let's call you Player1!");
                        players.Append(new Player("Player1"));
                    }else
                    {
                        players.Append(new Player(player1Name));
                    }
                    players.Append(new Player("CPU"));
                    

                    break;
                case "2":
                    Console.WriteLine("You have chosen to play against a friend!");
                    Console.WriteLine("Get Ready!");
                    vsCPU = false;
                    Console.Write("Please enter your name, Player1: ");
                    
                    player1Name = Console.ReadLine();
                    HandlePlayerSetup(player1Name, 1);

                    string? player2Name = Console.ReadLine();
                    HandlePlayerSetup(player2Name, 2);
                    
                    
                    
                    
                    
                    break;
                case "3":
                Console.WriteLine("Going back to the main menu!");
                    
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again!");
                    break;
            }
        }

        public void HandlePlayerSetup(string playerName, int player)
        {

            if (playerName == null || playerName.Length == 0)
            {
                Console.WriteLine($"Invalid name! Let's call you Player{player}!");
                players.Append(new Player($"Player{player}"));
            }
            else
            {
                players.Append(new Player(playerName));
            }
        }






    }

    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public RPS choice { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
