using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;

using makerspace.Models;

namespace makerspace.Utils
{
    public static class User_Utils
    {

        public static int Calc_Age(DateTime bday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - bday.Year;
            if (now < bday.AddYears(age)) age--;

            return age;
        }

        public static App_User_Profiles Get_App_User_Profile()
        {       
            var app_model = new App_Model();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string app_user_id = HttpContext.Current.User.Identity.GetUserId();
                try
                {
                    return app_model.App_User_Profiles.Where(up => up.app_user_id == app_user_id).Single<App_User_Profiles>();
                }
                catch { throw new Exception("Can't find this User Profile."); }
            }
            return null;
        }

        public static bool AreaMemberships_AddEdit(int area_id, int membership_type_id, int user_id = 0, int id=0)
        {
            var app_model = new App_Model();

            App_User_Profiles user_profile = User_Utils.Get_App_User_Profile();
            if (!(user_id > 0)) { 
                user_id = user_profile.id;
            }
            App_Areas area = app_model.App_Areas.Where(a => a.id == area_id).Single<App_Areas>();
            if(area.requirement_age != null){
                if (User_Utils.Calc_Age(user_profile.birthdate) < area.requirement_age)
                    throw new Exception("Not of appropriate age.");
            }

            App_User_Area_Memberships item = null;
            if (id > 0)
                item = app_model.App_User_Area_Memberships.Find(id);
            else
                item = new App_User_Area_Memberships();

            if (item != null)
            {
                item.area_id = area_id;
                item.membership_type_id = membership_type_id;
                item.user_id = user_id;
                if (!(id > 0))
                    app_model.App_User_Area_Memberships.Add(item);


                //Check # of shop stewards
                App_Membership_Types member_type_steward = app_model.App_Membership_Types.Where(mt => mt.title == "Shop Steward").Single();
                if (member_type_steward.id == membership_type_id)
                {
                    if (app_model.App_User_Area_Memberships.Where(uam => (uam.area_id == area_id) && (uam.membership_type_id == member_type_steward.id)).Count() > 0)
                        throw new Exception("Too many Stewards. Only 1 allowed. You may need to demote someone.");
                     
                }

                try
                {
                    app_model.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("User is already a member.");

                }
            }
            return true;
        }


    }
}