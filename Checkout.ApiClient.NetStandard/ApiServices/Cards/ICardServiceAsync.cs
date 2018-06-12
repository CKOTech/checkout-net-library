using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Cards
{
    public interface ICardServiceAsync
    {
        Task<HttpResponse<Card>> CreateCardAsync(string customerId, CardCreate requestModel);
        Task<HttpResponse<Card>> CreateCardAsync(string customerId, string cardToken);
        Task<HttpResponse<OkResponse>> DeleteCardAsync(string customerId, string cardId);
        Task<HttpResponse<Card>> GetCardAsync(string customerId, string cardId);
        Task<HttpResponse<CardList>> GetCardListAsync(string customerId);
        Task<HttpResponse<OkResponse>> UpdateCardAsync(string customerId, string cardId, CardUpdate requestModel);
    }
}