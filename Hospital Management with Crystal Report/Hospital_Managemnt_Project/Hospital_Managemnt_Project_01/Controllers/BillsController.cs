using Hospital_Managemnt_Project_01.Models;
using Hospital_Managemnt_Project_01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Managemnt_Project_01.Controllers
{
    public class BillsController : Controller
    {
        HospitalDbContext db =  new HospitalDbContext();
        // GET: Bills
        public ActionResult Index()
        {
            return View(db.Bills.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.patients = db.Patients.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BillInputModel model)
        {
            if(ModelState.IsValid)
            {
                Bill b = new Bill
                {
                    BillDate = model.BillDate,
                    SeatRent = model.SeatRent,
                    OtherCharge = model.OtherCharge,
                    PatientId = model.PatientId
                };
                db.Bills.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patients = db.Patients.ToList();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var bill = db.Bills.FirstOrDefault(x=> x.BillId== id);
            ViewBag.patients = db.Patients.ToList();
            return View(new BillEditModel
            {
                BillId = bill.BillId,
                BillDate = bill.BillDate,
                SeatRent = bill.SeatRent,
                OtherCharge = bill.OtherCharge,
                PatientId = bill.PatientId
            });
        }
        [HttpPost]
        public ActionResult Edit(BillEditModel model)
        {
            Bill data = db.Bills.First(x=> x.BillId== model.BillId);
            if(ModelState.IsValid)
            {
                data.BillDate = model.BillDate;
                data.SeatRent = model.SeatRent;
                data.OtherCharge = model.OtherCharge;
                data.PatientId = model.PatientId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patients = db.Patients.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.Bills.FirstOrDefault(x => x.BillId == id);
            if (existing != null)
            {
                db.Bills.Remove(existing);
                db.SaveChanges();
                return Json(existing.BillId);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}