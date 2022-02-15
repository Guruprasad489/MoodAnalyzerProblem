using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MoodAnalyzerProblem;
using MoodAnalyzerProblem.Reflection;

namespace AnalyseMoodTesting
{
    [TestClass]
    public class AnalyseMoodTestCases
    {
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

                MoodAnalyserFactory factory = new MoodAnalyserFactory();
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
                MoodAnalyserFactory factory = new MoodAnalyserFactory();
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
                MoodAnalyserFactory factory = new MoodAnalyserFactory();
                object actual = factory.CreateMoodMoodAnalyse(className, constructorName);
            }
            catch (MoodAnalyzerException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
