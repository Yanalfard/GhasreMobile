using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class IpInfo
    {
        public string ip { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }

        public IpInfo()
        {
            
        }

        public IpInfo(string ip, string city, string region, string country, string loc, string org, string postal, string timezone)
        {
            this.ip = ip;
            this.city = city;
            this.region = region;
            this.country = country;
            this.loc = loc;
            this.org = org;
            this.postal = postal;
            this.timezone = timezone;
        }
    }
}
