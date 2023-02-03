using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_Month_Exam_Works_01.Models
{
    [MetadataType(typeof(PatientMetaData))]
    public partial class Patient
    {
    }
    public partial class PatientMetaData
    {
        public int PatientId { get; set; }
        [Required, StringLength(50)]
        public string PatientName { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required, StringLength(30)]
        public string BloodGroup { get; set; }
        [Required, StringLength(100)]
        public string Address { get; set; }
        [Required, StringLength(40)]
        public string Gender { get; set; }
        [Required]
        public int DoctorId { get; set; }

    }
}