using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Cards
{
    public class CardService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public CardService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }

        public HttpResponse<Card> CreateCard(string customerId, CardCreate requestModel)
        {
            var createCardUri = string.Format(_appSettings.ApiUrls.Cards, customerId);
            return _apiHttpClient.PostRequest<Card>(createCardUri, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<Card> GetCard(string customerId, string cardId)
        {
            var getCardUri = string.Format(_appSettings.ApiUrls.Card, customerId, cardId);
            return _apiHttpClient.GetRequest<Card>(getCardUri, _appSettings.SecretKey);
        }

        public HttpResponse<OkResponse> UpdateCard(string customerId, string cardId, CardUpdate requestModel)
        {
            var updateCardUri = string.Format(_appSettings.ApiUrls.Card, customerId, cardId);
            return _apiHttpClient.PutRequest<OkResponse>(updateCardUri, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> DeleteCard(string customerId, string cardId)
        {
            var deleteCardUri = string.Format(_appSettings.ApiUrls.Card, customerId, cardId);
            return _apiHttpClient.DeleteRequest<OkResponse>(deleteCardUri, _appSettings.SecretKey);
        }

        public HttpResponse<CardList> GetCardList(string customerId)
        {
            var getCardListUri = string.Format(_appSettings.ApiUrls.Cards, customerId);
            return _apiHttpClient.GetRequest<CardList>(getCardListUri, _appSettings.SecretKey);
        }

    }
}