using Hospital_Managemnt_Project_01.Models;
using Hospital_Managemnt_Project_01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Managemnt_Project_01.Controllers
{
    public class BillDetailsController : Controller
    {
        HospitalDbContext db = new HospitalDbContext();
        // GET: BillDetails
        public ActionResult Index()
        {
            return View(db.BillDetails.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Bill = db.Bills.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BillDetailsInputModel model)
        {
            if (ModelState.IsValid)
            {
                BillDetail b = new BillDetail
                {
                    Advance = model.Advance,
                    HealthCard= model.HealthCard,
                    BillId= model.BillId
                };
                db.BillDetails.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bill = db.Bills.ToList();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var billDetail = db.BillDetails.First(x=> x.BillDetailId== id);
            ViewBag.Bill = db.Bills.ToList();
            return View(new BillDetailEditModel
            {
                BillDetailId = billDetail.BillId,
                Advance = billDetail.Advance,
                HealthCard= billDetail.HealthCard,
                BillId= billDetail.BillId
            });
        }
        [HttpPost]
        public ActionResult Edit(BillDetailsInputModel model)
        {
            BillDetail data = db.BillDetails.First(x=> x.BillDetailId== model.BillId);
            if (ModelState.IsValid)
            {
                data.Advance = model.Advance;
                data.HealthCard= model.HealthCard;
                data.BillId = model.BillId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bill = db.Bills.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.BillDetails.FirstOrDefault(x=> x.BillDetailId== id);
            if (existing != null)
            {
                db.BillDetails.Remove(existing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }else
            {
                return HttpNotFound();
            }
        }
    }
}