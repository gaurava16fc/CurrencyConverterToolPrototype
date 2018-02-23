using System;

namespace CurrencyConverterTool.Core
{
    public class ConverterRequest : BaseRequest, IRequestInjection
    {
        private IConverterRequestDependancy _converterRequestDependancy;
        public SourceAPI SourceAPI { get; set; }

        public ConverterRequest(string fromCurrency, string toCurrency, decimal? amount, DateTime? conversionDate)
        {
            this.FromCurrency = fromCurrency;
            this.ToCurrency = toCurrency;
            this.Amount = amount;
            this.ConversionDate = conversionDate;
        }

        public void ProcessRequest(ConverterRequest request)
        {
            try
            {
                if (request != null)
                    _converterRequestDependancy.ProcessRequest(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InjectRequest(IConverterRequestDependancy converterRequestDependancy)
        {
            try
            {
                this._converterRequestDependancy = converterRequestDependancy;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
