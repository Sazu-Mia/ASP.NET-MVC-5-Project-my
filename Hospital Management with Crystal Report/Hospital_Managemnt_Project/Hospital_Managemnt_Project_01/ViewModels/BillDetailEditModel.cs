using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels
{
    public class BillDetailEditModel
    {
        public int BillDetailId { get; set; }
        public decimal Advance { get; set; }
        public bool HealthCard { get; set; }
        [Required]
        public int BillId { get; set; }
    }
}