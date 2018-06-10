using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FilmsLibrary1;

namespace ExamConsole
{
    class Program
    {
        public static void messagePrint(string message)
        {
            Console.WriteLine(message);
        }
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema(@"C:\Users\solol\Desktop\Films.txt");
            FileStream fs = new FileStream(@"C:\Users\solol\Desktop\Films.txt", FileMode.Open);
            fs.Close();
            cinema.Sort();
            foreach (Film elem in cinema.List)
            {
                Console.WriteLine(elem);
            }
            var max = cinema.Max();
            Console.WriteLine($"Max element: \n{max}");
            Console.WriteLine(max.GetType().Name + '\n');

            Console.ReadKey();
            Console.Clear();

            for (int i = 0; i < cinema.List.Count; ++i)
            {
                cinema.List[i].BudgetChanged += messagePrint;
                cinema.List[i].ChangeBudget(500);
            }

            Console.ReadKey();
            Console.Clear();

            foreach (Film elem in cinema.List)
            {
                Console.WriteLine(elem);
            }

            Console.ReadKey();
        }
    }
}
