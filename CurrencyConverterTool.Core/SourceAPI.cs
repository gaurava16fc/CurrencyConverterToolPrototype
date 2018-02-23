using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterTool.Core
{
    public class SourceAPI
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public bool IsAuthenticationRequired { get; set; }
        public Dictionary<string, string> AuthenticationParams { get; set; }
    }
}
