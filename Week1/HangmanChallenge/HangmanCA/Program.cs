using System;

namespace HangmanCA
{
    class Program
    {
        public static int maxGuesses = 6;

        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("1: New Game");
                Console.WriteLine("2: Set Max Wrong Guesses");
                // Console.WriteLine("3: Set Max Word Length");
                Console.WriteLine("4: Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        runGame();
                        break;
                    case "2":
                        Console.WriteLine("Enter your number for max wrong guesses (min 3)");
                        string numInput = Console.ReadLine();
                        try
                        {
                            int num = int.Parse(numInput);
                            if (num < 3)
                            {
                                Console.WriteLine("Enter a number greater than 3");
                            }
                            else
                            {
                                maxGuesses = num;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Enter a number a number");
                        }
                        break;
                    case "4":
                        return;
                        break;
                    default:
                        Console.WriteLine("Enter a number");
                        break;
                }
            }
        }

        public static void runGame()
        {
            // indicate application startup
            Console.WriteLine("Starting Hangman Game...");

            // placeholder for game setup and initlialization
            Console.WriteLine("Welcome to Hangman!");

            // placeholder for game loop
            Dictionary<char, bool> guessedLetters = new Dictionary<char, bool>();
            foreach (char c in "abcdefghijklmnopqrstuvwxyz")
            {
                guessedLetters[c] = false;
            }
            int guesses = 0;
            // const int maxGuesses = 6;
            // string[] words = new string[] { "Hello", "World", "Peace", "Alphabet", "Ratatouille" };
            Random random = new Random();
            string selectedWord = getRandomWord();
            string selectedWordLower = selectedWord.ToLower();
            string guessedWord = getCurrentWord(guessedLetters, selectedWord);

            while (guesses != maxGuesses && selectedWord != guessedWord)
            {
                Console.WriteLine("\r\r\r\r------------------------------------------------");
                Console.WriteLine($"\r\r\r\rWord: {guessedWord}");
                Console.WriteLine($"\r\r\r\rGuessed Letters: {string.Join(',', getGuessedLetters(guessedLetters))}");
                Console.WriteLine($"\r\r\r\rWrong guesses: {guesses} out of {maxGuesses}");

                // IN LOOP

                char guess = getOneLetter(guessedLetters);
                if (guess == ' ')
                {
                    Console.WriteLine("Enter a letter!");
                    continue;
                }
                // If we already guessed, re-ask
                else if (guessedLetters[guess] == true)
                {
                    Console.WriteLine("You already entered this letter. Enter a new letter!");
                    continue;
                }
                // If we guess and its right, continue
                else if (selectedWordLower.Contains(guess))
                {
                    Console.WriteLine("Correct!");
                    guessedLetters[guess] = true;
                }
                // If we guess and its wrong, increment guesses
                else
                {
                    Console.WriteLine("Wrong!");
                    guesses++;
                    guessedLetters[guess] = true;
                }
                guessedWord = getCurrentWord(guessedLetters, selectedWord);
            }

            Console.WriteLine("------------------------------------------------");
            if (selectedWord == guessedWord)
            {
                Console.WriteLine($"Congratulations! The Word is {selectedWord}!");
            }
            else
            {
                Console.WriteLine($"Oh no, better luck next time. The Word was {selectedWord}!");
            }
        }

        public static List<char> getGuessedLetters(Dictionary<char, bool> guessedLetters)
        {
            List<char> letters = new List<char>();
            foreach (char c in "abcdefghijklmnopqrstuvwxyz")
            {
                if (guessedLetters[c])
                {
                    letters.Add(c);
                }
            }
            // Console.WriteLine($"Guessed Letters: {string.Join(',', letters)}");
            return letters;
        }

        public static char getOneLetter(Dictionary<char, bool> guessedLetters)
        {
            string inputLine = Console.ReadLine();
            if (inputLine.Length != 1) return ' ';
            char c = inputLine[0];
            c = char.ToLower(c);
            if (!guessedLetters.ContainsKey(c)) return ' ';
            return c;
        }

        public static string getCurrentWord(Dictionary<char, bool> guessedLetters, string selectedWord)
        {
            string currentGuess = "";
            foreach (char c in selectedWord)
            {

                if (guessedLetters[char.ToLower(c)])
                {
                    currentGuess += c;
                }
                else
                {
                    currentGuess += "_";
                }
            }
            return currentGuess;
        }

        public static string getRandomWord()
        {
            string filePath = "words_alpha.txt";
            int lineCount = File.ReadLines(filePath).Count();
            Random random = new Random();
            int targetLine = random.Next(lineCount);

            string selectedWord = File.ReadLines(filePath).Skip(targetLine).First();
            return selectedWord;
        }
    }
}
