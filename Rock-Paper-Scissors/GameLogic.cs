using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1.Rock_Paper_Scissors
{

	public enum RPS
	{
		[Description("Rock")]
		Rock,
		[Description("Paper")]
		Paper,
		[Description("Scissors")]
		Scissors
	}



	internal class GameLogicRPS
	{
		private int maxScore { get; set; } = 3;
		private bool vsCPU = false;
		private Player[] players = { };
		private bool wantsReturn = false;
		private bool wantsQuit = false;
		private bool wantsReplay = true;


		public GameLogicRPS()
		{
			players = new Player[2];
			Gamestart();
		}

		private void Intro()
		{
			Console.WriteLine("Welcome to Rock, Paper, Scissors!");
			Console.Write("Are you playing alone, or with a friend?\n1. Player vs CPU\n2. Player vs Player\n3. Go back to the main menu\nChoose 1,2 or 3:");
			string? choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					Console.WriteLine("Your own machine has risen from it's silicone shackles to challenge YOU!");
					Console.WriteLine("Get Ready!");
					vsCPU = true;
					Console.Write("Please enter your name, Player1: ");

					string? player1Name = Console.ReadLine();
					HandlePlayerSetup(player1Name, 1);
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
					Console.WriteLine("Going back to the Game Selection Menu!");
					wantsQuit = true;
					break;
				default:
					Console.WriteLine("Invalid choice! Please try again!");
					break;
			}
		}

		private void HandlePlayerSetup(string playerName, int player)
		{

			if (playerName == null || playerName.Length == 0)
			{
				Console.WriteLine($"Invalid name! Let's call you Player{player}!");
				players[player - 1] = new Player($"Player{player}");


			}
			else
			{
				players[player - 1] = (new Player(playerName));
			}
			if (vsCPU == true)
			{
				players[1] = new Player("CPU");
			}
		}

		private void Gamestart()
		{
			Intro();
			if (wantsQuit == true)
			{
				return;
			}
			while (wantsReplay)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.BackgroundColor = ConsoleColor.Cyan;
				Console.Write($"{players[0].Name}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write(" VS ");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.BackgroundColor = ConsoleColor.Green;
				Console.WriteLine($"{players[1].Name}");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.BackgroundColor = ConsoleColor.Cyan;
				Console.Write($"{players[0].Score}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write(":");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.BackgroundColor = ConsoleColor.Green;
				Console.WriteLine($"{players[1].Score}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				Console.WriteLine("\n\n");

				HandlePlayerChoices(players[0], vsCPU);

				HandlePlayerChoices(players[1], vsCPU);
				CheckScoreCondition(players);
				WhoWon(players);
				Console.Write("Press any key to continue...");
				Console.Read();
				Console.Clear();
			}
		}

		private static void CheckScoreCondition(Player[] players)
		{
			var player1 = players[0];
			var player2 = players[1];

			if (player1.choice == player2.choice)
			{
				Console.WriteLine("DRAW!");
				Console.WriteLine("The the rounds go on and on");
			}//player 2 wins 
			else if ((player1.choice == RPS.Rock && player2.choice == RPS.Paper) ||
				(player1.choice == RPS.Paper && player2.choice == RPS.Scissors) ||
				(player1.choice == RPS.Scissors && player2.choice == RPS.Rock))
			{
				player2.Score++;
				Console.WriteLine($"{player2.Name} has won this round.");
			}
			else
			{
				player1.Score++;
				Console.WriteLine($"{player1.Name} has won this round.");
			}


		}

		private void WhoWon(Player[] players)
		{
			if (players[1].Score == maxScore)
			{
				Console.WriteLine($"{players[1].Name} has won!");
				Console.WriteLine("Press a button to continue.");
				Console.Read();
				Console.Clear();
				Console.WriteLine("The battle has ended, but the game can always restart");
				Console.Write("Restart game? [Y/N] ");
				string confirmation = Console.ReadLine();
				if (confirmation == "y" || confirmation == "Y")
				{
					Console.WriteLine("Alright, let's go again!");
				}
				else if (confirmation == "n" || confirmation == "N")
				{
					Console.WriteLine("Alright, let's go back to the main menu!");
				}
				else
				{
					Console.WriteLine("You didn't press Y or N, so we're going back to the main menu!");
				}
			} else
			{
				Console.WriteLine("")
			}
		}


		private static void HandlePlayerChoices(Player player, Boolean isCPU)
		{
			if (isCPU && player.Name == "CPU")
			{
				Console.WriteLine("The machine is making it's move....");
				player.choice = (RPS)(new Random().Next(3));
				Thread.Sleep(1000);
				Console.WriteLine($"The silicone ghost chose {GetEnumName(player.choice)}.");
			}else
			{

			while (true)
			{
				Console.WriteLine($"{player.Name}, it is your turn, choose your Destiny:");
				Console.WriteLine("1. Rock? Press 1 or the r key");
				Console.WriteLine("2. Paper? Choose 2 or the p key");
				Console.WriteLine("3. Scissors? Use the 3 key or press s");
				string chosen = Console.ReadLine();

				if (chosen == "r" || chosen == "R" || chosen == "3")
				{
					player.choice = RPS.Scissors;
					break;
				}
				else if (chosen == "p" || chosen == "P" || chosen == "2")
				{
					player.choice = RPS.Paper;
					break;
				}
				else if (chosen == "s" || chosen == "S" || chosen == "1")
				{
					player.choice = RPS.Rock;
					break;
				}
				else
				{
					Console.WriteLine($"You entered {chosen}");
					Console.WriteLine("If you didn't want to play the game, you could've just closed the window.");
				}
			}

			}
		}
		private static string GetEnumName(Enum enumValue)
		{
			var descriptionAttribute = enumValue.GetType().GetField(enumValue.ToString())
										   .GetCustomAttributes(typeof(DescriptionAttribute), false)
										   .FirstOrDefault() as DescriptionAttribute;

			return descriptionAttribute != null ? descriptionAttribute.Description : enumValue.ToString();
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
