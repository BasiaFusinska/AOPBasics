using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public class CleanSubscriptionService : ISubscriptionService
    {
        public void Subscribe(Subscription subsciption)
        {
            //subscription != null
            //logging

            //try
            //transaction
            subsciption.Game.IsBought = true;

            var rentalTime = subsciption.EndDate.Subtract(subsciption.StartDate);
            var numberOfDays = (int)rentalTime.TotalDays;
            var pointsPerDay = subsciption.Game.IsPremium ? 2 : 1;
            var points = numberOfDays * pointsPerDay;

            subsciption.User.Points += points;
            //logging
        }    
    }
}