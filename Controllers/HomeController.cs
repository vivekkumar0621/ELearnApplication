using ELearnApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearnApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            Session.RemoveAll();
            var context = new ModelContext();
            List<User> obj = context.Users.ToList();

            foreach (var a in obj)
            {

                if (a.EmailId == login.EmailId && a.Password == login.Password)
                {
                    Session["userId"] = a.EmailId;
                    if (a.RoleId == 1)
                    {
                        //return Content("Admin Page");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (a.RoleId == 2)
                    {
                        //return Content("User Page");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        //return Content("Vendor Page");
                        return RedirectToAction("Index", "Vendor");
                    }
                }

            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(SignUpDetail details)
        {
            var context = new ModelContext();
            User user = new User();
            Address address = new Address();

            user.Name = details.Name;
            user.Age = details.Age;
            user.Gennder = (Gennder)details.Gender;
            user.ContactNumber = details.ContactNumber;
            user.EmailId = details.EmailId;
            user.Password = details.Password;

            if (details.RoleName == RoleType.User)
                user.RoleId = 2;
            else if (details.RoleName == RoleType.Vendor)
                user.RoleId = 3;
            else
                user.RoleId = 1;

            address.AddressLine1 = details.AddressLine1;
            address.AddressLine2 = details.AddressLine2;
            address.City = details.City;
            address.State = details.State;
            address.Country = details.Country;
            address.PinCode = details.PinCode;

            context.Addresses.Add(address);
            context.SaveChanges();

            user.AddressId=context.Addresses.Max(x => x.AddressId);
            context.Users.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index"); 
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}