using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilmsLibrary1
{
    public class Cinema
    {
        public List<Film> List { get; }

        public Cinema()
        {
            List = new List<Film>();
        }
        public Cinema(List<Film> l)
        {
            List = l;
        }
        public Cinema(string filepath)
        {
            List = new List<Film>();
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string line;
            string[] array;
            char[] separators = { ',', ';', '\n' };
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                array = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length == 3)
                {
                    List.Add(new Film(array[0], array[1], Double.Parse(array[2])));
                }
                else if (array.Length == 4)
                {
                    List.Add(new MusicalFilm(array[0], array[1], Double.Parse(array[2]), array[3]));
                }
            }
            fs.Close();
        }
        Cinema(Cinema o)
        {
            List = o.List;
        }
        public void AddFilm(Film f)
        {
            if (f is MusicalFilm)
            {
                List.Add(new MusicalFilm(f as MusicalFilm));
            }
            else
            {
                List.Add(new Film(f));
            }
        }
        public void Sort()
        {
            List.Sort();
        }
        public Film Max()
        {
            Film max = List[0];
            for (int i = 0; i < List.Count; ++i)
            {
                if (List[i] > max)
                {
                    max = List[i];
                }
            }
            return max;
        }
    }
}
