﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterTool.Core
{
    public interface IConverterRequestDependancy
    {
        void ProcessRequest(ConverterRequest converterRequest);
    }
}
