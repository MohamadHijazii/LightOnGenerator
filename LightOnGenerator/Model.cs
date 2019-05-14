using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LightOnGenerator
{
    public class Model
    {
        private string path;
        public List<List<int>> conf;

        public Model(string fileName)
        {
            this.path = "D:\\LightOnGenerator\\LightOnGenerator\\" +fileName;
            readConfig();
            
            
        }

        public List<int> getTimes(int n)
        {
            List<int> times = new List<int>();
            for (int i = 0; i < conf.Count; i++)
            {
                List<int> temp = conf[i];
                if (temp[0] == n)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        
                        times.Add(temp[j]);
                    }
                }
            }
            return times;
        }

        public int getN() {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                s = sr.ReadLine();
                int N = int.Parse(s);
                return N;
            }
            
        }

        public List<List<int>> read()
        {
            List<List<int>> cases;
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                s = sr.ReadLine();
                int N = int.Parse(s);
                int n = 0;
                cases = new List<List<int>>();
                
                while (n < N)
                {
                    List<int> temp = new List<int>();
                    s = sr.ReadLine();
                    string[] ss = s.Split(' ');
                    for (int i = 0; i < ss.Length; i++)
                    {
                        int d = int.Parse(ss[i]);
                        temp.Add(d);
                    }
                    cases.Add(temp);
                    n++;
                }
            }
            return cases;
        }

        public void write(List<List<int>> ls) {

            int N = ls.Count;
            using (StreamWriter wr = File.CreateText(path))
            {
                wr.WriteLine(N);
                for (int i = 0; i < N; i++) {
                    List<int> temp = ls[i];
                    for(int j = 0; j < temp.Count; j++)
                    {
                        wr.Write(temp[j]);
                        if(j!= temp.Count -1)
                            wr.Write(' ');
                    }
                    wr.WriteLine("");
                }
                 
            }
        }

        public void readConfig()
        {
            string conf_path = "D:\\LightOnGenerator\\LightOnGenerator\\config";
            conf = new List<List<int>>();
            using(StreamReader sr = File.OpenText(conf_path))
            {
                string s;
                s = sr.ReadLine();
                int N = int.Parse(s);
                while(N-- > 0)
                {
                    List<int> temp = new List<int>();
                    s = sr.ReadLine();
                    string[] ss = s.Split(' ');
                    for (int i = 0; i < ss.Length; i++)
                    {
                        int d = int.Parse(ss[i]);
                        temp.Add(d);
                    }
                    conf.Add(temp);

                }
                
            }
        }

        public void saveToMongo(List<List<int>> ls,string note,string sol)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("Light");
            var lvl = db.GetCollection<BsonDocument>("levels");
            int n = ls.Count;
            List<string> s = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string ss = "";
                List<int> temp = ls[i];
                for (int j = 0; j < temp.Count; j++)
                {
                    ss += temp[j];
                    if (j != temp.Count - 1)
                    {
                        ss += " ";
                    }
                }
                s.Add(ss);
            }

            BsonDocument doc = new BsonDocument();
            doc.Add("number", n);
            for(int i = 0; i < s.Count; i++)
            {
                string nou = "n" + i;
                doc.Add(nou, s[i]);
            }
            doc.Add("best solution", sol);
            doc.Add("note", note);
            lvl.InsertOne(doc);
        }

    }
}
