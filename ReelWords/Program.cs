using ReelWords.FileReaders;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var allEnglishWords = await new EnglishDictionaryReader(File.OpenRead(Path.Combine("Resources", "american-english-large.txt"))).ReadAsync();
            var reels = await new ReelsReader(File.OpenRead(Path.Combine("Resources", "reels.txt"))).ReadAsync();

            var game = new Game(allEnglishWords, new Reels(reels));

            while (true)
            {
                Console.WriteLine("Available letters are: " + string.Join(',', game.GetAvailableLetters()));
                Console.WriteLine("Write '-' to exit");

                Console.Write("Type word to guess: ");

                var input = Console.ReadLine();

                if (input == "-")
                    break;

                var result = game.SubmitWord(input);

                Console.WriteLine(result.Message);
                Console.WriteLine();
            }

            Console.WriteLine($"Total score: {game.TotalScore}");
        }
    }
}