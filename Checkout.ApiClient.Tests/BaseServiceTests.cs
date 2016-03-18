using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout;
using NUnit.Framework;

namespace Tests
{
    public class BaseServiceTests
    {
        protected APIClient CheckoutClient;

        [SetUp]
        public void Init()
        {
            CheckoutClient = new APIClient(); 
        }
    }
}
