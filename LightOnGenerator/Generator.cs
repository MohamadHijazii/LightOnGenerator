using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LightOnGenerator
{
    public class Generator
    {
        private int N { get; set; }
        private int difficulty;
        private List<List<int>> lights { get; set; }
        private List<int> times;
        
        public Generator(int n,int d)
        {
            Model model = new Model("test");
            N = n;
            difficulty = d;
            times = suffle<int>(model.getTimes(N));
            lights = generateAll();
            model.write(lights);
        }

        public List<List<int>> generate() {
            return lights;
        }

        private List<List<int>> generateAll() {
            List<List<int>> res = new List<List<int>>();
            for(int i = 0; i < N; i++)
            {
                res.Add(generateOne(i, times[i]));
            }
            return res;
        }

        private List<int> generateOne(int pos,int n)
        {
            List<int> ls = new List<int>();
            ls.Add(pos);
            Random rand = new Random();
            int r;
            for (int i = 0; i < n; i++)
            {
                do
                {
                     r = rand.Next(0, N - 1);                   
                } while (belong(ls, r));
                ls.Add(r);
            }
            return ls;
        }

        private bool belong(List<int> ls , int v)
        {
            for(int i = 0; i < ls.Count; i++)
            {
                if (ls[i] == v)
                    return true;
            }
            return false;
        }

        public List<E> suffle<E>(List<E> inputList)
        {
            List<E> res = new List<E>();
            Random r = new Random();
            int n = inputList.Count;
            while(n > 0)
            {
                int ind = r.Next(0, n);
                res.Add(inputList[ind]);
                inputList.RemoveAt(ind);
                n--;
            }
            return res;
        }

        }
}
