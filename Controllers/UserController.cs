using ELearnApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ELearnApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            string email = Session["userId"].ToString();
            ModelContext db = new ModelContext();

            List<UserHome> list = new List<UserHome>();
            List<User_Service> USList = db.User_Services.ToList();
            
            foreach(var us in USList)
            {
               if(us.EmailId==email)
               {
                    string sid = us.ServiceId;
                    List<Service_Course> SCList = db.Service_Courses.ToList();
                    foreach(var sc in SCList)
                    {
                        if(sc.ServiceId==sid)
                        {
                            UserHome userHome = new UserHome();
                            userHome.Id = email;
                            userHome.CourseCode = sc.CourseId;
                            userHome.CourseName = db.Courses.Find(userHome.CourseCode).Name;
                            userHome.Servicename = db.Services.Find(sid).Name;
                            userHome.StartDate = db.Services.Find(sid).StartDate.ToString();
                            userHome.EndDate = db.Services.Find(sid).StartDate.AddMonths(db.Services.Find(sid).Duration).ToString();
                            userHome.Detail = db.Courses.Find(userHome.CourseCode).Detail;

                            list.Add(userHome);
                        }
                    }
               }
            }
            
            return View(list);
            //return Content("hvshdu"+USList.Count);
        }

        public ActionResult UVIndex()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            
            ModelContext db = new ModelContext();
            List<OnlineServices> list = new List<OnlineServices>();
            List<Service> services = db.Services.ToList();
            foreach(Service s in services)
            {
                OnlineServices t = new OnlineServices();
                t.ServiceName = s.Name;
                t.Courses = new List<Course>();
                List<Service_Course> sc = db.Service_Courses.ToList();
                foreach(Service_Course temp in sc)
                {
                    if(temp.ServiceId==s.ServiceId)
                    {
                        t.Courses.Add(db.Courses.Find(temp.CourseId));
                    }
                }
                t.Amount = s.Amount;
                list.Add(t);

            }

            return View(list);
        }

        public ActionResult Transaction()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            return Content("Transaction Successful");
        }

        public ActionResult History()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext context = new ModelContext();
            List<Transaction> transactionList = context.Transactions.ToList();

            List<TransactionDetail> list = new List<TransactionDetail>();
            foreach (Transaction t in transactionList)
            {
                if (t.EmailId == Session["userId"].ToString())
                {
                    TransactionDetail temp = new TransactionDetail();
                    temp.TransactionId = t.TransactionId;
                    temp.ServiceName = context.Services.Find(t.ServiceId).Name;
                    temp.StartDate = context.Services.Find(t.ServiceId).StartDate;
                    temp.Amount = context.Services.Find(t.ServiceId).Amount;

                    list.Add(temp);
                }
            }
            return View(list);
        }

        public ActionResult Edit()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            User user = db.Users.Find(Session["userId"].ToString());
            Address address = db.Addresses.Find(user.AddressId);
            UserDetail userDetail = new UserDetail();
            
            userDetail.EmailId = user.EmailId;
            userDetail.Name = user.Name;
            userDetail.Age = user.Age;
            userDetail.Gender = (Gender)user.Gennder;
            if (user.RoleId == 2)
                userDetail.RoleName = RoleType.User;
            else if (user.RoleId == 3)
                userDetail.RoleName = RoleType.Vendor;
            userDetail.UserStatus = user.UserStatus;

            userDetail.AddressLine1 = address.AddressLine1;
            userDetail.AddressLine1 = address.AddressLine1;
            userDetail.City = address.City;
            userDetail.State = address.State;
            userDetail.Country = address.Country;
            userDetail.PinCode = address.PinCode;

            return View(userDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( UserDetail details)
        {

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            var context = new ModelContext();
            User user = context.Users.Find(Session["userId"].ToString());
            Address address = context.Addresses.Find(user.AddressId);

            user.Name = details.Name;
            user.Age = details.Age;
            user.Gennder = (Gennder)details.Gender;
        //    user.ContactNumber = details.;
            user.EmailId = details.EmailId;
        //    user.Password = details.Password;

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

            context.SaveChanges();

            user.AddressId = context.Addresses.Max(x => x.AddressId);
       
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Deactivate()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            db.Users.Find(Session["userId"].ToString()).UserStatus = ActiveStatus.No;
            db.SaveChanges();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
            //User user = new ModelContext().Users.Find(Session["userId"].ToString());
            //return View(user);
        }


    }
}