using _3_Games_in_1.Rock_Paper_Scissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1._15_Puzzle
{
    /// <summary>
    /// Npuzzle is a game where you have to order the numbers from 1 to n in a square grid.
    /// </summary>
    public class NPuzzle
    {
        /// <summary>
        /// The numbers that will be used in the game.
        /// </summary>
        private int[] numbers { get; set; }

        /// <summary>
        /// The position of the player in the game.
        /// </summary>
        int[] playerPosition { get; set; }

        /// <summary>
        /// The game board that will be used in the game.
        /// </summary>
        private int[,] gameBoard { get; set; } = { };

        /// <summary>
        /// Instantiantig the game with a default size of 9 or more if the perfect square is given.
        /// Check if the game is solvable, if not, shuffle the numbers until it is.
        ///
        /// </summary>
        /// <param name="size">has to be a perfect square candidate, else it reverts to 9</param>
        public NPuzzle(int size)
        {
            if (size % Math.Sqrt(size) != 0)
            {
                Console.WriteLine("Size didn't have a square root\nUsing default size of 9");
                numbers = ArrayCreate(9);
            }
            else
            {
                numbers = ArrayCreate(size);
            }
            GameFieldPrep();
            while (isSolvable(gameBoard) == false)
            {
                numbers = KnuthShuffle(numbers, new Random());
                GameFieldPrep();
            }

            numbers = KnuthShuffle(numbers, new Random());

            while (true)
            {
                GameField();
                PlayerMovement(gameBoard, playerPosition); //playerPosition will never be null in this context. Get out of here bro!
                if (checkOrdered(numbers) == true)
                {
                    Console.WriteLine("Ordered,");
                    GameField();
                    Console.ReadKey();
                    break;
                }
            }
        }

        /// <summary>
        /// Creates a 1-dimensional array with the numbers from 0 to n.
        /// </summary>
        /// <param name="n">the length of the array</param>
        /// <returns>returns an array the size n</returns>
        public int[] ArrayCreate(int n)
        {
            int[] arr = new int[n]; // instantiate an empty int array

            for (int i = 0; i < n; i++)
            {
                arr[i] = i;
            }
            return arr;
        }

        /// <summary>
        /// Prepare the gamefield that will be used to display on the console and used for various game logic checks.
        /// </summary>
        /// <returns>the perfect square of the size of a single dimensional array</returns>
        private int GameFieldPrep()
        {
            int n = (int)Math.Sqrt(numbers.Length);
            gameBoard = new int[n, n];
            int k = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    gameBoard[i, j] = numbers[k++];
                }
            }
            return n;
        }

        /// <summary>
        /// Displays the array and checks where the player resides to create a gamefield.
        /// This gamefield displays the player as a green square and the possible moves as red squares.
        ///
        /// </summary>
        public void GameField()
        {
            int n = GameFieldPrep();
            gameBoard = new int[n, n];
            int k = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    gameBoard[i, j] = numbers[k++];
                }
            }
            playerPosition = FindZero(gameBoard);
            // gameboard drawing
            for (int i = 0; i < n; i++)
            {
                //outer
                Console.Write("+");
                for (int m = 0; m < 7 * n - 1; m++)
                {
                    Console.Write('-');
                }
                Console.WriteLine("+");

                for (int j = 0; j < n; j++)
                {
                    Console.Write("|");
                    if (i == playerPosition[0] && j == playerPosition[1])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("      ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] - 1 && j == playerPosition[1])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] + 1 && j == playerPosition[1])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] + 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write("      ");
                    }
                }
                Console.WriteLine("|");

                for (int j = 0; j < n; j++)
                {
                    if (i == playerPosition[0] && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{gameBoard[i, j]:D2}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] - 1 && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{gameBoard[i, j]:D2}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] + 1 && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{gameBoard[i, j]:D2}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] - 1)
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{gameBoard[i, j]:D2}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] + 1)
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{gameBoard[i, j]:D2}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write($"|  {gameBoard[i, j]:D2}  ");
                    }
                }
                Console.WriteLine("|");

                for (int j = 0; j < n; j++)
                {
                    if (i == playerPosition[0] && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] - 1 && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] + 1 && j == playerPosition[1])
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] - 1)
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1] + 1)
                    {
                        Console.Write("|");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write("|      ");
                    }
                }
                Console.WriteLine("|");
            }

            Console.Write("+");
            for (int m = 0; m < 7 * n - 1; m++)
            {
                Console.Write('-');
            }
            Console.WriteLine("+");
        }

        /// <summary>
        /// sets the player movement starting from 00.
        /// The moves by rotating.
        /// </summary>
        /// <param name="gameboard">the gameboard the player is playing in</param>
        /// <param name="playerposition">the player's coordinate on the gameboard</param>
        public void PlayerMovement(int[,] gameboard, int[] playerposition)
        {
            ConsoleKey movement = Console.ReadKey().Key;

            switch (movement)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    //if the user has not left the field with the next step, go up

                    if (playerposition[0] > 0)
                    {
                        int above = gameboard[playerposition[0] - 1, playerposition[1]];
                        int center = gameboard[playerposition[0], playerposition[1]];

                        int aboveIndex = Array.IndexOf(numbers, above);
                        int centerIndex = Array.IndexOf(numbers, center);

                        numbers[aboveIndex] = center;
                        numbers[centerIndex] = above;
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (playerposition[0] < Math.Sqrt(gameBoard.Length) - 1)
                    {
                        int below = gameboard[playerposition[0] + 1, playerposition[1]];
                        int center = gameboard[playerposition[0], playerposition[1]];

                        int belowIndex = Array.IndexOf(numbers, below);
                        int centerIndex = Array.IndexOf(numbers, center);

                        numbers[belowIndex] = center;
                        numbers[centerIndex] = below;
                    }
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (playerposition[1] > 0)
                    {
                        int left = gameboard[playerposition[0], playerposition[1] - 1];
                        int center = gameboard[playerposition[0], playerposition[1]];

                        int leftIndex = Array.IndexOf(numbers, left);
                        int centerIndex = Array.IndexOf(numbers, center);

                        numbers[leftIndex] = center;
                        numbers[centerIndex] = left;
                    }
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (playerposition[1] < Math.Sqrt(gameboard.Length) - 1)
                    {
                        int right = gameboard[playerposition[0], playerposition[1] + 1];
                        int center = gameboard[playerposition[0], playerposition[1]];

                        int rightIndex = Array.IndexOf(numbers, right);
                        int centerIndex = Array.IndexOf(numbers, center);

                        numbers[rightIndex] = center;
                        numbers[centerIndex] = right;
                    }
                    break;
                default:

                    break;
            }
        }

        /// <summary>
        /// Check if the numbers are in order.
        /// Requirement to win the game
        /// </summary>
        /// <param name="numbers">the array of numbers to check</param>
        /// <returns>boolean on numbers ordered</returns>
        public bool checkOrdered(int[] numbers)
        {
            int count = 1;
            for (int i = 0; i < numbers.Length - 2; i++)
            {
                if (numbers[i] != count)
                    return false;
                count++;
            }
            if (0 != numbers[numbers.Length - 1])
                return false;
            return true;
        }

        /// <summary>
        /// find where zero is in order to return the coordinates and assign them to the player
        /// </summary>
        /// <param name="gameboard">gameboard to find the zero caret</param>
        /// <returns>an array of 2 elements, x,y position</returns>
        public int[] FindZero(int[,] gameboard)
        {
            int[] zeroPosition = new int[2];
            for (int i = 0; i < Math.Sqrt(gameboard.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(gameboard.Length); j++)
                {
                    if (gameboard[i, j] == 0)
                    {
                        zeroPosition[0] = i;
                        zeroPosition[1] = j;
                    }
                }
            }
            return zeroPosition;
        }

        /// <summary>
        /// KnuthShuffle is a shuffle algorithm that is used to shuffle the numbers in the gameboard
        ///
        /// </summary>
        /// <param name="array">the numbers array</param>
        /// <param name="r">object of class Random</param>
        /// <returns></returns>
        public static int[] KnuthShuffle(int[] array, Random r)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                int temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
            return array;
        }

        //Borrowed from https://www.geeksforgeeks.org/check-instance-15-puzzle-solvable/
        //

        /// <summary>
        /// Get the count of inverse elements in the array
        /// </summary>
        /// <param name="arr">an array</param>
        /// <param name="gameboard">the current gameboard</param>
        /// <returns>the count of inverse elements</returns>
        public static int GetInvCount(int[] arr, int[,] gameboard)
        {
            int inv_count = 0;
            for (int i = 0; i < Math.Sqrt(gameboard.Length * gameboard.Length) - 1; i++)
            {
                for (int j = i + 1; j < Math.Sqrt(gameboard.Length * gameboard.Length) - 1; j++)
                {
                    // count pairs(arr[i], arr[j]) such that
                    // i < j but arr[i] > arr[j]
                    if (arr[j] != 0 && arr[i] != 0 && arr[i] > arr[j])
                        inv_count++;
                }
            }
            return inv_count;
        }

        /// <summary>
        /// finds the x position based on the gameboard
        ///
        /// </summary>
        /// <param name="gameboard"> the gameboard </param>
        /// <returns>the length of the array until x</returns>
        static int findXPosition(int[,] gameboard)
        {
            //start from bottom-right corner of matrix
            for (int i = (int)Math.Sqrt(gameboard.Length) - 1; i >= 0; i--)
            {
                for (int j = (int)Math.Sqrt(gameboard.Length) - 1; j >= 0; j--)
                {
                    if (gameboard[i, j] == 0)
                    {
                        return gameboard.Length - i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// checks if the shuffled board is solvable
        /// </summary>
        /// <param name="gameboard">board to check</param>
        /// <returns>boolean true or false</returns>
        static bool isSolvable(int[,] gameboard)
        {
            int[] arr = new int[(int)Math.Sqrt(gameboard.Length * gameboard.Length)];
            int k = 0;
            for (int i = 0; i < Math.Sqrt(gameboard.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(gameboard.Length); j++)
                {
                    arr[k++] = gameboard[i, j];
                }
            }

            // Count inversions in given puzzle
            int invCount = GetInvCount(arr, gameboard);

            //if grid is odd, return true if inversion
            if (gameboard.Length % 2 == 1)
            {
                return invCount % 2 == 0;
            }
            else // grid is even
            {
                int pos = findXPosition(gameboard);
                if (pos % 2 != 0)
                {
                    return invCount % 2 == 0;
                }
                else
                {
                    return invCount % 2 != 0;
                }
            }
        }
    }
}
