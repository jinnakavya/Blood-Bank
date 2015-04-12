using Blood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blood.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        [HttpGet]
        [ActionName("Register")]
        public ActionResult Register_Get()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult Register_Post(user u)
        {
            if (ModelState.IsValid)
            {
                using (BloodDatabaseEntities dc = new BloodDatabaseEntities())
                {
                    //you should check duplicate registration here 
                    dc.users.Add(u);
                    dc.SaveChanges();
                    ModelState.Clear();

                    u = null;
                    ViewBag.Message = "Successfully Registration Done";
                }
            }
            return View(u);
        }

        [HttpGet]
        [ActionName("Search")]
        public ActionResult Search_Get()
        {
            return View("Register");
        }

        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(string State, string blood_type)
        {
            List<user> items;
            BloodDatabaseEntities db = new BloodDatabaseEntities();

            if (State != "Choose a state" && blood_type != "Choose blood type")
            {
            items = db.users.Where(x=>x.State == State && x.BloodType == blood_type).ToList();
            }
            else if (State == "Choose a state" && blood_type != "Choose blood type")
            {
            items = db.users.Where(x=>x.BloodType == blood_type).ToList();
            }
            else if (State != "Choose a state" && blood_type == "Choose blood type")
            {
            items = db.users.Where(x => x.State == State).ToList();
            }
            else
            {
            items = db.users.ToList();
            }


            if(items.Count != 0)
            {
            ViewBag.Summary = "";
            return View("Result",items);
            }
            else
            {
                ViewBag.Summary = "No Results Found";
                return View("Result", items);
            }
        }


    }
}
