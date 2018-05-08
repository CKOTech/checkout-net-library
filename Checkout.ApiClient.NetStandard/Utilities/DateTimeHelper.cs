using System;

namespace Checkout.Utilities
{
    public class DateTimeHelper
    {
        #region TimeStamp Helpers
       
        public static Tuple<double, double> GetUnixTimeTodayStartAndEnd()
        {
            var startOfDay = DateTime.UtcNow.Date;
            var endOfDay = DateTime.UtcNow.AddDays(1).AddTicks(-1);

            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var startDayUnix = startOfDay.ToUniversalTime() - origin;
            var endDayUnix = endOfDay.ToUniversalTime() - origin;

            return new Tuple<double, double>(Math.Floor(startDayUnix.TotalSeconds), Math.Floor(endDayUnix.TotalSeconds));
        }

        #endregion

        public static string GetUtcNow()
        {
            return FormatAsUtc(DateTime.UtcNow);
        }

        public static string FormatAsUtc(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
