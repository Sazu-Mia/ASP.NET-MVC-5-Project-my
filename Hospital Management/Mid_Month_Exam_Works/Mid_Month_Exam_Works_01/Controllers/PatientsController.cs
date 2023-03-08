using Mid_Month_Exam_Works_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Mid_Month_Exam_Works_01.Controllers
{
    public class PatientsController : Controller
    {
        DoctorDbContext db = new DoctorDbContext();
        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.Include(x=> x.Doctor).ToList());
        }

        
        public ActionResult Create()
        {
            ViewBag.Doctors = db.Doctors.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Patient p)
        {
            if(ModelState.IsValid)
            {
                db.Patients.Add(p);
                db.SaveChanges();
                return PartialView("_SusscessPartial");
            }
            return PartialView("_FailPartial");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Doctors = db.Doctors.ToList();
            return View(db.Patients.FirstOrDefault(x=> x.PatientId==id));
        }
        [HttpPost]
        public ActionResult Edit(Patient p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State= EntityState.Modified;
                db.SaveChanges();
                return PartialView("_SusscessPartial");
            }
            return PartialView("_FailPartial");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = new Patient { PatientId = id };
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { id = id });
        }
    }
}