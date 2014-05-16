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
                    new Game {Id = 1, Name = "Fire Catcher", IsPremium = true},
                    new Game {Id = 2, Name = "Drakensang"},
                    new Game {Id = 3, Name = "Chima"},
                    new Game {Id = 4, Name = "Rally Challenge"},
                    new Game {Id = 5, Name = "Freedom Tower", IsPremium = true},
                    new Game {Id = 6, Name = "Bedazzled"},
                    new Game {Id = 7, Name = "Survival Instincts"},
                    new Game {Id = 8, Name = "Village Car Race", IsPremium = true},
                    new Game {Id = 9, Name = "FlappyBird"},
                    new Game {Id = 10, Name = "Water Mania"},
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