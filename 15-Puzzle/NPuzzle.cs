using _3_Games_in_1.Rock_Paper_Scissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1._15_Puzzle
{
    public class NPuzzle
    {
        /// <summary>
        /// Usually 15, but can be changed to any number in the future
        /// 0 is the empty space
        /// </summary>
        private int[] numbers { get; set; }
        int[] playerPosition { get; set; } = { 0, 0};
        private int[,] position { get; set; }
        private int[,] gameBoard { get; set; } = { };
        

        public NPuzzle(int size)
        {
            if (size % Math.Sqrt(size) != 0)
            {
                Console.WriteLine("Size didn't have a square root\nUsing default size of 9");
                numbers = ArrayLength(9);
                
            }
            else 
            {
            numbers =  ArrayLength(size);
            }

            numbers = KnuthShuffle(numbers, new Random());

            while (true)
            {
                GameField();
                PlayerMovement(gameBoard, playerPosition);
                Console.Clear();
            }
        }


        public int[] ArrayLength(int n)
        {
            int[] arr = new int[n]; // instantiate an empty int array
            
            for (int i = 0;i < n; i++)
            {
                arr[i] = i;
            }
            return arr;
            
        }


        /**
         * TODO:
         * 0. Warn the user that the terminal is not playing well, if the gamefield is greater than the visible terminal
         * 1. add pulsing light near the player which it can move or change numbers with.
         *  We can calculate this and save it, or we can calculate it in the moment
         *  The idea is simple:
         *  Take the position of the player and show what it can change left, right, up and down from it's situation
         *  Let's see how the implementaion goes.

         * 2. increase the coloring of the player location -> DONE
         * 3. implement the function to change the numbers.
         */

        /// <summary>
        /// The game field should look like this:
        /// +------+------+------+------+
        /// |      |      |      |      |  
        /// |  01  |  02  |  03  |  04  |
        /// |      |      |      |      |
        /// +------+------+------+------+
        /// |      |      |      |      |
        /// |  05  |  06  |  07  |  08  |
        /// |      |      |      |      |
        /// +------+------+------+------+
        /// |      |      |      |      |
        /// |  09  |  10  |  11  |  12  |
        /// |      |      |      |      |
        /// +------+------+------+------+
        /// |      |      |      |      |
        /// |  13  |  14  |  15  |      |
        /// |      |      |      |      |
        /// +------+------+------+------+
        /// 29 - 16 = 
        /// </summary>
        public void GameField()
        {
            /**
             * Create a 2D array to represent the game field and use for loop to fill the spaces with the numbers.
             * Use Console.WriteLine to print the content; StringBuilder to help print the borders 
             * 
             */
            int n = (int)Math.Sqrt(numbers.Length);
            this.gameBoard = new int[n, n];
            int k = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    gameBoard[i, j] = numbers[k++];
                }
            }

            for (int i = 0; i < n; i++)
            {
                
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
                    }else if(i == playerPosition[0]-1 &&  j == playerPosition[1])
                    {
                        
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0] && j == playerPosition[1]-1)
                    {

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("    ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (i == playerPosition[0]+1 && j == playerPosition[1])
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
                    else if (i == playerPosition[0]-1 && j == playerPosition[1])
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
                    else if (i == playerPosition[0]+1 && j == playerPosition[1])
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


        public void AdjacentChoices()
        {

        }
        
        public void PlayerMovement(int[,] gameboard, int[] playerposition)
        {
            ConsoleKey movement = Console.ReadKey().Key;

            switch (movement)
            {
                case ConsoleKey.UpArrow:  
                case ConsoleKey.W:
                    //if the user has not left the field with the next step, go up
                    if (playerposition[0]-1 >=0)
                    {
                        playerposition[0]--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (playerposition[0]+1 < Math.Sqrt(gameboard.Length)) 
                    {
                        playerposition[0]++;
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (playerposition[1] - 1 >= 0)
                    {
                        playerposition[1]--;
                    }
                        break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (playerposition[1] + 1 < Math.Sqrt(gameboard.Length))
                    {
                        playerposition[1]++;
                    }
                    break;
            }
        }

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
    }
}
