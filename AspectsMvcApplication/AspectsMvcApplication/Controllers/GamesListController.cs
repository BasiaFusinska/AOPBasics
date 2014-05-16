using System;
using System.Web.Mvc;
using AspectsMvcApplication.DataAccess;
using AspectsMvcApplication.Models;
using AspectsMvcApplication.Services;

namespace AspectsMvcApplication.Controllers
{
    public class GamesListController : Controller
    {
        private readonly IGamesDataAccess _gamesDataAccess;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRedemptionService _redemptionService;
        private readonly IExternalServiceHandler _serviceHandler;
        private readonly IResourceService _resourceService;

        public GamesListController(IGamesDataAccess gamesDataAccess,
                                   ISubscriptionService subscriptionService,
                                   IRedemptionService redemptionService,
                                   IExternalServiceHandler serviceHandler,
                                   IResourceService resourceService)
        {
            _gamesDataAccess = gamesDataAccess;
            _subscriptionService = subscriptionService;
            _redemptionService = redemptionService;
            _serviceHandler = serviceHandler;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            var games = _gamesDataAccess.GetGamesListForUser(1);
            foreach (var game in games)
            {
                game.IconPath = _resourceService.GetResource(game.Name);
            }
            return View(games);
        }

        public ActionResult Buy(int id)
        {
            ViewBag.Prices = _serviceHandler.GetPricesFromService();
            ViewBag.User = _gamesDataAccess.User;
            
            var game = _gamesDataAccess.GetById(id);
            return View("BuyGame", game);
        }

        public ActionResult PayByMoney(int id)
        {
            var game = _gamesDataAccess.GetById(id);

            var subscription = new Subscription
                {
                    User = _gamesDataAccess.User,
                    Game = game,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7)
                };
            _subscriptionService.Subscribe(subscription);
            return RedirectToAction("Index");
        }

        public ActionResult PayByPoints(int id)
        {
            var game = _gamesDataAccess.GetById(id);

            var invoice = new Invoice
                {
                    User = _gamesDataAccess.User,
                    Game = game
                };

            _redemptionService.Redeem(invoice, 7);
            return RedirectToAction("Index");
        }

        public ActionResult Account()
        {
            return View("UserAccount", _gamesDataAccess.User);
        }
    }
}
