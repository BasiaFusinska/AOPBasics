using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public interface IJsonToGamePricesParser
    {
        GamePrices Parse(string jsonText);
    }
}
