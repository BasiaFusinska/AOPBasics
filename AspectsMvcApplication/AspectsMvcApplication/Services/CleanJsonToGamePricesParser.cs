using AspectsMvcApplication.Models;
using Newtonsoft.Json.Linq;

namespace AspectsMvcApplication.Services
{
    public class CleanJsonToGamePricesParser : IJsonToGamePricesParser
    {
        public GamePrices Parse(string jsonText)
        {
            JToken token = JObject.Parse(jsonText);
            
            var price = token["GamePrice"].Value<int>();
            var premiumPrice = token["PremiumGamePrice"].Value<int>();
            
            return new GamePrices
                {
                    GamePrice = price, 
                    PremiumGamePrice = premiumPrice
                };
        }
    }
}