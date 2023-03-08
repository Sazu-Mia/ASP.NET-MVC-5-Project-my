using CrystalDecisions.CrystalReports.Engine;
using Hospital_Managemnt_Project_01.Models;
using Hospital_Managemnt_Project_01.ViewModels;
using Hospital_Managemnt_Project_01.ViewModels.identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Managemnt_Project_01.Controllers
{
    public class CrystalReportController : Controller
    {
        HospitalDbContext db = new HospitalDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }
        public ActionResult DoctorsReport()
        {
            var data = db.Patients.Include(x => x.Doctor).Select(x => new DoctorReportView
            {
                PatientName = x.PatientName,
                Phone= x.Phone,
                Gender = x.Gender,
                Address= x.Address,
                DoctorId= x.DoctorId,
                DoctorName = x.Doctor.DoctorName
            }).ToList();
            return View(data);
        }
        public ActionResult BillsReport()
        {
            var data = db.Bills.Include(x => x.Patient).Select(x => new BillReportModel
            {
                BillId = x.BillId,
                BillDate= x.BillDate,
                SeatRent = x.SeatRent,
                OtherCharge= x.OtherCharge,
                PatientId = x.PatientId,
                PatientName = x.Patient.PatientName
            }).ToList();
            return View(data);
        }
        public ActionResult DoctorPDF()
        {
            var data = db.Doctors.AsEnumerable().Select(x => new DoctorViewModel
            {
                DoctorId = x.DoctorId,
                DoctorName = x.DoctorName,
                BirthDate = x.BirthDate,
                DoctorFee = x.DoctorFee,
                IsAvaliable= x.IsAvaliable,
                Picture = x.Picture,
                Image = System.IO.File.ReadAllBytes(System.IO.Path.Combine(Server.MapPath("~/Uploads"), x.Picture))
            })
                .ToList();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport1.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Doctors.pdf");

        }
        public ActionResult PatientPDF()
        {
            var data = db.Patients.Include(x => x.Doctor).Select(x => new DoctorReportView
            {
                PatientName = x.PatientName,
                Phone = x.Phone,
                Gender = x.Gender,
                Address = x.Address,
                DoctorId = x.DoctorId,
                DoctorName = x.Doctor.DoctorName
            }).ToList();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Patients.pdf");
        }
        public ActionResult PatientExcel()
        {
            var data = db.Patients.Include(x => x.Doctor).Select(x => new DoctorReportView
            {
                PatientName = x.PatientName,
                Phone = x.Phone,
                Gender = x.Gender,
                Address = x.Address,
                DoctorId = x.DoctorId,
                DoctorName = x.Doctor.DoctorName
            }).ToList();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/vnd.ms-excel", "Patients.xls");
        }
        public ActionResult BillPDF()
        {
            var data = db.Bills.Include(x => x.Patient).Select(x => new BillReportModel
            {
                BillId = x.BillId,
                BillDate = x.BillDate,
                SeatRent = x.SeatRent,
                OtherCharge = x.OtherCharge,
                PatientId = x.PatientId,
                PatientName = x.Patient.PatientName
            }).ToList();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport3.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Bills.pdf");
        }
        public ActionResult BillExcel()
        {
            var data = db.Bills.Include(x => x.Patient).Select(x => new BillReportModel
            {
                BillId = x.BillId,
                BillDate = x.BillDate,
                SeatRent = x.SeatRent,
                OtherCharge = x.OtherCharge,
                PatientId = x.PatientId,
                PatientName = x.Patient.PatientName
            }).ToList();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport3.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/vnd.ms-excel", "Bills.xls");
        }
    }
}