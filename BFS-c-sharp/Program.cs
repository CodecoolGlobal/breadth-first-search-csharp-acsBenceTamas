using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            RandomDataGenerator generator = new RandomDataGenerator();
            graph.AddRange(generator.Generate());

            foreach (var user in graph.Users)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
