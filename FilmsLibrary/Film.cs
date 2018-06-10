using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace FilmsLibrary
{
    public class Film : IComparable
    {
        public string Name { get; protected set; }
        public string Director { get; protected set; }
        public double Budget { get; protected set; }
        public delegate void BudgetHandler(string message);
        public event BudgetHandler BudgetChanged;

        public Film()
        {
            Name = "no name";
            Director = "unknown";
            Budget = 0;
        }
        public Film(string Name, string Director, double Budget)
        {
            this.Name = Name;
            this.Director = Director;
            if (Budget >= 0)
            {
                this.Budget = Budget;
            }
            else
            {
                this.Budget = 0;
            }
        }
        public Film(Film o)
        {
            this.Name = o.Name;
            this.Director = o.Director;
            this.Budget = o.Budget;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append($"\tName: {this.Name}\n");
            s.Append($"\tDirector: {this.Director}\n");
            CultureInfo us = new CultureInfo("en-US");
            s.Append($"\tBudget: {this.Budget:c}\n");
            return s.ToString();
        }
        public virtual List<string> ToList()
        {
            List<string> list = new List<string>();
            list.Add($"{this.Name}");
            list.Add($"{this.Director}");
            list.Add($"{this.Budget}");
            return list;
        }
        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;
            }
            MusicalFilm mf = o as MusicalFilm;
            if (mf != null)
            {
                return this.Name.CompareTo(mf.Name);
            }
            else
            {
                Film f = o as Film;
                if (f != null)
                {
                    return this.Name.CompareTo(f.Name);
                }
                else
                {
                    throw new ArgumentException("Object is not a Film!");
                }
            }
        }
        public void ChangeBudget(double newBudget)
        {
            double oldBudget = this.Budget;
            if (newBudget < 0)
            {
                this.Budget = 0;
            }
            else
            {
                this.Budget = newBudget;
            }
            if (BudgetChanged != null)
            {
                BudgetChanged($"Budget of the film \"{this.Name}\" has been changed from {oldBudget} to {Budget}.");
            }
        }
        public static bool operator <(Film o1, Film o2)
        {
            return o1.Budget < o2.Budget;
        }
        public static bool operator >(Film o1, Film o2)
        {
            return o1.Budget > o2.Budget;
        }
        // Є сенс зробити абстрактним, щоб повертав готовий об'єкт
        public virtual void readFromFile(string filepath)
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
        public virtual void saveToFile(string filepath)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(this.Name + ", ");
            buffer.Append(this.Director + ", ");
            buffer.Append(this.Budget + "\n");
            File.WriteAllText(filepath, buffer.ToString());
        }
    }
}
