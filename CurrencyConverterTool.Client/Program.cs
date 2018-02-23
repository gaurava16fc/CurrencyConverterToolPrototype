using System;
using CurrencyConverterTool.Core;
using CurrencyConverterTool.ExceptionLogger;

namespace CurrencyConverterTool.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            try
            {
                Converter cnvtr = new Converter();
                cnvtr.StartConversion("OandaAPI", "USD", "KWD", 2, null);

                Console.WriteLine("");
                cnvtr.StartConversion("XEAPI", "EUR", "GBP", 5, null);

                Console.WriteLine("");
                cnvtr.StartConversion("CurrencyConverterAPI", "INR", "AUD", 7, null);

                Console.WriteLine("");
                cnvtr.StartConversion("FixerAPI", "INR", "AUD", 7, null);

                Console.WriteLine("");
                cnvtr.StartConversion("YahooAPI", "INR", "AUD", 7, null);               
            }
            catch (Exception ex)
            {
                //Exception Logging
                ExceptionLogging exceptionLogging = new ExceptionLogging("CurrencyConverter");
                exceptionLogging.LogException(ex);
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to quit....");

            Console.Read();
        }
    }
}
