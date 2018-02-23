﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterTool.Core
{
    public abstract class BaseRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ConversionDate { get; set; }
    }
}
