using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Linq.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using makerspace.Models;

namespace makerspace.Controllers
{
    public class APIController : Controller
    {
        private static string content_type = "application/json";

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_Areas_Get(string params_json)
        {
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);
            string[] filter_columns = new string[] {"id","title","requirement_age"};
            string[] select_columns = new string[] { "id", "title", "requirement_age"};

            App_Model app_model = new App_Model();

            string return_json_string = App_Lib.Utils.DAL_Utils.List_Filter_Sort_Page_Get<App_Areas>(app_model.App_Areas.ToList<App_Areas>(), params_jsoned, filter_columns, select_columns).ToString();
            return Content(return_json_string, content_type);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_Membership_Types_Get(string params_json)
        {
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);
            string[] filter_columns = new string[] { "id", "title" };
            string[] select_columns = new string[] { "id", "title" };
            App_Model app_model = new App_Model();
            string return_json_string = App_Lib.Utils.DAL_Utils.List_Filter_Sort_Page_Get<App_Membership_Types>(app_model.App_Membership_Types.ToList<App_Membership_Types>(), params_jsoned, filter_columns, select_columns).ToString();
            return Content(return_json_string, content_type);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_User_Profiles_Get(string params_json)
        {
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);
            string[] filter_columns = new string[] { "id", "name" };
            string[] select_columns = new string[] { "id", "name", "birthdate", "app_user_id" };
            App_Model app_model = new App_Model();
            string return_json_string = App_Lib.Utils.DAL_Utils.List_Filter_Sort_Page_Get<App_User_Profiles>(app_model.App_User_Profiles.ToList<App_User_Profiles>(), params_jsoned, filter_columns, select_columns).ToString();
            return Content(return_json_string, content_type);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_User_Area_Memberships_Get(string params_json)
        {
            System.Diagnostics.Debug.WriteLine(params_json);
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);
            string[] filter_columns = new string[] { "id", "user_id", "area_id", "membership_type_id" };
            string[] select_columns = new string[] { 
                "id", 
                "user_id", 
                "area_id", 
                "membership_type_id", 
                "App_Areas.title as App_Area_Title", 
                "App_User_Profiles.name as App_User_Profile_Name", 
                "App_Membership_Types.title as App_Membership_Types_Title" 
            };
            App_Model app_model = new App_Model();
            string return_json_string = App_Lib.Utils.DAL_Utils.List_Filter_Sort_Page_Get<App_User_Area_Memberships>(app_model.App_User_Area_Memberships.ToList<App_User_Area_Memberships>(), params_jsoned, filter_columns, select_columns).ToString();
            return Content(return_json_string, content_type);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_User_Area_Memberships_AddUpdate(string params_json)
        {
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);

            bool is_success = false;
            string message = "";
            try{
                App_Lib.Utils.User_Utils.AreaMemberships_AddEdit(
                    Convert.ToInt32(params_jsoned["area_id"]),
                    Convert.ToInt32(params_jsoned["membership_type_id"]),
                    Convert.ToInt32(params_jsoned["user_id"]),
                    Convert.ToInt32(params_jsoned["id"])
                );
                is_success = true;
                message = "Record Added/Updated.";
            }catch(Exception e){
                message = "An error occurred: " + e.Message;
            }

            string result = String.Format(@"{{
                ""success"":{0},
                ""message"":""{1}""
            }}", is_success.ToString().ToLower(), message);


            return Content(result.ToString(), content_type);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ContentResult App_User_Area_Memberships_Delete(string params_json)
        {
            JObject params_jsoned = App_Lib.Utils.App_Utils.Parse_JSON(params_json);

            var app_model = new App_Model();
            bool is_success = false;
            string message = "";
            try
            {
                App_User_Area_Memberships user_area_membership = app_model.App_User_Area_Memberships.Find(Convert.ToInt32(params_jsoned["id"]));
                app_model.App_User_Area_Memberships.Remove(user_area_membership);
                app_model.SaveChanges();
                is_success = true;
                message = "Record Deleted.";
            }
            catch
            {
                message = "An error occurred.";
            }

            string result = String.Format(@"{{
                ""success"":{0},
                ""message"":""{1}""
            }}", is_success.ToString().ToLower(), message);


            return Content(result.ToString(), content_type);
        }
    }
}