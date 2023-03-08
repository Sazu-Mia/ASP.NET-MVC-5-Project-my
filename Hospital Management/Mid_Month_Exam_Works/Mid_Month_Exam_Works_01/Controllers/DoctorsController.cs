using Mid_Month_Exam_Works_01.Models;
using Mid_Month_Exam_Works_01.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Month_Exam_Works_01.Controllers
{
    public class DoctorsController : Controller
    {
        DoctorDbContext db = new DoctorDbContext();
        // GET: Doctors
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DoctorList()
        {
            return PartialView("_DoctorPartial", db.Doctors.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult Create(DoctorInputModel d)
        {
            if(ModelState.IsValid)
            {
                var data = new Doctor
                {
                    DoctorName = d.DoctorName,
                    BirthDate = d.BirthDate,
                    Qualification = d.Qualification,
                    Salary= d.Salary,
                    IsAvaliable= d.IsAvaliable
                };
                if(d.Picture.ContentLength> 0)
                {
                    string ext = Path.GetExtension(d.Picture.FileName);
                    string FileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())+ext;
                    string savePath = Path.Combine(Server.MapPath("~/Uploads"), FileName);
                    d.Picture.SaveAs(savePath);
                    data.Picture = FileName;
                }
                db.Doctors.Add(data);
                db.SaveChanges();
                return PartialView("_SusscessPartial");
            }
            return PartialView("_FailPartial");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Doctors = db.Doctors.ToList();
            var doc = db.Doctors.FirstOrDefault(x=> x.DoctorId== id);
            if (doc == null)
                return new HttpNotFoundResult();
            ViewBag.Picture = doc.Picture;
            return View(new DoctorEditModel
            {
                DoctorId = doc.DoctorId,
                DoctorName = doc.DoctorName,
                BirthDate= doc.BirthDate,
                Qualification = doc.Qualification,
                Salary = doc.Salary,
                IsAvaliable = doc.IsAvaliable.HasValue ? doc.IsAvaliable.Value : false
            });
        }
        [HttpPost]
        public PartialViewResult Edit(DoctorEditModel d)
        {
            Doctor data = db.Doctors.FirstOrDefault(x=> x.DoctorId== d.DoctorId);
            if(data == null)
                return PartialView("_FailPartial");
            if (ModelState.IsValid)
            {
                data.DoctorName = d.DoctorName;
                data.BirthDate = d.BirthDate;
                data.Qualification = d.Qualification;
                data.Salary = d.Salary;
                if (d.Picture != null)
                {
                    string ext = Path.GetExtension(d.Picture.FileName);
                    string FileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Uploads"), FileName);
                    d.Picture.SaveAs(savePath);
                    data.Picture = FileName;
                }
                db.SaveChanges();
                return PartialView("_SusscessPartial");
            }
            return PartialView("_FailPartial");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if(db.Patients.Any(x=> x.DoctorId == id))
            {
                return Json(new { success = false, id=0});
            }
            else
            {
                var d = new Doctor { DoctorId=id};
                db.Entry(d).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, id = id });
            }
        }
        [Route("Custom/Master")]
        public ActionResult MasterDetailInsert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMaster(DoctorInputModel data)
        {
            if (ModelState.IsValid)
            {
                Doctor d = new Doctor
                {
                    DoctorName = data.DoctorName,
                    BirthDate = data.BirthDate,
                    Qualification = data.Qualification,
                    Salary = data.Salary,
                    IsAvaliable = data.IsAvaliable
                };
                if (data.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    data.Picture.SaveAs(savePath);
                    d.Picture = fileName;
                }
                db.Doctors.Add(d);
                db.SaveChanges();
                return Json(d);
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult AddPatients(int id, Patient[] data)
        {
            var d = db.Doctors.FirstOrDefault(x => x.DoctorId == id);
            if (d == null) return new HttpNotFoundResult();
            foreach (var p in data)
            {
                d.Patients.Add(p);
            }
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}