using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Linq.Dynamic;
using makerspace.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace makerspace.Controllers
{
    public class APIController : Controller
    {
        private static string content_type = "application/json";

        class Paging
        {
            public int Page { get; set; }
            public int Page_Size { get; set; }
        }

        private static Paging _default_paging = new Paging {Page=1,Page_Size=25};
        private Paging Paging_Get(JObject params_jsoned = (JObject)null)
        {
            return new Paging
            {
                Page = Convert.ToInt32(params_jsoned["page"] ?? _default_paging.Page),
                Page_Size = Convert.ToInt32(params_jsoned["page_size"] ?? _default_paging.Page_Size)
            };

        }

        private List<T> List_Filter_Sort_Page_Get<T>(List<T> list, JObject params_jsoned, string[] filter_columns, string[] return_columns)
        {
            if (params_jsoned == null)
                params_jsoned = new JObject();
            /* sorting */
            /*
            var prop_info = typeof(T).GetProperties()[0]; //hopefully this defaults to id column appropriately.
            if(!(String.IsNullOrEmpty(Convert.ToString(params_jsoned["order_by"]))))
            {
                try
                {
                    prop_info = typeof(T).GetProperty(params_jsoned["order_by"].ToString()); //attempt to get column from json params.
                }
                catch { }
            }
            System.Diagnostics.Debug.WriteLine(prop_info); //keep eye on column sorted
            */
            /* sorting */
            
            //new Object(){id}
            Paging paging = Paging_Get(params_jsoned);
            string filter = "1=1";
            foreach (string item in filter_columns)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(params_jsoned[item])))
                    filter += " and " + item + "=\"" + params_jsoned[item] + "\"";
            }
            return list
                .Where(filter)
                .OrderBy(Convert.ToString(params_jsoned["order_by"] ?? "0"))
                //.OrderBy(item => prop_info.GetValue(item, null))
                .Skip(paging.Page_Size * (paging.Page - 1)).Take(paging.Page_Size)
                .Select<dynamic>("new (id as id, title as title)")
                /*.Select(
                    item => new T{
                    id = 0,//prop_info.GetValue("title", null),
                    title = prop_info.GetValue("title", null),
                    //id = item.id,
                    requirement_age = 0//item.requirement_age
                })*/
                //.().
                .ToList<T>();
                
        }

        [HttpGet]
        public ContentResult App_User_Area_Memberships(string json_params)
        {
            App_Model app_model = new App_Model();

            string list = JsonConvert.SerializeObject(
                app_model.App_User_Area_Memberships,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(list,content_type);
        }

        [HttpGet]
        public ContentResult App_Areas(string params_json)
        {
            JObject params_jsoned = JsonConvert.DeserializeObject<JObject>(params_json ?? "{}");
            
            App_Model app_model = new App_Model();
            string[] filter_columns = new string[] { "id", "title" };
            string[] return_columns = new string[]{"id","title"};
            var list = List_Filter_Sort_Page_Get<App_Areas>(app_model.App_Areas.ToList<App_Areas>(), params_jsoned, filter_columns, return_columns);
            /*var list = Paged_Get<App_Areas>(params_jsoned, app_model.App_Areas.ToList<App_Areas>())
                //.Where(item => item.id == 1)
;

            
            */
            string list_string = JsonConvert.SerializeObject(
                list,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            
            return Content(list_string, "application/json");
        }
    }
}