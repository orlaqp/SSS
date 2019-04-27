using System;
namespace SSS.Utils.Seedwork.Datetime
{
    public static class ConvertDateTime
    {
        public static DateTime FormatDateTime(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (format.Contains("HH") && !format.Contains(":mm"))
            {
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, 00, 00);
            }
            if (format.Contains(":mm") || format.Contains(":ss"))
            {
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 00);
            }
            if (!format.Contains("HH") && !format.Contains(":mm") && !format.Contains(":ss"))
            {
                return new DateTime(time.Year, time.Month, time.Day);
            }

            return DateTime.Now;
        }
    }
}
