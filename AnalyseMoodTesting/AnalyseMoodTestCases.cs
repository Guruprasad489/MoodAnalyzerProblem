using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MoodAnalyzerProblem;
using MoodAnalyzerProblem.Reflection;

namespace AnalyseMoodTesting
{
    [TestClass]
    public class AnalyseMoodTestCases
    {
        MoodAnalyserFactory factory;
        [TestInitialize]
        public void Setup()
        {
            factory = new MoodAnalyserFactory();
        }

        //TC 1.1,1.2,2.1 - Method to test Sad Mood and Happy Mood
        [TestMethod]       
        [TestCategory ("Test Mood in Message")]
        //Arrange
        [DataRow("I am in sad Mood", "SAD")]
        [DataRow ("I am in Any Mood", "HAPPY")]
        [DataRow(null, "HAPPY")]
        public void TestMoodInMessage(string message, string expected)
        {
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer(message);
            //Act
            string actual = moodAnalyzer.AnalyseMood();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //TC 3.1 - Method to test Custom exception for null message
        [TestMethod]
        [TestCategory("Custom Exception")]
        [DataRow (null, "Message should not be null")]
        [DataRow ("", "Message should not be empty")]
        public void GivenNullMessageTestCustomException(string userInput, string expected)
        {
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer(userInput);
            try
            {
                string actual = moodAnalyzer.AnalyseMood();
            }
            catch(MoodAnalyzerException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        //TC 4.1 - Proper class details are provided and expected to return the MoodAnalyser Object
        [TestMethod]
        [TestCategory("Reflection")]
        [DataRow("MoodAnalyzerProblem.Reflection.Customer", "Customer")]
        public void GivenMoodAnalyzerClassName_ReturnMoodAnalyzerObject(string className, string constructorName)
        {
            MoodAnalyzer expected = new MoodAnalyzer();
            object obj = null;

            obj = factory.CreateMoodMoodAnalyse(className, constructorName);
            expected.Equals(obj);
        }

        //TC 4.2, 4.3 - improper class, constructor details are provided and expected to throw exception Class not found
        [TestMethod]
        [TestCategory("Reflection")]
        [DataRow("MoodAnalyzerProblem.Reflection.Owner", "Reflection.Owner", "Class not found")]
        [DataRow("MoodAnalyzerProblem.Reflection.Customer", "Reflection.OwnerMood", "Constructor not found")]
        public void GivenImproperClassName_ThrowException(string className, string constructorName, string expected)
        {
            try
            {
                object actual = factory.CreateMoodMoodAnalyse(className, constructorName);
            }
            catch (MoodAnalyzerException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        //TC 5.1 - Method to test moodanalyser class with parameter constructor to check if two objects are equal
        [TestCategory("Reflection")]
        [TestMethod]
        [DataRow("I am in Happy mood")]
        [DataRow("I am in Sad mood")]
        [DataRow("I am in any mood")]
        public void GivenMessageReturnParameterizedConstructor(string message)
        {
            MoodAnalyzer expected = new MoodAnalyzer(message);
            object obj = null;
            try
            {
                obj = factory.CreateMoodMoodAnalyserParameterObject("MoodAnalyzer", "MoodAnalyzer", message);
            }
            catch (MoodAnalyzerException actual)
            {
                Assert.AreEqual(expected, actual.Message);
            }
            obj.Equals(expected);
        }

        //TC 5.2 - Method to test moodanalyser with diff class with parameter constructor to throw error
        [TestCategory("Reflection")]
        [TestMethod]
        [DataRow("Company", "I am in Happy mood", "Could not find class")]
        [DataRow("Student", "I am in Sad mood", "Could not find class")]
        public void GivenMessageReturnParameterizedClassNotFound(string className, string message, string expextedError)
        {
            try
            {
                object obj = factory.CreateMoodMoodAnalyserParameterObject(className, "MoodAnalyzer", message);
            }
            catch (MoodAnalyzerException actual)
            {
                Assert.AreEqual(expextedError, actual.Message);
            }
        }

        //TC 5.3 - Method to test moodanalyser with diff constructor with parameter constructor to throw error
        [TestCategory("Reflection")]
        [TestMethod]
        [DataRow("Customer", "I am in Happy mood", "Could not find constructor")]
        [DataRow("Student", "I am in Sad mood", "Could not find constructor")]
        public void GivenMessageReturnParameterizedConstructorNotFound(string constructor, string message, string expextedError)
        {
            try
            {
                object obj = factory.CreateMoodMoodAnalyserParameterObject("MoodAnalyzer", constructor, message);
            }
            catch (MoodAnalyzerException actual)
            {
                Assert.AreEqual(expextedError, actual.Message);
            }
        }

        //UC 6.1,6.2 - Method to invoke analyse mood method to return happy or sad or invalid method
        [TestCategory("Reflection")]
        [TestMethod]
        [DataRow("HAPPY")]
        [DataRow("Method not found")]
        public void ReflectionReturnMethod(string expected)
        {
            try
            {
                string actual = factory.InvokeMoodAnalyzer("happy", "AnalyseMood");
            }
            catch(MoodAnalyzerException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
