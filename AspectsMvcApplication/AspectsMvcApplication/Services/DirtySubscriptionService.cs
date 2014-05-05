using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public class DirtySubscriptionService : ISubscriptionService
    {
        private readonly ILoggingService _loggingService;

        public DirtySubscriptionService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Subscribe(Subscription subsciption)
        {
            //defensive programming
            if (subsciption == null) throw new ArgumentNullException("subsciption");
            //logging
            _loggingService.Log(string.Format("Subscribtion time: {0}", DateTime.Now));

            //exception handling
            try
            {
                //transaction
                using (var scope = new TransactionScope())
                {
                    //reries
                    var retries = 3;
                    var succeeded = false;
                    while (!succeeded)
                    {
                        try
                        {
                            subsciption.Game.IsBought = true;

                            var rentalTime = subsciption.EndDate.Subtract(subsciption.StartDate);
                            var numberOfDays = (int) rentalTime.TotalDays;
                            var pointsPerDay = subsciption.Game.IsPremium ? 2 : 1;
                            var points = numberOfDays*pointsPerDay;

                            subsciption.User.Points += points;

                            scope.Complete();
                            succeeded = true;
                        }
                        catch
                        {
                            if (retries >= 0)
                                retries--;
                            else
                                throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle exception
                _loggingService.Log(string.Format("Exception: {0}", ex.Message));
            }
            _loggingService.Log(string.Format("Subscribtion completed: {0}", DateTime.Now));
        }
    }
}