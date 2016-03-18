using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public enum RecurringPlanStatus
    {
        FailedInitial,
        Active,
        Cancelled,
        InArrears,
        Suspended,
        Completed,
        AutoSuspended
    }
}
