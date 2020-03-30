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
            if (Session["role"] != null)
            {

                if (Session["role"].ToString() == "1")
                {
                    //return Content("Admin Page");
                    return RedirectToAction("Index", "Admin");
                }
                else if (Session["role"].ToString() == "2")
                {
                    //return Content("User Page");
                    return RedirectToAction("Index", "User");
                }
                else if (Session["role"].ToString() == "3")
                {
                    //return Content("Vendor Page");
                    return RedirectToAction("Index", "Vendor");
                }

            }
            if (new ModelContext().Users.ToList().Count == 0)
                return RedirectToAction("SignUpAdmin");
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
                if (a.EmailId == login.EmailId && a.Password == login.Password )
                {
                    if (a.UserStatus == ActiveStatus.No)              
                        return RedirectToAction("Index", "User");
                    Session["userId"] = a.EmailId;
                    if (a.RoleId == 1)
                    {
                        Session["roleId"] = 1;
                        //return Content("Admin Page");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (a.RoleId == 2)
                    {
                        Session["roleId"] = 2;
                        //return Content("User Page");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        Session["roleId"] = 3;
                        //return Content("Vendor Page");
                        return RedirectToAction("Index", "Vendor");
                    }
                }

            }
            return View();
        }

        public ActionResult SignUpAdmin()
        {

            if (new ModelContext().Users.ToList().Count != 0)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult SignUpAdmin(AdminSignUpDetail details)
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

            user.RoleId = 1;

            address.AddressLine1 = details.AddressLine1;
            address.AddressLine2 = details.AddressLine2;
            address.City = details.City;
            address.State = details.State;
            address.Country = details.Country;
            address.PinCode = details.PinCode;

            context.Addresses.Add(address);
            context.SaveChanges();

            user.AddressId = context.Addresses.Max(x => x.AddressId);
            context.Users.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        public ActionResult SignUp()
        {
            if (Session["role"] != null)
            {

                if (Session["role"].ToString() == "1")
                {
                    //return Content("Admin Page");
                    return RedirectToAction("Index", "Admin");
                }
                else if (Session["role"].ToString() == "2")
                {
                    //return Content("User Page");
                    return RedirectToAction("Index", "User");
                }
                else if (Session["role"].ToString() == "3")
                {
                    //return Content("Vendor Page");
                    return RedirectToAction("Index", "Vendor");
                }

            }
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

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetDetails details)
        {
            User user = new ModelContext().Users.Find(details.EmailId);
            if (user == null)
                return View("ForgetError");
            if(user.ContactNumber!=details.ContactNumber)
                return View("ForgetError");
            NewPassword pass = new NewPassword();
            pass.EmailId = details.EmailId;
            return View("CreateNewPassword",pass);
        }

        public ActionResult CreatePassword(NewPassword pass)
        {
            ModelContext context = new ModelContext();
            User user = context.Users.Find(pass.EmailId);
            user.Password = pass.Password;
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