using System;
using System.Transactions;
using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public class RedemptionService : IRedemptionService
    {
        public void Redeem(Invoice invoice, int numberOfDays)
        {
            invoice.Game.IsBought = true;

            var pointsPerDay = invoice.Game.IsPremium ? 8 : 5;
            var points = numberOfDays * pointsPerDay;
            invoice.User.Points -= points;
        }
    }
}