using System;
using System.Collections.Generic;
using System.IO;

namespace Primes
{
    public class Program
    {
        //Decided to make this a list so that I could invoke it in the PrimeUnitTests (initially had it as a void)
        //The results.add(number) statements are primarily for PrimeUnitTests so that I can compare expected factors
        //with actual factors. Otherwise, I would have to parse the console which I would prefer not to do.
        public static List<int> PrimeEvaluator(int number)
        {
            //List to be compared with in PrimeUnitTests
            var results = new List<int>();

            //Take out the 2s
            while (number % 2 == 0)
            {
                results.Add(2);
                number /= 2;
                Console.Write("2");
                Console.Write(", ");
            }

            //Take out the 3s
            while (number % 3 == 0)
            {
                results.Add(3);
                number /= 3;
                Console.Write("3");
                Console.Write(", ");
            }

            //Take out other prime numbers
            int boundary = (int)Math.Floor(Math.Sqrt(number)) + 1;
            for (int j = 3; j < boundary; j += 2)
            {
                while (number % j == 0)
                {
                    results.Add(j);
                    number /= j;
                    Console.Write(j.ToString());
                    Console.Write(", ");
                }
            }

            //If number is not 1, then whatever left is prime
            if (number > 1)
            {
                results.Add(number);
                Console.Write(number.ToString());
                Console.Write(", ");
            }
            //Must have method return something so that I can invoke the method in PrimeUnitTests
            return results;
        }

        static void Main(string[] args)
        {
            //Below try-catch is for saving a console output log to the Primes Debug bin - check if it's working
            FileStream outputStream;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                //Find output log under Primes\Primes\bin\Debug\PrimesOutput.txt
                outputStream = new FileStream("./PrimesOutput.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(outputStream);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open PrimesOutput for log saving.");
                Console.WriteLine(e.Message);
                return;
            }
            //Start listening to write to PrimesOutput.txt
            Console.SetOut(writer);

            //integers.txt pathway is read by this ReadLine and saved to string 'path'
            string integersPath = Console.ReadLine(); //Example used for testing: c:\Nexidia\integers.txt

            //Read all lines in integers.txt and save to array
            string[] integersArray = File.ReadAllLines(integersPath);
            
            //Iterate through all indices in integersArray, save them to integersList so we can pass to PrimeEvaluator
            for (int i = 0; i < integersArray.Length; i++)
            {     
                string integersList = integersArray[i];
                //Try to parse numbers and call PrimeEvaluator
                try
                { 
                    int number = int.Parse(integersList);
                    Console.Write(number + ": ");
                    //Call PrimeEvaluator on all numbers in integersList to find prime factorization
                    PrimeEvaluator(number);
                }
                //Catch bad inputs
                catch (FormatException e)
                {
                    Console.Write("{0}: {1}", e.GetType().Name, e.Message);
                }
                
                Console.WriteLine();
            }          
            //Stop listening for PrimesOutput.txt log, close streams
            Console.SetOut(oldOut);
            writer.Close();
            outputStream.Close();
        }
    }
}
