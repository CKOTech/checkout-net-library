using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.SharedModels
{
    public interface IQueryResponse
    {
        int TotalRows { get; set; }
        int Offset { get; set; }
        int Count { get; set; }
    }
}
