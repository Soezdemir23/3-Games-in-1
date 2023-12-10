using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1.Rock_Paper_Scissors
{
    /// <summary>
    /// The enum for the required Rock, Paper, Scissors.
    /// 
    /// </summary>
    public enum RPS
    {
        [Description("Rock")]
        Rock,

        [Description("Paper")]
        Paper,

        [Description("Scissors")]
        Scissors
    }
    /// <summary>
    /// The class for the Rock Paper Scissors Logic
    /// </summary>
    internal class GameLogicRPS
    {
        /// <summary>
        /// Maximum Score you can and, for the sake of not becoming boring, should get
        /// </summary>
        private int maxScore { get; set; } = 3;

        /// <summary>
        /// Simple check whether the Computer is playing against the player or not
        /// </summary>
        private bool vsCPU = false;

        /// <summary>
        /// players, maximum should be 2.
        /// </summary>
        private Player[] players = { };

        /// <summary>
        /// checks if the game has started.
        /// If not, helps branching into the Intro method
        /// </summary>
        private bool gameStarted = false;

        /// <summary>
        /// while this is true, the game is running on repeat
        /// </summary>
        private bool gameRepeats = true;
        /// <summary>
        /// After object instantiation it creates two player objects and starts the game.
        /// Also clears after itself when done.
        /// </summary>
        public GameLogicRPS()
        {
            players = new Player[2];
            Gamestart();
            Console.Clear();
        }
        /// <summary>
        /// The meat of the game, it creates a nice view of the game table and
        /// delegates necessary choices to the other functions
        /// </summary>
        private void Gamestart()
        {
            //check if the game is already running
            while (gameRepeats)
            {
                //if the game didn't start yet, branch into the Intro method
                if (gameStarted == false)
                {
                    Intro();
                    gameStarted = true;
                    //
                    if (gameRepeats == false)
                    {
                        return;
                    }
                }
                // really proud of that one
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
                if (SomebodyWon(players) == true)
                {
                    Console.WriteLine("Will you play again?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    ConsoleKeyInfo decision = Console.ReadKey(true);
                    if (decision.Key == ConsoleKey.D1)
                    {
                        Array.Clear(players);
                        gameStarted = false;
                        Console.Clear();
                    }
                    else if (decision.Key == ConsoleKey.D2)
                    {
                        gameRepeats = false;
                    }
                }
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
        /// <summary>
        /// Handles how the players are to be named depending on whether it is a Computer you play against or not.
        /// Naming is only allowed for Human players to make sure there is no ambiguity.
        /// </summary>
        /// <param name="playerName">playername given</param>
        /// <param name="player">the position of the player in the player array</param>
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
        /// <summary>
        /// A very lengthy, flashy introduction to the game.
        /// Basically a game menu with:
        /// <list type="bullet">
        /// Player1 vs Computer
        /// 2 Player Hotseat
        /// Exit to Main menu
        /// </list>
        /// </summary>
        private void Intro()
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors!");
            Console.WriteLine(
                "Are you playing alone, or with a friend?\n1. Player vs CPU\n2. Player vs Player\n3. Go back to the main menu\nChoose 1,2 or 3:"
            );
            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine(
                        "Your own machine has risen from it's silicone shackles to challenge YOU!"
                    );
                    Console.WriteLine("Get Ready!");
                    vsCPU = true;
                    Console.Write("Please enter your name, Player1: ");

                    string? player1Name = Console.ReadLine();
                    HandlePlayerSetup(player1Name, 1);
                    players.Append(new Player("CPU"));
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("You have chosen to play against a friend!");
                    Console.WriteLine("Get Ready!");
                    vsCPU = false;

                    Console.Write("Please enter your name, Player1: ");
                    player1Name = Console.ReadLine();
                    HandlePlayerSetup(player1Name, 1);

                    Console.Write("Please enter your name, Player2: ");
                    string? player2Name = Console.ReadLine();
                    HandlePlayerSetup(player2Name, 2);

                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Going back to the Game Selection Menu!");
                    gameRepeats = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again!");
                    break;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Checks the players' scoring and declares the following conditions:
        /// <list type="bullet">
        /// Draw
        /// Player 1 won
        /// Player 2 won
        /// </list>
        /// Draw, Player 1 won, Player 2 won
        /// </summary>
        /// <param name="players">player objects passed for quick access and intellisense</param>
        private static void CheckScoreCondition(Player[] players)
        {
            Player player1 = players[0];
            Player player2 = players[1];

            if (player1.choice == player2.choice)
            {
                Console.WriteLine("DRAW!");
                Console.WriteLine("The the rounds go on and on");
            } //player 2 wins
            else if (
                (player1.choice == RPS.Rock && player2.choice == RPS.Paper)
                || (player1.choice == RPS.Paper && player2.choice == RPS.Scissors)
                || (player1.choice == RPS.Scissors && player2.choice == RPS.Rock)
            )
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
        /// <summary>
        /// This function simply checks if somebody won.
        /// Then prints the name of the Winner
        /// </summary>
        /// <param name="players">player array to check the player objects</param>
        /// <returns>boolean </returns>
        private bool SomebodyWon(Player[] players)
        {
            if (players[1].Score == maxScore || players[0].Score == maxScore)
            {
                Player player = players.Where(x => x.Score == maxScore).FirstOrDefault(); // can't be null, why does it say it could be null?
                Console.WriteLine($"{player.Name} has won!");
                Console.WriteLine("Press a button to continue.");
                Console.ReadKey(true);
                Console.Clear();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Handles player AND computer choices.
        /// Simulates a computer thinking by throwing the game's thread into sleep for a second.
        /// </summary>
        /// <param name="player">player object</param>
        /// <param name="isCPU">is true if player choose to play against computer</param>
        private static void HandlePlayerChoices(Player player, Boolean isCPU)
        {
            if (isCPU && player.Name == "CPU")
            {
                Console.WriteLine("The machine is making it's move....");
                player.choice = (RPS)(new Random().Next(3));
                Thread.Sleep(1000);
                Console.WriteLine($"The silicone ghost chose {GetEnumName(player.choice)}.");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine($"{player.Name}, it is your turn, choose your Destiny:");
                    Console.WriteLine("1. Rock? Press 1 or the r key");
                    Console.WriteLine("2. Paper? Choose 2 or the p key");
                    Console.WriteLine("3. Scissors? Use the 3 key or press s");
                    ConsoleKeyInfo chosen = Console.ReadKey(true);

                    if (chosen.Key == ConsoleKey.D1 || chosen.Key == ConsoleKey.R)
                    {
                        player.choice = RPS.Rock;
                        Console.WriteLine($"{player.Name} choose: {GetEnumName(player.choice)}");
                        return;
                    }
                    else if (chosen.Key == ConsoleKey.D2 || chosen.Key == ConsoleKey.P)
                    {
                        player.choice = RPS.Paper;
                        Console.WriteLine($"{player.Name} choose: {GetEnumName(player.choice)}");
                        break;
                    }
                    else if (chosen.Key == ConsoleKey.D3 || chosen.Key == ConsoleKey.S)
                    {
                        player.choice = RPS.Rock;
                        Console.WriteLine($"{player.Name} choose: {GetEnumName(player.choice)}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"You entered {chosen.KeyChar}");
                        Console.WriteLine(
                            "If you didn't want to play the game, you could've just closed the window."
                        );
                    }
                }
            }
        }
        /// <summary>
        /// Gets the Name of the enum's value that the player has.
        ///
        /// </summary>
        /// <param name="enumValue">Rock, Paper, Or Scissor enum</param>
        /// <returns>the name of the enum</returns>
        private static string GetEnumName(Enum enumValue)
        {
            DescriptionAttribute descriptionAttribute =
                enumValue
                    .GetType()
                    .GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null
                ? descriptionAttribute.Description
                : enumValue.ToString();
        }
    }
    /// <summary>
    /// Player character to play the game
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Score of the player
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// the choosen hand or RPS enum of the player
        /// </summary>
        public RPS choice { get; set; }
        /// <summary>
        /// instantiates the name and scoring
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            Name = name;
            Score = 0;
        }

        /// <summary>
        /// Not required, I should remove this
        /// </summary>
        public void Reset()
        {
            Score = 0;
        }
    }
}
