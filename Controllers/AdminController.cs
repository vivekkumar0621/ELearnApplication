using ELearnApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ELearnApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["userId"]==null || Session["userId"].ToString() == "" || Session["roleId"].ToString()!="1")
                return RedirectToAction("Index", "Home");
            //return View();
            //string EmailId = Session["userId"].ToString();

            ModelContext context = new ModelContext();

            List<User> users = context.Users.ToList();
            List<UserDetail> list = new List<UserDetail>();

            UserDetail userDetail;

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = context.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
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
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                
                list.Add(userDetail);

            }
            UserServiceCourse userServiceCourse = new UserServiceCourse();
            userServiceCourse.Courses = context.Courses.ToList();
            userServiceCourse.Services = context.Services.ToList();
            userServiceCourse.UserDetails = list;
            
            return View(userServiceCourse);

            //return Content("check" + EmailId);
        }

        //string service;
        public ActionResult CourseDetail(string Serviceid)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "" || Session["roleId"].ToString() != "1")
                return RedirectToAction("Index", "Home");
            //service = Serviceid;
            TempData["serviceid"] = Serviceid;
            
            ModelContext context = new ModelContext();
       //     ViewBag.Name = context.Services.Where(x => x.ServiceId==Serviceid).Select(a=>a.Name);
            List<Service_Course> service_Courses = context.Service_Courses.Where(a => a.ServiceId == Serviceid).ToList();
            List<Course> courses = new List<Course>();
            Course course;
            foreach (Service_Course sc in service_Courses)
            {
                course = context.Courses.SingleOrDefault(a => a.CourseId == sc.CourseId);
                courses.Add(course);
            }
            return View(courses);
            //return Content("Course-->"+ Serviceid);
        }

        public ActionResult EditCourse(string id)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "" || Session["roleId"].ToString() != "1")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course courses = new ModelContext().Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            //return Content("Course2-->" + id);
            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "CourseId,Name,CourseStatus")] Course course)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Content("Course3-->" + course.CourseId);
            //return View(course);
        }

        public ActionResult SortByName()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            List<User> users = db.Users.OrderBy(a => a.Name).ToList();


            UserDetail userDetail;
            List<UserDetail> list = new List<UserDetail>();

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = db.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
                userDetail.EmailId = user.EmailId;
                userDetail.Name = user.Name;
                userDetail.Age = user.Age;
                userDetail.Gender = (Gender)user.Gennder;
                if (user.RoleId == 2)
                    userDetail.RoleName = RoleType.User;
                else if (user.RoleId == 3)
                    userDetail.RoleName = RoleType.Vendor;
                //userDetail.UserStatus = user.UserStatus;
                userDetail.UserStatus = ActiveStatus.Yes;

                userDetail.AddressLine1 = address.AddressLine1;
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                list.Add(userDetail);

            }
            UserServiceCourse all = new UserServiceCourse();
            all.Courses = db.Courses.ToList();
            all.Services = db.Services.ToList();
            all.UserDetails = list;


            return View("Index", all);
        }

        public ActionResult SortByAge()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            List<User> users = db.Users.OrderBy(a => a.Age).ToList();


            UserDetail userDetail;
            List<UserDetail> list = new List<UserDetail>();

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = db.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
                userDetail.EmailId = user.EmailId;
                userDetail.Name = user.Name;
                userDetail.Age = user.Age;
                userDetail.Gender = (Gender)user.Gennder;

                if (user.RoleId == 2)
                    userDetail.RoleName = RoleType.User;
                else if (user.RoleId == 3)
                    userDetail.RoleName = RoleType.Vendor;
                //userDetail.UserStatus = user.UserStatus;
                userDetail.UserStatus = ActiveStatus.Yes;

                userDetail.AddressLine1 = address.AddressLine1;
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                list.Add(userDetail);

            }

            UserServiceCourse all = new UserServiceCourse();
            all.Courses = db.Courses.ToList();
            all.Services = db.Services.ToList();
            all.UserDetails = list;

            return View("Index", all);
        }

        public ActionResult Search(string searching)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            List<User> users = db.Users.Where(a => a.Name.Contains(searching)).ToList();


            UserDetail userDetail;
            List<UserDetail> list = new List<UserDetail>();

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = db.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
                userDetail.EmailId = user.EmailId;
                userDetail.Name = user.Name;
                userDetail.Age = user.Age;
                userDetail.Gender = (Gender)user.Gennder;

                if (user.RoleId == 2)
                    userDetail.RoleName = RoleType.User;
                else if (user.RoleId == 3)
                    userDetail.RoleName = RoleType.Vendor;
                //userDetail.UserStatus = user.UserStatus;
                userDetail.UserStatus = ActiveStatus.Yes;

                userDetail.AddressLine1 = address.AddressLine1;
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                list.Add(userDetail);

            }

            UserServiceCourse all = new UserServiceCourse();
            all.Courses = db.Courses.ToList();
            all.Services = db.Services.ToList();
            all.UserDetails = list;

            return View("Index", all);
        }

        public ActionResult EditUser(string EmailId)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            if (EmailId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ModelContext context = new ModelContext();
            User user = context.Users.Find(EmailId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
            //return Content("shhjas" + EmailId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //        public ActionResult EditUser([Bind(Include = "UserID,Name,Age,Gender,UserRole,UserStatus,AddressId")] User user)
        public ActionResult EditUser( User user)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            //if (ModelState.IsValid)
            //{
            //    db.Entry(user).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            User u1 = db.Users.Find(user.EmailId);
        //    return Content(user.EmailId);
        //    db.Users.Remove(u1);
            u1.UserStatus = user.UserStatus;
        //    db.Users.Add(u1);
            db.SaveChanges();



            //return View(user);
            return RedirectToAction("Index");
        }


        public ActionResult EditService(string id)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService([Bind(Include = "ServiceId,Name,Duration,Amount,StartingDate,ServiceStatus")] Service service)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return Content("ghsdgh");
            return View(service);
        }

        public ActionResult AllSubscriber()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            //List<User> users = db.Users.Where(a => a.UserRole == RoleType.User).ToList();
            List<User> users = db.Users.Where(a => a.RoleId == 2).ToList();

            UserDetail userDetail;
            List<UserDetail> list = new List<UserDetail>();

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = db.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
                userDetail.EmailId = user.EmailId;
                userDetail.Name = user.Name;
                userDetail.Age = user.Age;
                userDetail.Gender = (Gender)user.Gennder;
                if (user.RoleId == 2)
                    userDetail.RoleName = RoleType.User;
                else if (user.RoleId == 3)
                    userDetail.RoleName = RoleType.Vendor;

                
                //userDetail.UserStatus = user.UserStatus;

                userDetail.AddressLine1 = address.AddressLine1;
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                list.Add(userDetail);

            }


            UserServiceCourse all = new UserServiceCourse();
            all.Courses = db.Courses.ToList();
            all.Services = db.Services.ToList();
            all.UserDetails = list;

            return View("Index", all);
        }

        public ActionResult AllVendor()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            // List<User> users = db.Users.Where(a => a.UserRole == RoleType.Vendor).ToList();
            List<User> users = db.Users.Where(a => a.RoleId == 3).ToList();

            UserDetail userDetail;
            List<UserDetail> list = new List<UserDetail>();

            foreach (User user in users)
            {
                userDetail = new UserDetail();
                Address address = db.Addresses.SingleOrDefault(m => m.AddressId == user.AddressId);
                userDetail.EmailId = user.EmailId;
                userDetail.Name = user.Name;
                userDetail.Age = user.Age;
                userDetail.Gender = (Gender)user.Gennder;
                if (user.RoleId == 2)
                    userDetail.RoleName = RoleType.User;
                else if (user.RoleId == 3)
                    userDetail.RoleName = RoleType.Vendor;

                //userDetail.UserStatus = user.UserStatus;

                userDetail.AddressLine1 = address.AddressLine1;
                userDetail.AddressLine2 = address.AddressLine2;
                userDetail.City = address.City;
                userDetail.State = address.State;
                userDetail.Country = address.Country;
                userDetail.PinCode = address.PinCode;
                list.Add(userDetail);

            }
            UserServiceCourse all = new UserServiceCourse();
            all.Courses = db.Courses.ToList();
            all.Services = db.Services.ToList();
            all.UserDetails = list;



            return View("Index", all);
        }

        public ActionResult CreateCourse()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseNew([Bind(Include = "CourseId,Name,CourseStatus")] Course course)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            Service_Course service_Course = new Service_Course();

            service_Course.ServiceId = TempData["serviceid"].ToString();

            string cid = db.Courses.Max(x => x.CourseId);
            int id=Convert.ToInt32(cid.Substring(1,cid.Length-1))+1;
            cid = cid.Substring(0, 1) + id;

            course.CourseId = cid;

            service_Course.CourseId = course.CourseId;

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                CreateServiceCourse(service_Course);
                return RedirectToAction("Index");
            }
            return View(course);
           // return Content(""+course.Name);
        }

        public ActionResult CreateServiceCourse(Service_Course service_Course)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            db.Service_Courses.Add(service_Course);
            db.SaveChanges();
            return null;
        }

        public ActionResult Create()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            return View("AddServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,Name,Duration,Amount,StartingDate,ServiceStatus")] Service service)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }


    }
}