using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_Month_Exam_Works_01.Models
{
    [MetadataType(typeof(DoctorMetaData))]
    public partial class Doctor
    {
    }
    public partial class DoctorMetaData
    {

        public int DoctorId { get; set; }
        [Required, StringLength(50)]
        public string DoctorName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        public string Qualification { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsAvaliable { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }

    }
}