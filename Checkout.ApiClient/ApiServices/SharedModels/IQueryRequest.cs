using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.SharedModels
{
    public interface IQueryRequest
    {
        int? Count { get; set; }
        int? Offset { get; set; }
        DateTime? FromDate { get; set; }
        DateTime? ToDate { get; set; }
    }
}
