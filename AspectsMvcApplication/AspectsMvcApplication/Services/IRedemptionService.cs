using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public interface IRedemptionService
    {
        void Redeem(Invoice invoice, int numberOfDays);
    }
}