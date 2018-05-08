using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class BaseResponseRecurringPlan
    {
        public string Name { get; set; }
        public string PlanTrackId { get; set; }
        public decimal? AutoCapTime { get; set; }
        // Value must be an int but some API endpoints return a decimal
        // This handles the assignment of the Value property and converts from decimal to int, if necessary
        #region Value Type Handling
        [JsonIgnore]
        public int Value
        {
            get { return (int) _privateValue; }
            set { _privateValue = value; }
        }
        [JsonProperty(PropertyName = "value")]
        [Obsolete]
        public decimal _privateValue { get; set; }
        #endregion
        public string Cycle { get; set; }
        public int? RecurringCount { get; set; }
    }
}
