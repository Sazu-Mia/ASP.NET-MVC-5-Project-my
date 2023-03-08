using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Managemnt_Project_01.ViewModels.identity
{
    public class LoginViewModel
    {
        [Required, StringLength(80)]
        public string UserName { get; set; }
        [Required, StringLength(80) ,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}