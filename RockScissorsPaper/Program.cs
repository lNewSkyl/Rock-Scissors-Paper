using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RockScissorsPaper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length<3||args.Length%2==0)
            {
                Console.WriteLine("YOU NEED TO ENTER >2 AND NOT EVEN ARGUMENTS TO PLAY!!!!\n");
                return;
            }
            var collection = new TurnCollection(args);
            while (true)
            {
                byte[] tokenData = Randomizer.GetBytes(32);
                var min = collection.Turns.Min(x => x.Button);
                var max = collection.Turns.Max(x => x.Button);
                var compChoice = Randomizer.GetRandomNumber(min, max);
                var choiceTurn = collection.Turns.First(x => x.Button == compChoice);
                var choiceHash = Randomizer.GetHash(tokenData, choiceTurn.Name);
                Console.WriteLine($"HMAC: {choiceHash} \nAvailable moves: ");
                foreach (var t in collection.Turns)
                {
                    Console.WriteLine($"{t.Button} - {t.Name}");
                }
                Console.WriteLine("0 - exit \n? -help\nEnter your move:");
                var userKey = Console.ReadLine();
                if (userKey == "?")
                {
                    var table = collection.GetTable();
                    TableHelper.Draw(table);
                }
                else if (Int32.TryParse(userKey, out var userInt))
                {
                    if (userInt == 0)
                    {
                        return;
                    }
                    else if (userInt > max || userInt < min)
                    {
                        Console.WriteLine($"\nYou should choose between {min} and {max} \n");
                        continue;
                    }
                    else
                    {
                        var result = collection.GameResult((int)compChoice, userInt);
                        var userTurn = collection.Turns.First(x => x.Button == userInt);
                        var compTurn = collection.Turns.First(x => x.Button == compChoice);
                        Console.WriteLine($"You choose: {userTurn.Name}\nComputer choose: {compTurn.Name}");
                        Console.WriteLine(result);
                        Console.WriteLine(Encoding.ASCII.GetString(tokenData));
                        Console.WriteLine("\n\n");
                    }
                }
            }
        }

    }
}
