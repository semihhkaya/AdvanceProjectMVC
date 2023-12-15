using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Extensions
{
	public static class SessionExtension
	{
        public static void MySet(this ISession session, string mykey, object myvalue)
        {
            var data = JsonConvert.SerializeObject(myvalue);
            session.SetString(mykey, data);
        }
        public static T MyGet<T>(this ISession session, string mykey) where T : class
        {
            var sessionValue = session.GetString(mykey);
            if (string.IsNullOrEmpty(sessionValue))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(sessionValue);
        }
    }
}
