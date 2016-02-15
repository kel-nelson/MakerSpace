using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json.Linq;
namespace makerspace.App_Lib.Utils
{
    public static class App_Utils
    {
        public static JObject Parse_JSON(string value){
            JObject jsoned = new JObject();
            try
            {
                jsoned = JObject.Parse(value);
            }
            catch { }
            return jsoned;
        }

    }
}