using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace GhasreMobile.Utilities
{
    public static class ShamsiDateTimeConverter
    {
        public static string AllToShamsi(this DateTime value)
        {
            PersianCalendar Pc = new PersianCalendar();

            return " " + Pc.GetYear(value) + "/" + Pc.GetMonth(value).ToString("00") + "/" + Pc.GetDayOfMonth(value).ToString("00") + "-" + Pc.GetHour(value).ToString("00") + ":" + Pc.GetMinute(value).ToString("00");
        }

        public static string DateToShamsi(this DateTime value)
        {
            PersianCalendar Pc = new PersianCalendar();

            return " " + Pc.GetYear(value) + "/" + Pc.GetMonth(value).ToString("00") + "/" + Pc.GetDayOfMonth(value).ToString("00");
        }

        public static string DateClock(this DateTime value)
        {
            PersianCalendar Pc = new PersianCalendar();

            return Pc.GetHour(value).ToString("00") + ":" + Pc.GetMinute(value).ToString("00");
        }
        public static string ToSpecialOffer(this DateTime value)
        {
            value = value.AddDays(+25);
            PersianCalendar Pc = new PersianCalendar();
            string day = value.Day.ToString();
            string manth = value.ToString("MMMM");
            string year = value.Year.ToString();
            string time = value.Hour + ":" + value.Minute + ":" + value.Second;
            return day + " " + manth + " " + year + " " + time + " " + "GMT";
        }
        public static int ConvertToUnixTimestamp(this DateTime date)
        {
            PersianCalendar Pc = new PersianCalendar();
            int day = date.Day;
            int manth = date.Month;
            int year = date.Year;
            int Hour = date.Hour;
            int Minute = date.Minute;
            int Second = date.Second;

            DateTime dtNow = DateTime.Now;
            TimeSpan result = dtNow.Subtract(date);

            int seconds = Convert.ToInt32(result.TotalSeconds) * -1;
            return seconds;

        }
    }
}
