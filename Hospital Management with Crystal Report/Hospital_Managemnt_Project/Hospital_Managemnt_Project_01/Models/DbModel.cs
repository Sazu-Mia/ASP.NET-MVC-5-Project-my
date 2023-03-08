using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.Models
{
    public class Doctor
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
        [Required, StringLength(50)]
        public string Picture { get; set; }
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    }
    public class Patient
    {
        public int PatientId { get; set; }
        [Required, StringLength(80)]
        public string PatientName { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required, StringLength(100)]
        public string Gender { get; set; }
        [Required, StringLength(100)]
        public string Address { get; set; }
        [Required, ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    }

    public class Bill
    {
        public int BillId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BillDate { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal SeatRent { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal OtherCharge { get; set; }
        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    }
    public class BillDetail
    {
        public int BillDetailId { get; set; }
        public decimal Advance { get; set; }
        public bool HealthCard { get; set; }
        [Required, ForeignKey("Bill")]
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }
    }
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Bill> Bills { get; set; }

    }
}