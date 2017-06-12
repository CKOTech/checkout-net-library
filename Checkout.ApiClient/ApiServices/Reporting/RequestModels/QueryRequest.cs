using Checkout.ApiServices.SharedModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Reporting.RequestModels
{
    public class QueryRequest : BasePagination
    {
        public string Search { get; set; }

        public List<Filter> Filters { get; set; }

        public SortColumn? SortColumn { get; set; }

        public SortOrder? SortOrder { get; set; }
    }
}
