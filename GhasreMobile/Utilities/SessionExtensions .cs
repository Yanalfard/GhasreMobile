using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Utilities
{
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //public static void SetObject(this ISession session, string key, object value)
        //{
        //    session.SetString(key, JsonConvert.SerializeObject(value));
        //}

        //public static T GetObject<T>(this ISession session, string key)
        //{
        //    var value = session.GetString(key);
        //    return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        //}
    }
}
