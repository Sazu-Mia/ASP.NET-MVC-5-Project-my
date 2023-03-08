using Hospital_Managemnt_Project_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class BillInputModel
    {
        public int BillId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BillDate { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal SeatRent { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal OtherCharge { get; set; }
        [Required]
        public int PatientId { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    }
}