
using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.SharedModels
{
    public class ResponseError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string EventId { get; set; }
        public List<String> Errors { get; set; }
    }
}