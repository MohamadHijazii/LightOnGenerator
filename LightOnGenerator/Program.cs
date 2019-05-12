using System;
using System.Collections.Generic;

namespace LightOnGenerator
{

    class Program
    {
        static int low = 4;
        static int height = 12;

        static void printList(List<List<int>> cases)
        {
            int N = cases.Count;
            for (int i = 0; i < N; i++)
            {
                List<int> temp = cases[i];
                Console.Write(i + ": ");
                for (int j = 0; j < temp.Count; j++)
                {
                    Console.Write(temp[j] + " ");
                }
                Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {
            int n = 0;
            bool repeat = true;
            bool rep = true;

            while (rep)
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("Enter the number of lights (" + low + " -- " + height + "): ");
                    var tem = Console.ReadLine();
                    n = int.Parse(tem);
                } while (n < low || n > height);

                while (repeat)
                {
                    Console.Clear();
                    Generator g = new Generator(n, 0);
                    Console.WriteLine("This is the level generated: ");
                    printList(g.generate());
                    Console.WriteLine("Do you want to change it? (y or n): ");
                    string tem;
                    do
                    {
                        tem = Console.ReadLine();
                    } while (tem[0] != 'y' && tem[0] != 'n');
                    repeat = (tem[0] == 'y');
                }

                Agent agent = new Agent("test", 100000);
                Console.WriteLine("This is the solution of the case above: ");
                agent.solve(100000);
                agent.printSolution();

                Console.WriteLine("Would you like to add this level?(y or n): ");
                string temp;
                do
                {
                    temp = Console.ReadLine();
                } while (temp[0] != 'y' && temp[0] != 'n');
                if (temp[0] == 'y')
                {
                    Console.WriteLine("Add some notes: ");
                    string note = Console.ReadLine();
                    agent.save("test", note);
                    Console.WriteLine("level saved");
                }
                else
                {
                    Console.WriteLine("Level aborted!");
                }

                Console.WriteLine("Would you like to add more?");
                string tempo;
                do
                {
                    tempo = Console.ReadLine();
                } while (tempo[0] != 'y' && tempo[0] != 'n');
                rep = (tempo[0] == 'y');
            } while (rep) ;
            Console.ReadLine();
        }
    }
}
