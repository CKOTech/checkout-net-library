using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.ApiServices.Charges.ResponseModels
{
    public class ChargeHistory
    {
        public List<Charge> Charges { get; set; }
    }
}
