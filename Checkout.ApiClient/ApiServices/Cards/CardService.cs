using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.SharedModels;
namespace Checkout.ApiServices.Cards
{
    public class CardService 
    {

        public HttpResponse<Card> CreateCard(string customerId, CardCreate requestModel)
        {
            var createCardUri = string.Format(ApiUrls.Cards, customerId);
            return new ApiHttpClient().PostRequest<Card>(createCardUri, AppSettings.SecretKey, requestModel);
        }
        
        public HttpResponse<Card> GetCard(string customerId, string cardId)
        {
            var getCardUri = string.Format(ApiUrls.Card, customerId, cardId);
            return new ApiHttpClient().GetRequest<Card>(getCardUri, AppSettings.SecretKey);
        }

        public HttpResponse<OkResponse> UpdateCard(string customerId, string cardId, CardUpdate requestModel)
        {
            var updateCardUri = string.Format(ApiUrls.Card, customerId, cardId);
            return new ApiHttpClient().PutRequest<OkResponse>(updateCardUri, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> DeleteCard(string customerId, string cardId)
        {
            var deleteCardUri = string.Format(ApiUrls.Card, customerId, cardId);
            return new ApiHttpClient().DeleteRequest<OkResponse>(deleteCardUri, AppSettings.SecretKey);
        }

        public HttpResponse<CardList> GetCardList(string customerId)
        {
            var getCardListUri = string.Format(ApiUrls.Cards, customerId);
            return new ApiHttpClient().GetRequest<CardList>(getCardListUri, AppSettings.SecretKey);
        }

    }
}