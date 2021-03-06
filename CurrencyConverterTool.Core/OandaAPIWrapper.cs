﻿using System;
using System.Collections.Generic;

namespace CurrencyConverterTool.Core
{
    public class OandaAPIWrapper : IConverterRequestDependancy
    {
        public void ProcessRequest(ConverterRequest converterRequest)
        {
            try
            {
                if (converterRequest == null)
                    return;

                Console.WriteLine("***********************************************************************");
                Console.WriteLine("OandaAPIWrapper: ProcessRequest(...) method has been started...");
                Console.WriteLine("     API Name        :   " + converterRequest.SourceAPI.Name);
                Console.WriteLine("     API URL         :   " + converterRequest.SourceAPI.URL);
                Console.WriteLine("     From Currency   :   " + converterRequest.FromCurrency);
                Console.WriteLine("     To Currency     :   " + converterRequest.ToCurrency);
                Console.WriteLine("     Conversion      :       Conversion Logic executes successfully!");
                Console.WriteLine("OandaAPIWrapper: ProcessRequest(...) method runs successfully...");
                Console.WriteLine("**********************************************************************");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
