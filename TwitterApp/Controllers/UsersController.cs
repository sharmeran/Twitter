using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TwitterApp.BusinessDomain;
using TwitterApp.Constants;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class UsersController : Controller
    {

        public ActionResult Index()
        {
            UserDomain userDomain = new UserDomain();
            return View(userDomain.FindAll());
        }

        public ActionResult Add()
        {

            var user = Request.Form;
            if (user.AllKeys.Count() == 0)
            {
                return View();
                
            }
            else
            {
                UserDomain userDomain = new UserDomain();
                TwitterApp.Models.User userEntity = new User();
                userEntity.Name = user.GetValues(UserConstants.Name)[0];
                userEntity.Username = user.GetValues(UserConstants.Username)[0];
                userEntity.Password = user.GetValues(UserConstants.Password)[0];
                ViewData["ActionState"] = userDomain.Add(userEntity);
                return View();

            }


        }

        public ActionResult Delete(string id)
        {

           
            if (id==string.Empty)
            {
                return RedirectToAction("Index", "Users");
                
            }
            else
            {
                UserDomain userDomain = new UserDomain();
                TwitterApp.Models.User userEntity = new User();
                userEntity.ID = id;                
                ViewData["ActionState"] = userDomain.Delete(userEntity);
                return RedirectToAction("Index", "Users");

            }


        }

        public ActionResult Edit(string id)
        {
            var data = Request.Form;

            if (data.AllKeys.Count() == 0)
                return View();
            else
            {
                UserDomain userDomain = new UserDomain();
                TwitterApp.Models.User userEntity = new User();
                userEntity.Name = data.GetValues(UserConstants.Name)[0];                
                userEntity.Password = data.GetValues(UserConstants.Password)[0];
                userEntity.ID =id;
                ViewData["ActionState"] = userDomain.Update(userEntity);
                return View();
            }
        }
    }
}
