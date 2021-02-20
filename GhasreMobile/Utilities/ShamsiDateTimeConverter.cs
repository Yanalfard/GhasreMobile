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
    }
}
