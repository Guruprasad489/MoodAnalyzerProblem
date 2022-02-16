using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer("I am Happy");
            Console.WriteLine(moodAnalyzer.AnalyseMood());
            ValidateMoodAnalyzer(moodAnalyzer);
            Console.ReadLine();
        }
        //Method to Vaidate MoodAnalyzer Property Using Annotations
        public static void ValidateMoodAnalyzer(MoodAnalyzer moodAnalyzer)
        {
            ValidationContext validationContext = new ValidationContext(moodAnalyzer, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(moodAnalyzer, validationContext, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine("All Validations are successful");
            }
        }
    }
}
