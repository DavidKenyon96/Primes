
using Primes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Primes.Tests
{
    //Unit test to confirm PrimeEvaluator is working as expected
    [TestClass()]
    public class PrimeEvaluatorTest
    {
        [TestMethod()]
        public void PrimeTest()
        {
            //Arange
            //Here we pick a prime number and input the expected factors to compare to actual factors
            int number = 738;
            string expectedFactors = "2, 3, 3, 41";

            //Act
            //Call PrimeEvaluator on above number and save it to a list to be made to a string below
            List<int> primeFactorsList = Primes.Program.PrimeEvaluator(number);
            //Join numbers together in comma delimited list so actual and expected can be compared
            string actualFactors = string.Join(", ", primeFactorsList);
 
            //Assert
            //Compare strings
            Assert.AreEqual(expectedFactors, actualFactors);
        }       
    }
}

