using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1.Hangman
{
    /// <summary>
    ///
    /// </summary>
    public class Hangman
    {
        private int guesses { get; set; } = 6;
        private List<string> alphabet { get; set; } =
            "abcdefghijklmnopqrstuvwxyz".ToCharArray().Select(c => c.ToString()).ToList();

        private List<string> guessedAlphabet { get; set; } = new List<string>();
        private List<string> incorrect { get; set; } = new List<string>();

        public Hangman(List<string> content)
        {

            while (true)
            {
                Console.WriteLine("Welcome to Hangman!");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. [S]tart the game\n2. [E]xit");
                Console.WriteLine("Choose by the numbers, or the letters in the bracket.");
                ConsoleKeyInfo choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.D1 || choice.Key == ConsoleKey.S)
                {
                    var secretWord = content[new Random().Next(content.Count)];
                    gameStart(secretWord);
                }
                else if (choice.Key == ConsoleKey.D2 || choice.Key == ConsoleKey.E)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }else
                {
                    Console.WriteLine("Please enter a valid choice!");
                }

            }

        }

        private void gameStart(string secretWord)
        {
            Console.WriteLine(
                "Let's start the game! One secret word, 6 chances to get it right. Let's GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!"
            );
            Console.WriteLine($"This word has {secretWord.Length} letters, guess the first word: ");
            while (true)
            {
                //check if the user lost all their chances
                //also check if the user has won the game
                if (SecretWord(guessedAlphabet, secretWord).Contains("_") == false || guesses == 0)
                {
                    break;
                }
                var key = Console.ReadKey(true);
                //check if it is a letter
                guessedAlphabet.Add(key.KeyChar.ToString());
                if (secretWord.Contains(key.KeyChar.ToString()) == false)
                {
                    incorrect.Add(key.KeyChar.ToString());
                    guesses = -1;
                }

                Console.WriteLine(
                    $"Word: {SecretWord(guessedAlphabet, secretWord)} | Remaining: {LeftAlphabet(alphabet, guessedAlphabet)} | Incorrect: {IncorrectGuesses()} | Guess: {key.KeyChar} | Chances Left: {guesses}"
                );
            }

            if (SecretWord(guessedAlphabet, secretWord).Contains("_") == false)
            {
                for (global::System.Int32 i = 0; i < 12; i++)
                {
                    int random = new Random().Next(3);
                    if (random == 1)
                        Console.WriteLine("You Won!");
                    else if (random == 2)
                        Console.WriteLine("You Won!".ToLower());
                    else
                        Console.WriteLine("You won".ToUpper());
                }
            }
            else
            {
                for (global::System.Int32 i = 0; i < 12; i++)
                {
                    int random = new Random().Next(3);
                    if (random == 1)
                        Console.WriteLine("You Lost!");
                    else if (random == 2)
                        Console.WriteLine("You Lost!".ToLower());
                    else
                        Console.WriteLine("You Lost!".ToUpper());
                }
                Console.WriteLine("Secretword was: " + secretWord);
            }

            Console.WriteLine("\n");
            
        }

        public string SecretWord(List<string> guessedAlphabet, string secretWord)
        {
            string result = "";
            foreach (var c in secretWord.ToCharArray())
            {
                if (guessedAlphabet.Contains(c.ToString()))
                {
                    result += c.ToString() + " ";
                }
                else
                {
                    result += "_ ";
                }
            }

            return result;
        }

        public string IncorrectGuesses()
        {
            string _ = "";
            foreach (var c in incorrect)
            {
                _ += c;
            }
            return _;
        }

        public string LeftAlphabet(List<string> alphabet, List<string> guessedAlphabet)
        {
            string rest = "";
            foreach (var c in alphabet)
            {
                if (!guessedAlphabet.Contains(c))
                {
                    rest += c.ToString();
                }
            }
            return rest;
        }

        /**
         * I need following things:
         *
         * 1. User enters a key
         * 2. Key is checked if it's part of the alphabet
         * 2.1. if not -> warn them up to three times,
         *  then take their lives away until they start to guess right
         * 2.2. if it is -> add to the guessed characters and add it to
         *  a function that checks the alphabet list for what hasn't been tried,
         *  returning a list of characters that haven't been tried yet
         *
         * 3. Key is checked if it's part of the guessed List of characters
         * 4. Key is checked if it's part of the word
         *
         *
         * */
    }
}
