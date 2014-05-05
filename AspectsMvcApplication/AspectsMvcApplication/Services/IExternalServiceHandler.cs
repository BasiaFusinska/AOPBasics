using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public interface IExternalServiceHandler
    {
        GamePrices GetPricesFromService();
    }
}
