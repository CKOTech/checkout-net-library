using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Lookups.ResponseModels
{
    public class CountryInfo
    {
        public string Bin { get; set; }

        public string IssuerCountryISO2 { get; set; }

        public string CountryName { get; set; }

        public string BankName { get; set; }

        public string CardType { get; set; }
    }
}
