using System;
using System.Transactions;
using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public class DirtyRedemptionService : IRedemptionService
    {
        private readonly ILoggingService _loggingService;

        public DirtyRedemptionService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Redeem(Invoice invoice, int numberOfDays)
        {
            //defensive programming
            if (invoice == null) throw new ArgumentNullException("invoice");
            if (numberOfDays <= 0) throw new ArgumentException("", "numberOfDays");

            //logging
            _loggingService.Log(string.Format("Redemption time: {0}", DateTime.Now));
            
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
                            invoice.Game.IsBought = true;

                            var pointsPerDay = invoice.Game.IsPremium ? 8 : 5;
                            var points = numberOfDays*pointsPerDay;
                            invoice.User.Points -= points;

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

            //logging
            _loggingService.Log(string.Format("Redemption completed: {0}", DateTime.Now));
        }
    }
}