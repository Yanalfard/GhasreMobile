using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer.ViewModels;
using Newtonsoft.Json;

namespace GhasreMobile.Utilities
{
    public static class MainUtil
    {
        public static double Round(double input, int deg)
        {
            double degIn10 = Math.Pow(10, deg);
            input = input / degIn10;
            // input = Math.Round(input);
            input = Math.Floor(input);
            return input * degIn10;
        }

        public static async Task<string> GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                using HttpClient client = new HttpClient();
                string content = await client.GetStringAsync($"https://ipinfo.io/{ip}?token=000bc84576e573");
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(content);
                RegionInfo myRI1 = new RegionInfo(ipInfo.country);
                ipInfo.country = myRI1.EnglishName;
            }
            catch (Exception e)
            {
                ipInfo.country = null;
            }

            return ipInfo.country;
        }
    }
}
