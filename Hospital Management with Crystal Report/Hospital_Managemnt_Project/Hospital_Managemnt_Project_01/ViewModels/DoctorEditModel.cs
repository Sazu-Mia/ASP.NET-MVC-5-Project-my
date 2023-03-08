using Hospital_Managemnt_Project_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class DoctorEditModel
    {
        public int DoctorId { get; set; }
        [Required, StringLength(100)]
        public string DoctorName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal DoctorFee { get; set; }
        [Required]
        public bool IsAvaliable { get; set; }
        public string Picture { get; set; }
        public virtual List<Patient> Patients { get; set; } = new List<Patient>();
    }
}