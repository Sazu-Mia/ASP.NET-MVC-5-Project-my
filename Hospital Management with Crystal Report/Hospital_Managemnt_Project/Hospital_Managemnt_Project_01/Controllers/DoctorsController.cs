using Hospital_Managemnt_Project_01.Models;
using Hospital_Managemnt_Project_01.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Managemnt_Project_01.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        HospitalDbContext db = new HospitalDbContext();
        // GET: Doctors
        public ActionResult Index()
        {
            return View(db.Doctors.Include(x=> x.Patients).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DoctorInputModel model)
        {
            if (ModelState.IsValid)
            {
                Doctor d = new Doctor
                {
                    DoctorName = model.DoctorName,
                    BirthDate = model.BirthDate,
                    DoctorFee = model.DoctorFee,
                    IsAvaliable = model.IsAvaliable,
                    Picture = model.Picture
                };
                foreach (var p in model.Patients)
                {
                    d.Patients.Add(p);
                }
                db.Doctors.Add(d);
                db.SaveChanges();
                return Json(new { id = d.DoctorId });
            }
            return View(model);
        }
        public PartialViewResult AddPatientToForm(DoctorInputModel d = null, int? index = null)
        {
            if (d == null) d = new DoctorInputModel();
            if (index.HasValue)
            {
                if (index < d.Patients.Count)
                {
                    d.Patients.RemoveAt(index.Value);
                }
            }
            else
            {
                d.Patients.Add(new Patient());
            }
            return PartialView("_PatientInputPartial", d);
        }
        public ActionResult UploadImage(int id, ImageUpload pic)
        {
            if (ModelState.IsValid)
            {
                if (pic.Picture != null)
                {
                    Doctor d = db.Doctors.First(x => x.DoctorId == id);
                    string ext = Path.GetExtension(pic.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    pic.Picture.SaveAs(savePath);
                    d.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);
                }
            }
            return Json(null);
        }
        public ActionResult Edit(int id)
        {
            var doctor = db.Doctors.First(x=> x.DoctorId==id);
            return View(new DoctorEditModel
            {
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.DoctorName,
                BirthDate = doctor.BirthDate,
                DoctorFee = doctor.DoctorFee,
                IsAvaliable = doctor.IsAvaliable,
                Picture = doctor.Picture,
                Patients = doctor.Patients.ToList()
            });
        }
        [HttpPost]
        public ActionResult Edit(DoctorEditModel model)
        {
            var existing = db.Doctors.FirstOrDefault(x => x.DoctorId == model.DoctorId);
            if (ModelState.IsValid)
            {
                existing.DoctorName = model.DoctorName;
                existing.BirthDate = model.BirthDate;
                existing.DoctorFee = model.DoctorFee;
                existing.IsAvaliable = model.IsAvaliable;
                db.Patients.RemoveRange(existing.Patients.ToList());    
                foreach(var p in model.Patients)
                {
                    p.DoctorId = existing.DoctorId;
                    db.Patients.Add(p);
                }
                db.SaveChanges();
                return Json(existing.DoctorId);
            }
            return Json(null);
        }
        public PartialViewResult AddPatientsToEditForm(DoctorEditModel d, int ? index = null)
        {
            if(index.HasValue)
            {
                if(index < d.Patients.Count)
                {
                    d.Patients.RemoveAt(index.Value);
                }
            }
            return PartialView("_PatientsEditForm", d);
        }
        public PartialViewResult AddMore(DoctorEditModel d, int ? index = null)
        {
            if(d==null) d= new DoctorEditModel();
            if(index.HasValue)
            {
                if(index < d.Patients.Count)
                {
                    d.Patients.RemoveAt(index.Value);
                }
            }
            else
            {
                d.Patients.Add(new Patient());
            }
            return PartialView("_PatientsEditForm", d);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.Doctors.FirstOrDefault(x=> x.DoctorId==id);
            if(existing!=null)
            {
                db.Doctors.Remove(existing);    
                db.SaveChanges();
                return Json(existing.DoctorId);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}