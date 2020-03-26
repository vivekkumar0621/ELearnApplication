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
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            string email = Session["userId"].ToString();
            ModelContext db = new ModelContext();

            List<UserHome> list = new List<UserHome>();
            List<User_Service> USList = db.User_Services.ToList();

            foreach (var us in USList)
            {
                if (us.EmailId == email)
                {
                    string sid = us.ServiceId;
                    List<Service_Course> SCList = db.Service_Courses.ToList();
                    foreach (var sc in SCList)
                    {
                        if (sc.ServiceId == sid)
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
        }

        public ActionResult VendorManageServices()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();

            List<Service> list = new List<Service>();
            
            List<User_Service> user_Services = db.User_Services.ToList();
            foreach(User_Service us in user_Services)
            {
                if (us.EmailId == Session["userId"].ToString())
                {
                    list.Add(db.Services.Find(us.ServiceId));
                }
            }

           
        
            return View("VendorManageService",list);
        }

        public ActionResult EditService(string sid)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            //string id = Session["userId"].ToString();
            if (sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(sid);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService( Service service)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("Index", "Home");
            ModelContext db = new ModelContext();
            Service s = db.Services.Find(service.ServiceId);
            s.Name = service.Name;
            s.StartDate = service.StartDate;
            s.Duration = service.Duration;
            s.Amount = service.Amount;
            
                db.SaveChanges();
                return RedirectToAction("Index");
            
            //return Content("ghsdgh");
           
        }

        
        public ActionResult CourseDetail(string Serviceid)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
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
            if (Session["userId"] == null || Session["userId"].ToString() == "")
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


    }
}