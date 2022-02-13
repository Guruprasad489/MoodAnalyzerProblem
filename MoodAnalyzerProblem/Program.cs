using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodAnalyzerProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mood Anayzer Program");

            //UC1 - Creating MoodAnalyzer object
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer("I am in Happy Mood");
            Console.WriteLine("Mood is: "+ moodAnalyzer.AnalyseMood());
            Console.ReadLine();
        }
    }
}
