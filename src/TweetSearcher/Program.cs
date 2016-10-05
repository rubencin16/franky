using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace TweetSearcher
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var user = Auth.SetUserCredentials("NQKpOtz1V0xUvBMIMNT5nCfex", "pDj7vHOXZoYSRkC08qtydwg1GGmUPybsqbV6OVDC36lAHr6HvT", "88983495-mZr523Pc5xzFeU5RNdLXPkwC7FbdLPpb2izPZTQxr", "eQ0BMDlqL5XvWtF5d2wD1TuuoEtp0YLv5L8qNnGkg04w5");
            var userauth = User.GetAuthenticatedUser();
            var tweets = Timeline.GetHomeTimeline();

            Console.WriteLine("Choose an option\t1.-Publish tweet\t2.-Search for a tweet\t3.-Stream\tZ.-Exit");

            //Console.WriteLine($"The user: {userauth} and the tweets \r {tweets.Select(x => x.IsRetweet).FirstOrDefault()}");
            var input = Console.ReadLine().Trim().ToLower();
            int option = 0;
            bool number = int.TryParse(input, out option);
            //var matching = Search.SearchTweets("Dot Net");
            var stream = Stream.CreateFilteredStream();
            var track = string.Empty;
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
                            Console.WriteLine("Write the tweet to track:");
                            track = Console.ReadLine().Trim();
                            string[] tracks = track.Split(' ');
                            if (!string.IsNullOrEmpty(track))
                            {
                                Console.WriteLine("Do we use 1.-AND or 2.-OR operator?");
                                var operation = Console.ReadLine();
                                int newoption;
                                int.TryParse(operation, out newoption);
                                if (newoption > 0  && newoption <= 2)
                                {
                                    switch (newoption)
                                    {
                                        case 1:
                                            stream.AddTrack(track);
                                            break;
                                        case 2:
                                            foreach (var item in tracks)
                                            {
                                                stream.AddTrack(item);
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
                                //Console.WriteLine("Add a user to follow with the tweet?\n1.-Yes\t2.-No");
                                //var newoption = Console.Read();
                                //if (newoption == 1)
                                //{
                                //    Console.WriteLine("Add the user to follow:");
                                //    var newuser = Console.ReadLine();
                                    
                                //    stream.AddFollow(newuser);
                                //    stream.MatchingTweetReceived += (sender, arg) =>
                                //    {
                                //        Console.WriteLine($"=>We found a tweet that contains {track} and is this it: \n\t{arg.Tweet} ");
                                //    };
                                //}
                                //else
                                //{
                                    stream.MatchingTweetReceived += (sender, arg) =>
                                    {
                                        var print = newoption == 1 ? track : string.Join(" ", tracks);
                                        Console.WriteLine($"=>We found a tweet that contains: {print} and is this it: \n\t{arg.Tweet} ");
                                    };
                                //}
                                stream.StartStreamMatchingAllConditions();
                            }
                            else
                            {
                                Console.WriteLine("You need to write something to start streaming");
                                break;
                            }
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
            //foreach (var item in matching)
            //{
            //    Console.WriteLine($"Results of match {item}");
            //}           


            Console.ReadKey();
        }

        private static void Stream_MatchingTweetReceived(object sender, Tweetinvi.Events.MatchedTweetReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void StartStream(string track, bool stop)
        {
            
        }
        private static void ClearLine()
        {
            Console.Write(new string(' ', Console.BufferWidth - Console.CursorLeft + 1));
        }
    }
}
