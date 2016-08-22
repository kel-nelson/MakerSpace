using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using makerspace.Models;
using makerspace.Utils;
namespace makerspace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //ViewBag.ReturnUrl = Request.RawUrl;
            var app_model = new App_Model();
            ViewData["user_profile"] = User_Utils.Get_App_User_Profile();
            /*if (User.Identity.IsAuthenticated)
            {
                string app_user_id = User.Identity.GetUserId();
                ViewData["user_profile"] = app_model.App_User_Profiles.Where(up => up.app_user_id == app_user_id).Single();
            }*/
            ViewData["areas"] = app_model.App_Areas;
           
            return View();
        }


    }
}