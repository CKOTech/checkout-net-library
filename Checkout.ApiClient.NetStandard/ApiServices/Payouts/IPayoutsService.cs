﻿using Checkout.ApiServices.Payouts.RequestModels;
using Checkout.ApiServices.Payouts.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Payouts
{
    public interface IPayoutsService
    {
        HttpResponse<Payout> MakePayout(BasePayout requestModel);
    }

    public interface IPayoutsServiceAsync
    {
        Task<HttpResponse<Payout>> MakePayoutAsync(BasePayout requestModel);
    }
}