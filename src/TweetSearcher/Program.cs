using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TweetSearcher.Helpers;

namespace TweetSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AccountData account = new AccountData();
            Console.ForegroundColor = ConsoleColor.White;
            GetUserInput();
            Console.ReadKey();
        }

        static void GetUserInput()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = originalColor;
            Console.WriteLine("Choose an option\n\t1.-Publish tweet\t2.-Search for a tweet\t3.-Stream\tZ.-Exit");
            var input = Console.ReadLine().Trim().ToLower();
            int option = 0;
            bool number = int.TryParse(input, out option);
            while (number)
            {
                if (number && (option > 0 && option <= 3))
                {
                    ClearLine();
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("This option is not for you...sorry\nTry another one");
                            break;
                        case 2:
                            Console.WriteLine("Ña");
                            break;
                        case 3:
                            StartStream();
                            break;
                        default:
                            Console.WriteLine("There was an error ");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(">Don't try to be too smart...");
                }
                break;
            }
            if (!number && input == "z")
            {
                Console.WriteLine("Good bye");
                Environment.Exit(0);
            }
        }

        static void StartStream()
        {
            string userInput = string.Empty;
            var track = string.Empty;
            var stream = Stream.CreateFilteredStream();
            Console.WriteLine("Write the tweet/hashtag to track separated by an space:");
            track = Console.ReadLine().Trim();
            string[] tracks = track.Split(' ');
            if (!string.IsNullOrEmpty(track))
            {
                Console.WriteLine($"Is this right: {track}? \tHit Enter to continue\tC.-Cancel");
                userInput = Console.ReadLine().Trim().ToLower();
                if (userInput == "c")
                {
                    stream.StopStream();
                    Console.Clear();
                    GetUserInput();
                    ClearLine(2);
                }
                else
                {
                    ClearLine(3);
                    Console.Write("Operators: 1.-AND 2.-OR\n");
                    var operation = Console.ReadLine();
                    int newoption;
                    int.TryParse(operation, out newoption);
                    ClearLine(3);
                    if (newoption > 0 && newoption <= 2)
                    {
                        switch (newoption)
                        {
                            case 1:
                                stream.AddTrack(track);
                                break;
                            case 2:
                                if (tracks.Count() > 1)
                                {
                                    foreach (var item in tracks)
                                    {
                                        stream.AddTrack(item);
                                    }
                                }
                                else
                                {
                                    stream.AddTrack(track);
                                }
                                break;
                            default:
                                Console.WriteLine("There's no option for this");
                                break;
                        }
                        ClearLine();
                    }
                    else
                    {
                        Console.WriteLine("You choose and invalid option\nGood bye");
                        Environment.Exit(0);
                    }
                    stream.MatchingTweetReceived += (sender, arg) =>
                    {
                        var print = newoption == 1 ? track : string.Join(" ", tracks);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"=>We found a tweet that contains: {print} and this is it:");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" {arg.Tweet}");
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    };
                    stream.StartStreamMatchingAllConditions();

                }
            }
            else
            {
                Console.WriteLine("You need to write something to start streaming");
            }
        }

        static void Stream_MatchingTweetReceived(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ClearLine(int position = 1)
        {
            Console.Write(new string(' ', Console.BufferWidth - Console.CursorLeft + position));
            Console.SetCursorPosition(0, Console.CursorTop - position);
            Console.Write(new string(' ', Console.BufferWidth - Console.CursorLeft + position));
        }
    }
}
