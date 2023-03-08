using Hospital_Managemnt_Project_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
       
        public string DoctorName { get; set; }
       
        public DateTime BirthDate { get; set; }

        public decimal DoctorFee { get; set; }
      
        public bool IsAvaliable { get; set; }
        
        public string Picture { get; set; }
        public byte[] Image { get; set; }
    }
}