using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models
{
    public class CustomerLoginModel
    {
        [Display(Name = "Customer ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer ID required")]
        public int CustomerId { get; set; }
        [Display(Name ="Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string CustPwd { get; set; }
    }
}