using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Collections;
using System.Linq.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace makerspace.App_Lib.Utils
{
    public static class DAL_Utils
    {
        private static App_Lib.DAL.Paging _default_paging = new App_Lib.DAL.Paging { Page = 1, Page_Size = 25 };
        private static App_Lib.DAL.Paging Paging_Get(JObject params_jsoned = (JObject)null)
        {
            return new App_Lib.DAL.Paging
            {
                Page = Convert.ToInt32(params_jsoned["page"] ?? _default_paging.Page),
                Page_Size = Convert.ToInt32(params_jsoned["page_size"] ?? _default_paging.Page_Size)
            };

        }

        private static List<T> List_Filter_Item_Get<T>(List<T> list, string name, Object value)
        {
            //
            //to-do: Not very efficient, need to pass value type with value instead of many try-catches.
            //to-do: Fix case sensitivity
            //
            try //parse int
            {
                return list.Where((name + " = @0"), Convert.ToInt32(value)).ToList<T>();
            }
            catch { }
            try //parse datetime
            {
                return list.Where((name + " = @0"), Convert.ToDateTime(value)).ToList<T>();
            }
            catch { }
            try //parse string
            {
                return list.Where((name + " = @0"), Convert.ToString(value)).ToList<T>();
            }
            catch { }
            return list;
        }

        public static App_Lib.DAL.Paged_Results List_Filter_Sort_Page_Get<T>(List<T> list, JObject params_jsoned, string[] filter_columns, string[] return_columns)
        {

            if (params_jsoned == null)
                params_jsoned = new JObject();

            App_Lib.DAL.Paged_Results paged_results = new DAL.Paged_Results();

            //check data filter
            if (!String.IsNullOrEmpty(Convert.ToString(params_jsoned["filter"])))
            {
                JObject filter = JObject.FromObject(params_jsoned["filter"]);
                //Dynamic Filtering should be injection protected via placeholder.
                foreach (string name in filter_columns)
                {
                    if (Convert.ToString(filter[name]).Length > 0)
                    {
                        list = List_Filter_Item_Get<T>(list, name, filter[name]);
                    }
                }
            }
            /*
            // old sorting
            var prop_info = typeof(T).GetProperties()[0]; //hopefully this defaults to id column appropriately.
            if(!(String.IsNullOrEmpty(Convert.ToString(params_jsoned["order_by"]))))
            {
                try
                {
                    prop_info = typeof(T).GetProperty(params_jsoned["order_by"].ToString()); //attempt to get column from json params.
                }
                catch { }
            }
            //System.Diagnostics.Debug.WriteLine(prop_info); //keep eye on column sorted
            // old sorting
            */
            
            paged_results.Count = list.Count();
            paged_results.Paging = Paging_Get(params_jsoned);
            try
            {
                paged_results.Results = list
                    .OrderBy(Convert.ToString(params_jsoned["order_by"] ?? "0"))
                    .Skip(paged_results.Paging.Page_Size * (paged_results.Paging.Page - 1)).Take(paged_results.Paging.Page_Size) //.Cast<dynamic>().ToList(); //.ToList<T>();
                    .Select("new (" + String.Join(",",return_columns).TrimEnd(',') + ")").Cast<dynamic>().AsEnumerable().ToList();
                    //.Select("new (o.id, o.name, o.app_user_id), o").Cast<dynamic>().AsEnumerable().ToList(); //Convert.ToDateTime(created_on).Year,
            }
            catch (Exception e) { } //{throw new Exception("Unable to Get Data.");}
            return paged_results;
        }

        public static string List_To_JSON_String<T>(List<T> list)
        {
            return JObject.Parse(JsonConvert.SerializeObject(
                (
                    list.AsEnumerable().ToList()
                ),
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            )).ToString();

        }
    }
}