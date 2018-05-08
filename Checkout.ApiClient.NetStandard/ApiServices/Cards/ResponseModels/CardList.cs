using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.Cards.ResponseModels
{
    public class CardList
    {
        public int Count;
        public List<Card> Data;
    }
}