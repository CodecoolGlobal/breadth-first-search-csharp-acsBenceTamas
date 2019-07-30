using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rng = new Random();
            Graph graph = new Graph();
            RandomDataGenerator generator = new RandomDataGenerator();
            graph.AddRange(generator.Generate(4));
            var users = new List<UserNode>(graph.Users);
            int max = Math.Min(3, users.Count);
            Console.WriteLine($"Total number of people in the graph: {users.Count}");

            for (int i = 0; i < max; i++)
            {
                var user = users[rng.Next(users.Count)];
                if (i == 0)
                {
                    var friends = graph.GetFriendsOfFriends(user, 2);
                    Console.WriteLine($"The closest people to {user} are: ");
                    foreach (var friend in friends)
                    {
                        Console.WriteLine($"\t{friend}");
                    }
                }

                for (int j = 0; j < max; j++)
                {
                    var otherUser = users[rng.Next(users.Count)];
                    int distance = graph.GetDistance(user, otherUser);
                    var paths = graph.GetShortestPaths(user, otherUser);
                    var sb = new StringBuilder();

                    foreach (var path in paths)
                    {
                        sb.Append($"\t ({path.Count})");
                        sb.Append("[");
                        foreach (var userNode in path)
                        {
                            sb.Append(userNode);
                            sb.Append(",");
                        }
                        sb.Append("]\n");
                    }

                    Console.WriteLine($"{user} is {distance} friends away from {otherUser}");
                    Console.WriteLine($"The shortest routes between them are: \n{sb.ToString()}");
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
