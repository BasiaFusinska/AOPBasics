using System.Collections.Generic;
using System.Linq;
using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.DataAccess
{
    public interface IGamesDataAccess
    {
        User User { get; }
        IEnumerable<Game> GetGamesListForUser(int userId);
        Game GetById(int id);
    }

    public class GamesDataAccess : IGamesDataAccess
    {
        private readonly IEnumerable<Game> _games = new List<Game>
                {
                    new Game {Id = 1, Name = "Fire Catcher", IconPath = "fire_catcher.jpg", IsPremium = true},
                    new Game {Id = 2, Name = "Drakensang", IconPath = "drakensang.jpg"},
                    new Game {Id = 3, Name = "Chima", IconPath = "chima.jpg"},
                    new Game {Id = 4, Name = "Rally Challenge", IconPath = "rally_challenge.jpg"},
                    new Game {Id = 5, Name = "Freedom Tower", IconPath = "freedom_tower.jpg", IsPremium = true},
                    new Game {Id = 6, Name = "Bedazzled", IconPath = "bedazzled.jpg"},
                    new Game {Id = 7, Name = "Survival Instincts", IconPath = "survival_instincts.jpg"},
                    new Game {Id = 8, Name = "Village Car Race", IconPath = "car_race.jpg", IsPremium = true},
                    new Game {Id = 9, Name = "FlappyBird", IconPath = "flappybird.jpg"},
                    new Game {Id = 10, Name = "Water Mania", IconPath = "water_mania.jpg"},
                };

        private readonly User _user = new User {Id = 1, Name = "Basia F."};
        public User User { get { return _user; } }
        
        public IEnumerable<Game> GetGamesListForUser(int userId)
        {
            return _games;
        }

        public Game GetById(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }
    }
}