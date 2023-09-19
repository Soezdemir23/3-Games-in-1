// See https://aka.ms/new-console-template for more information

using _3_Games_in_1._15_Puzzle;
using _3_Games_in_1.Hangman;
using _3_Games_in_1.Rock_Paper_Scissors;
using System.Reflection;

Assembly assembly = Assembly.GetExecutingAssembly();

// We only have one embeded resource, so let's get it
string wordFile = assembly.GetManifestResourceNames()[0];
StreamReader streamReader = new StreamReader(
    wordFile.Replace("_3_Games_in_1.words.txt", "words.txt")
);
List<string> content = new List<string>();

// now from streamReader, read the content line by line and add it to the content list
while (!streamReader.EndOfStream)
{
    content.Add(streamReader.ReadLine());
}

while (true)
{
    Console.WriteLine("Welcome to the big 3-in-1 Game Section!\nChoose your game or exit!");
    Console.WriteLine("1. [R]ock-Paper-Scissors\n2. [N]-Puzzle\n3. [H]angman\n4. [E]xit");
    Console.WriteLine("Choose by the numbers, or the letters in the bracket.");

    var choice = Console.ReadKey(true).Key;
    Console.Clear();
    if (choice == ConsoleKey.D1 || choice == ConsoleKey.R)
    {
        new GameLogicRPS();
    }
    else if (choice == ConsoleKey.D2 || choice == ConsoleKey.N)
    {
        Console.WriteLine("How big do you want the puzzle to be?");
        string size = Console.ReadLine();
        size = size == null? "9":size.Trim();
        if (int.TryParse(size, out int num) == true)
        {
            new NPuzzle(num);
            
        } 
    
    }
    else if (choice == ConsoleKey.D3 || choice == ConsoleKey.H)
    {
        new Hangman(content);
    }
    else if (choice == ConsoleKey.D4 || choice == ConsoleKey.E)
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Please enter a valid choice!");
    }
}
