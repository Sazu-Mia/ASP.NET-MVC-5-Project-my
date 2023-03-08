using Hospital_Managemnt_Project_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class DoctorReportView
    {
        public int PatientId { get; set; }
       
        public string PatientName { get; set; }
       
        public int Phone { get; set; }
       
        public string Gender { get; set; }
        
        public string Address { get; set; }
   
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}