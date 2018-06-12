using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Cards
{
    public class CardService : ICardService
    {
        private ICardServiceAsync _cardServiceAsync;
      
        public CardService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _cardServiceAsync = new CardServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<Card> CreateCard(string customerId, CardCreate requestModel)
        {
            return _cardServiceAsync.CreateCardAsync(customerId, requestModel).Result;
        }

        public HttpResponse<Card> CreateCard(string customerId, string cardToken)
        {
            return _cardServiceAsync.CreateCardAsync(customerId, cardToken).Result;
        }

        public HttpResponse<Card> GetCard(string customerId, string cardId)
        {
            return _cardServiceAsync.GetCardAsync(customerId, cardId).Result;
        }

        public HttpResponse<OkResponse> UpdateCard(string customerId, string cardId, CardUpdate requestModel)
        {
            return _cardServiceAsync.UpdateCardAsync(customerId, cardId, requestModel).Result;
        }

        public HttpResponse<OkResponse> DeleteCard(string customerId, string cardId)
        {
            return _cardServiceAsync.DeleteCardAsync(customerId, cardId).Result;
        }

        public HttpResponse<CardList> GetCardList(string customerId)
        {
            return _cardServiceAsync.GetCardListAsync(customerId).Result;
        }
    }
}