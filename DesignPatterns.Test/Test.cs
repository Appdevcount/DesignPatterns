using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Test
{
    [TestFixture]
    public class Test
    {
        MyTest test = null;
        [SetUp]
        public void Initialize()
        {
            test = new MyTest();
        }
        [TearDown]
        public void CleanUp()
        {
            test = null;
        }
        [Test]

        public void Addition()
        {
            Assert.AreEqual(5, test.Add(2, 3));
        }
        //[Test]

        //public void Substraction()
        //{
        //    Assert.AreEqual(0, test.Substract(2, 2));

        //}
        //[Test]

        //public void Divide()
        //{
        //    Assert.AreEqual(1, test.Devide(2, 2));
        //}
        [Test]
        [Ignore("Ignore a test")]
        public void Substraction()
        {
            Assert.AreEqual(0, test.Substract(2, 2));
        }
        //If you're using NUnit 3.0, then your error is because the ExpectedExceptionAttribute has been removed. You should instead use a construct like the Throws Constraint.
        [Test]
        //[ExpectedException(typeof(DivideByZeroException))]
        public void Divide()
        {
            //Assert.AreEqual(1, test.Devide(2, 0));
            Assert.Throws<DivideByZeroException>(() => test.Devide(2, 0));
        }

    }
    [TestFixture]
    public class CalculatorTesterMoq
    {
        // Step 6. Add the definition of the mock objects
        private IUSD_CLP_ExchangeRateFeed prvGetMockExchangeRateFeed()
        {
            Mock<IUSD_CLP_ExchangeRateFeed> mockObject = new Mock<IUSD_CLP_ExchangeRateFeed>();
            mockObject.Setup(m => m.GetActualUSDValue()).Returns(500);
            return mockObject.Object;
        }
        // Step 7. Add the test methods for each test case
        [Test(Description = "Divide 9 by 3. Expected result is 3.")]
        public void TC1_Divide9By3()
        {
            IUSD_CLP_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.Divide(9, 3);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test(Description = "Divide any number by zero. Should throw an System.DivideByZeroException exception.")]
        //[ExpectedException(typeof(System.DivideByZeroException))]
        public void TC2_DivideByZero()
        {
            IUSD_CLP_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            //int actualResult = calculator.Divide(9, 0);
            //int actualResult =
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(9, 0));
        }
        [Test(Description = "Convert 1 USD to CLP. Expected result is 500.")]
        public void TC3_ConvertUSDtoCLPTest()
        {
            IUSD_CLP_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.ConvertUSDtoCLP(1);
            int expectedResult = 500;
            Assert.AreEqual(expectedResult, actualResult);

            //https://www.codeproject.com/Articles/796014/KickStart-your-Unit-Testing-using-Moq        
            //To verify that a method was called, use the Verify method on the mock object;

            //mockCustomerRepository.Verify(t => t.Add(It.IsAny<Customer>()));
        }
    }
}
