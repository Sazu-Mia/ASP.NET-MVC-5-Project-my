using Hospital_Managemnt_Project_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class BillReportModel
    {
        public int BillId { get; set; }
       
        public DateTime BillDate { get; set; }
       
        public decimal SeatRent { get; set; }
       
        public decimal OtherCharge { get; set; }
       
        public int PatientId { get; set; }
        public string PatientName { get; set; }
    }
}