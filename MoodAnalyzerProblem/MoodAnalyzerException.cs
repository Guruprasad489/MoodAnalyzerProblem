using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodAnalyzerProblem
{
    //UC 3 - Custom Exceptions
    public class MoodAnalyzerException : Exception
    {
        public ExceptionTypes type;

        //Enum to differentiate mood analysis errors
        public enum ExceptionTypes
        {
            NULL_MOOD_EXCEPTION,
            EMPTY_MOOD_EXCEPTION
        }

        //Constructor to initialize ExceptionTypes
        public MoodAnalyzerException(ExceptionTypes type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
