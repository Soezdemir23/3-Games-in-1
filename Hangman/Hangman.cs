using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _3_Games_in_1.Hangman
{
    /// <summary>
    ///Hangman game
    /// </summary>
    public class Hangman
    {
        /// <summary>
        /// Maximum guesses are always six
        /// </summary>
        private int guesses { get; set; } = 6;
        /// <summary>
        /// An alphabet as a List of strings, easier to check and compare
        /// </summary>
        private List<string> alphabet { get; set; } =
            "abcdefghijklmnopqrstuvwxyz".ToCharArray().Select(c => c.ToString()).ToList();
        /// <summary>
        /// an empty list of guessed letters, were easier to use since the size was to be dynamic.
        /// </summary>
        private List<string> guessedAlphabet { get; set; } = new List<string>();
        /// <summary>
        /// incorrect amount of letters that have not been in the secretWord
        /// </summary>
        private List<string> incorrect { get; set; } = new List<string>();
        /// <summary>
        ///  Creates an object, creating the  hangman oject runs immediately the game.
        ///  Randomly chooses a secret word.
        ///  Asks player if he wants to start or exit the game and  handlees the rest of the logic
        /// </summary>
        /// <param name="content">the textfile turned into a list that is embedded into the binary</param>
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
                    string secretWord = content[new Random().Next(content.Count)];
                    gameStart(secretWord);
                }
                else if (choice.Key == ConsoleKey.D2 || choice.Key == ConsoleKey.E)
                {
                    Console.WriteLine("Goodbye!");
                    Console.Clear();
                    return;
                }else
                {
                    Console.WriteLine("Please enter a valid choice!");
                }

            }

        }
        /// <summary>
        /// Starts the game:
        /// <list type="bullet">
        /// Tells the player the rules and how long the word is
        /// Asks player for a letter
        /// If the player guessed correctly or not, show it in the console
        /// ttake it awa
        /// </list>
        /// </summary>
        /// <param name="secretWord">with no secretWord, the hangman has no purpose</param>
        private void gameStart(string secretWord)
        {
            Console.WriteLine(
                "Let's start the game! One secret word, 6 chances to get it right. Let's GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!"
            );
            Console.WriteLine($"This word has {secretWord.Length} letters, guess the first letter: ");
            while (true)
            {
                //check if the user lost all their chances
                //also check if the user has won the game
                if (SecretWord(guessedAlphabet, secretWord).Contains("_") == false || guesses == 0)
                {
                    break;
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                //check if it is a letter
                guessedAlphabet.Add(key.KeyChar.ToString());
                if (secretWord.Contains(key.KeyChar.ToString()) == false)
                {
                    incorrect.Add(key.KeyChar.ToString());
                    guesses--;
                }

                Console.WriteLine(
                    $"Word: {SecretWord(guessedAlphabet, secretWord)} | Remaining: {LeftAlphabet(alphabet, guessedAlphabet)} | Incorrect: {IncorrectGuesses()} | Guess: {key.KeyChar} | Chances Left: {guesses}"
                );
            }

            if (SecretWord(guessedAlphabet, secretWord).Contains("_") == false)
            {
                for (int i = 0; i < 12; i++)
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
                for (int i = 0; i < 12; i++)
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
            Console.WriteLine("Press any key to continue, or close program to exit application");
            Console.Read();
            Console.Clear();
            
        }
        /// <summary>
        /// returns the secretWord spaced and replaced with underscores fo letters that were not guessed yet.
        /// 
        /// secretWord: rain
        /// 
        /// guessedAlphabet: ai
        /// 
        /// result: _ a i _
        /// </summary>
        /// <param name="guessedAlphabet">every letter guessed so far</param>
        /// <param name="secretWord">the secret word</param>
        /// <returns>a partially hidden secret word</returns>
        public string SecretWord(List<string> guessedAlphabet, string secretWord)
        {
            string result = "";
            foreach (char c in secretWord.ToCharArray())
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
        /// <summary>
        /// shows the player letters that didn't fit into the secret word
        /// </summary>
        /// <returns>a string of letters that failed :(</returns>
        public string IncorrectGuesses()
        {
            string _ = "";
            foreach (string c in incorrect)
            {
                _ += c;
            }
            return _;
        }
        /// <summary>
        /// shows the leftover alphabet that haven't been trid yet.
        /// </summary>
        /// <param name="alphabet">the alphabet as a list</param>
        /// <param name="guessedAlphabet">the letters used so far</param>
        /// <returns>a string of letters not used yet</returns>
        public string LeftAlphabet(List<string> alphabet, List<string> guessedAlphabet)
        {
            string rest = "";
            foreach (string c in alphabet)
            {
                if (!guessedAlphabet.Contains(c))
                {
                    rest += c.ToString();
                }
            }
            return rest;
        }

    }
}
