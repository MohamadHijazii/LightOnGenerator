using System;
using System.Collections.Generic;
using System.IO;

namespace LightOnGenerator
{
    public class Agent
    {
        private List<List<int>> cases;
        private int N;
        private bool[] states;
        private int tries;
        private string solution = "";
        string tempSol = "";
        int last;
        int steps = int.MaxValue;

        public Agent(int n, List<List<int>> casess, int tries)
        {
            cases = casess;
            N = n;
            states = new bool[N];
            for (int i = 0; i < N; i++)
                states[i] = false;
            this.tries = tries;

        }

        public Agent(string path, int tr)
        {
            Model model = new Model(path);
            tries = tr;
            cases = model.read();
            N = model.getN();
            states = new bool[N];
            ini();
            

        }



        public void save(string path,string note) {
            Model model = new Model(path);
            model.saveToMongo(cases,note,solution);
        }

        private void switcch_state(int pos)
        {
            states[pos] = !states[pos];

        }

        private bool solvebale()
        {
            for (int i = 0; i < N; i++)
                if (!states[i])
                    return false;
            return true;
        }

        private void press(int n)
        {
            List<int> temp = cases[n];
            for (int i = 0; i < temp.Count; i++)
                switcch_state(temp[i]);
        }

        public bool solve(int n)
        {
            while (n-- > 0)
            {
                ini();
                int a = solve();
                //tries = a;
                if (a < steps && a > 0)
                {
                    steps = a;
                    solution = tempSol;
                }
            }
            return !solution.Equals("");
        }


        private int solve()
        {
            Random rand = new Random();
            int r;
            int n = 0;
            tempSol = "";
            while (n++ < tries)
            {
                do
                {
                    r = rand.Next(0, N);
                } while (r == last);
                last = r;
                press(r);
                tempSol += (r + " ");
                if (solvebale())
                    return n;
            }
            return -1;
        }

        public void printSolution()
        {
            if (solution.Equals(""))
            {
                Console.WriteLine("Maby no solution");
                return;
            }
            Console.WriteLine("Min number of steps: " + steps);
            Console.WriteLine(solution);
        }

        private string getState(int pos) {
            if (states[pos])
                return "ON";
            return "OFF";
        }

        public string getStates()
        {
            string s = "";
            for (int i = 0; i < N; i++)
            {
                s += (i +": "+ getState(i)) + "\t";
            }
            return s;
        }

        private void ini()
        {
            for(int i = 0; i < N; i++)
            {
                states[i] = false;
            }
        }

    }
}
