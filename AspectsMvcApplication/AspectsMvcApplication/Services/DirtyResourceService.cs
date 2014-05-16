using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AspectsMvcApplication.Services
{
    public class DirtyResourceService : IResourceService
    {
        private readonly IDictionary<string, string> _gamesResources = new Dictionary<string, string>
                {
                    {"Fire Catcher", "fire_catcher.jpg"},
                    {"Drakensang", "drakensang.jpg"},
                    {"Chima", "chima.jpg"},
                    {"Rally Challenge", "rally_challenge.jpg"},
                    {"Freedom Tower", "freedom_tower.jpg"},
                    {"Bedazzled", "bedazzled.jpg"},
                    {"Survival Instincts", "survival_instincts.jpg"},
                    {"Village Car Race", "car_race.jpg"},
                    {"FlappyBird", "flappybird.jpg"},
                    {"Water Mania", "water_mania.jpg"},
                };

        public string GetResource(string gameName)
        {
            var cachedValue = (string)HttpContext.Current.Cache[gameName];

            if (cachedValue != null) return cachedValue;

            Thread.Sleep(1000);
            var resource = _gamesResources[gameName];

            HttpContext.Current.Cache[gameName] = resource;

            return resource;
        }
    }
}