using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoodAnalyzerProblem.Reflection
{
    public class MoodAnalyserReflector
    {
        /// <summary>
        /// Create MoodAnalyserReflector and specify static method to create MoodAnalyser Object
        /// </summary>
        /// <param name="className"></param>
        /// <param name="constructorName"></param>
        /// <returns></returns>
        /// <exception cref="MoodAnalyzerException"></exception>
        /// 
        public object CreateMoodMoodAnalyse(string className, string constructorName)
        {
            string pattern = @"." + constructorName + "$";
            Match result = Regex.Match(className, pattern);
            if (result.Success)
            {
                try
                {
                    Assembly executing = Assembly.GetExecutingAssembly();
                    Type moodAnalyzerType = executing.GetType(className);
                    return Activator.CreateInstance(moodAnalyzerType);
                }
                catch (ArgumentNullException)
                {

                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionTypes.NO_SUCH_CLASS, "Class not found");
                }
            }
            else
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionTypes.NO_SUCH_METHOD, "Constructor not found");
            }
        }

        //UC5 - Using Reflection Create MoodAnalyser with parameter constructor
        public object CreateMoodMoodAnalyserParameterObject(string className, string constructorName, string message)
        {
            Type type = typeof(MoodAnalyzer);
            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(constructorName))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(string) });
                    var obj = constructorInfo.Invoke(new object[] { message });
                    return obj;
                }
                else
                {
                    throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionTypes.NO_SUCH_METHOD, "Could not find constructor");
                }
            }
            else
            {
                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionTypes.NO_SUCH_CLASS, "Could not find class");
            }
        }

        //UC6 - Use Reflector to invoke MoodAnalyzer method 
        public string InvokeMoodAnalyzer(string message, string methodName)
        {
            try
            {
                Type type = typeof(MoodAnalyzer);
                MethodInfo methodInfo = type.GetMethod(methodName);
                MoodAnalyserReflector reflector = new MoodAnalyserReflector();
                object moodAnalyserObject = reflector.CreateMoodMoodAnalyserParameterObject("MoodAnalyzerProblem.MoodAnalyzer", "MoodAnalyzer", message);
                object info = methodInfo.Invoke(moodAnalyserObject, null);
                return info.ToString();
            }
            catch (NullReferenceException)
            {

                throw new MoodAnalyzerException(MoodAnalyzerException.ExceptionTypes.NO_SUCH_METHOD, "Method not found");
            }
        }
    }
}
