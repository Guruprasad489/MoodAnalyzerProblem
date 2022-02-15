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

        //TC 1.1 - Method to test Sad Mood
        [TestMethod]       
        [TestCategory ("Sad Message")]
        public void TestSadMoodInMessage()
        {
            //Arrange
            string message = "I am in sad Mood";
            string expected = "SAD";
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer(message);

            //Act
            string actual = moodAnalyzer.AnalyseMood();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //TC 1.2 - Method to test Happy Mood
        [TestMethod]
        [TestCategory("Happy Message")]
        public void TestHappyMoodInMessage()
        {
            //Arrange
            string message = "I am in Any Mood";
            string expected = "HAPPY";
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer(message);

            //Act
            string actual = moodAnalyzer.AnalyseMood();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //TC 2.1 - Method to test Happy Mood in null message
        [TestMethod]
        [TestCategory("Exception")]
        public void GivenNullMessageReturnHappyMood()
        {
            //Arrange
            string message = null;
            string expected = "HAPPY";
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
            //Arrange
            //string message = null;
            //string expected = "HAPPY";
            MoodAnalyzer moodAnalyzer = new MoodAnalyzer(userInput);
            try
            {
                //Act
                string actual = moodAnalyzer.AnalyseMood();
            }
            catch(MoodAnalyzerException ex)
            {
                //Assert
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

        //TC 4.2 - improper class details are provided and expected to throw exception Class not found
        [TestMethod]
        [TestCategory("Reflection")]
        [DataRow("MoodAnalyzerProblem.Reflection.Owner", "Reflection.Owner", "Class not found")]
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

        //TC 4.3 - improper constructor details are provided and expected to throw exception Constructor not found
        [TestMethod]
        [TestCategory("Reflection")]
        [DataRow("MoodAnalyzerProblem.Reflection.Customer", "Reflection.OwnerMood", "Constructor not found")]
        public void GivenImproperConstructorName_ThrowException(string className, string constructorName, string expected)
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
            MoodAnalyzer expected = new MoodAnalyzer(message);
            object obj = null;
            try
            {
                obj = factory.CreateMoodMoodAnalyserParameterObject(className, "MoodAnalyzer", message);

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
            MoodAnalyzer expected = new MoodAnalyzer(message);
            object obj = null;
            try
            {
                obj = factory.CreateMoodMoodAnalyserParameterObject("MoodAnalyzer", constructor, message);

            }
            catch (MoodAnalyzerException actual)
            {
                Assert.AreEqual(expextedError, actual.Message);
            }
        }
    }
}
