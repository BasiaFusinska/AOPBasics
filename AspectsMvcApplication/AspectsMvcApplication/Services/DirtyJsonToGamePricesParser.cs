using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspectsMvcApplication.Models;
using Newtonsoft.Json.Linq;

namespace AspectsMvcApplication.Services
{
    public class DirtyJsonToGamePricesParser : IJsonToGamePricesParser
    {
        public GamePrices Parse(string jsonText)
        {
            if (!Validate(jsonText)) return null;

            JToken token = JObject.Parse(jsonText);

            var price = token["GamePrice"].Value<int>();
            var premiumPrice = token["PremiumGamePrice"].Value<int>();
            
            return new GamePrices
                {
                    GamePrice = price, 
                    PremiumGamePrice = premiumPrice
                };
        }

        private bool Validate(string jsonText)
        {
            return jsonText != "Some wrong data";
        }
    }
}