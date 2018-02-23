using System;
using System.Linq;

namespace CurrencyConverterTool.Core
{
    public class Converter
    {
        public void StartConversion(string sourceName, string fromCurrency, string toCurrency, decimal? amount, DateTime? conversionDate)
        {
            try
            {
                if (!AuthenticationManager.GetAuthenticationManagerInstance().IsValidSource(sourceName))
                {
                    Console.WriteLine(String.Format("Invalid Source Name: '{0}'", sourceName));
                    return;
                }

                if (!AuthenticationManager.GetAuthenticationManagerInstance().IsActiveSource(sourceName))
                {
                    Console.WriteLine(String.Format("Source Name: '{0}' is not active", sourceName));
                    return;
                }

                IConverterRequestDependancy converterRequestDependancy = BuildRequestDependancy(sourceName);
                if (converterRequestDependancy != null)
                {
                    ConverterRequest converterRequest = new ConverterRequest(fromCurrency, toCurrency, amount, conversionDate);
                    ((IRequestInjection)converterRequest).InjectRequest(converterRequestDependancy);

                    SetAttributesToRequestDependancy(sourceName, fromCurrency, toCurrency, amount, conversionDate, ref converterRequest);
                    converterRequest.ProcessRequest(converterRequest);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetAttributesToRequestDependancy(string sourceName, string fromCurrency, string toCurrency, decimal? amount, DateTime? conversionDate, ref ConverterRequest converterRequest)
        {
            try
            {
                var sourceAPI = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceName);
                if (sourceAPI != null)
                {
                    converterRequest.SourceAPI = new SourceAPI();
                    converterRequest.SourceAPI.Params = new System.Collections.Generic.Dictionary<string, string>();
                    converterRequest.SourceAPI.AuthenticationParams = new System.Collections.Generic.Dictionary<string, string>();
                    converterRequest.SourceAPI.Name = sourceAPI.SourceAPIName;
                    converterRequest.SourceAPI.URL = sourceAPI.SourceAPIURL;
                    converterRequest.SourceAPI.IsAuthenticationRequired = false;
                    converterRequest.SourceAPI.Params.Add("FromCurrency", fromCurrency);
                    converterRequest.SourceAPI.Params.Add("ToCurrency", toCurrency);
                    converterRequest.FromCurrency = fromCurrency;
                    converterRequest.ToCurrency = toCurrency;
                    if (amount.HasValue && amount > 0)
                    {
                        converterRequest.SourceAPI.Params.Add("Amount", Convert.ToString(amount));
                        converterRequest.Amount = amount;
                    }

                    if (conversionDate.HasValue)
                    {
                        converterRequest.SourceAPI.Params.Add("ConversionDate", Convert.ToString(conversionDate));
                        converterRequest.ConversionDate = conversionDate;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IConverterRequestDependancy BuildRequestDependancy(string sourceName)
        {
            try
            {
                IConverterRequestDependancy converterRequestDependancy = null;
                var sourceAPIDetail = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceName);
                if (sourceAPIDetail == null)
                    return null;

                var sourceAPIClassName = sourceAPIDetail.SourceAPIClassName;
                if (string.IsNullOrEmpty(sourceAPIClassName))
                {
                    Console.WriteLine(String.Format("Source Name: '{0}' : No supported class defined!", sourceName));
                    return null;
                }

                var sourceClassType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                       from type in assembly.GetTypes()
                                       where type.Name == sourceAPIClassName
                                       select type).FirstOrDefault();

                if (sourceClassType == null)
                {
                    Console.WriteLine(String.Format("Source Name: '{0}': Define class '{1}'!", sourceName, sourceAPIClassName));
                    return null;
                }

                converterRequestDependancy = (IConverterRequestDependancy)Activator.CreateInstance(sourceClassType);
                return converterRequestDependancy;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
