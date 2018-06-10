using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilmsLibrary1
{
    public class MusicalFilm : Film, IComparable
    {
        public string Composer { get; private set; }

        public MusicalFilm() : base()
        {
            Composer = "unknown";
        }
        public MusicalFilm(string Name, string Director, double Budget, string Composer) : base(Name, Director, Budget)
        {
            this.Composer = Composer;
        }
        public MusicalFilm(MusicalFilm o) : base(o)
        {
            this.Composer = o.Composer;
        }

        public override string ToString()
        {
            return base.ToString() + $"\tComposer: {this.Composer}\n";
        }
        public override List<string> ToList()
        {
            List<string> list = base.ToList();
            list.Add($"{this.Composer}");
            return list;
        }
        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;
            }
            MusicalFilm f = o as MusicalFilm;
            if (f != null)
            {
                return this.Name.CompareTo(f.Name);
            }
            else
            {
                Film fm = o as Film;
                if (fm != null)
                {
                    return this.Name.CompareTo(fm.Name);
                }
                else
                {
                    throw new ArgumentException("Object is not a Film!");
                }
            }
        }
        public override void readFromFile(string filepath)
        {
            try
            {
                FileStream stream = new FileStream(filepath, FileMode.Open);
                StreamReader read = new StreamReader(stream);
                String readLine = read.ReadLine();
                char[] separator = { ',', '\n', };
                String[] readInfo = readLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                this.Name = readInfo[0];
                this.Director = readInfo[1];
                if (Double.TryParse(readInfo[2], out double res))
                {
                    this.Budget = res;
                }
                else
                {
                    this.Budget = 0;
                }
                this.Composer = readInfo[3];
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        public override void saveToFile(string filepath)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(this.Name + ", ");
            buffer.Append(this.Director + ", ");
            buffer.Append(this.Budget + ", ");
            buffer.Append(this.Composer + "\n");
            File.WriteAllText(filepath, buffer.ToString());
        }
    }
}
