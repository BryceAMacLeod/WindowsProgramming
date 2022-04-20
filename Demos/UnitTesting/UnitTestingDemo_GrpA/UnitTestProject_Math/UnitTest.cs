
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingDemo_GrpA;

namespace UnitTestProject_Math
{
    [TestClass]
    public class UnitTest1
    {
        MathModule math = new MathModule();

        [TestMethod]
        public void Test_Add_ValidInts()
        {
            //Test that checks Add method works with 2 valid integers.
            int firstNum = 4;
            int secondNum = 6;
            double expected = 10.0;

            double result = math.Add(firstNum, secondNum);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Test_Add_ValidNegativeIntegers()
        {
            //Test that checks Add method works with 2 valid negative integers.
            int firstNum = -2;
            int secondNum = -4;
            double expected = -6.0;

            double result = math.Add(firstNum, secondNum);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Test_Add_ValidDoubles()
        {
            //Test that checks Add method works with 2 valid doubles.
            double firstNum = 5.5;
            double secondNum = 7.2;
            double expected = 12.7;

            double result = math.Add(firstNum, secondNum);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Test_Add_ValidNegativeDoubles()
        {
            //Test that checks Add method works with 2 valid negative doubles.
            double firstNum = -2.5;
            double secondNum = -4.5;
            double expected = -7.0;

            double result = math.Add(firstNum, secondNum);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Test_Divide_ValidInts()
        {
            //Test that checks Add method works with 2 valid negative doubles.
            int firstNum = 12;
            int secondNum = 4;
            double expected = 3.0;

            double result = math.Divide(firstNum, secondNum);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        //[ExpectedException (typeof(DivideByZeroException))]
        public void Test_Divide_DivByZero_ErrorHandled()
        {
            //Test that checks whether Divide method's DivByZero exception handling works
            int firstNum = 12;
            int secondNum = 0;

            //Run the test
            Assert.ThrowsException<DivideByZeroException>(() => math.Divide(firstNum, secondNum));
        }
    }
}
