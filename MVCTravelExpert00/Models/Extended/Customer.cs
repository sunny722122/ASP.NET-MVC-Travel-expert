using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCTravelExpert00.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        public string ConfirmPassword { get; set; }
    }

    public class CustomerMetaData
    {
        [Display(Name = "Customer ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer ID required")]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string CustFirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string CustLastName { get; set; }

        [Display(Name ="Address")]
        [StringLength(75, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required")]
        public string CustAddress { get; set; }

        [Display(Name ="City")]
        [StringLength(50, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City required")]
        public string CustCity { get; set; }

        [Display(Name = "Province")]
        [StringLength(2, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Province required")]
        public string CustProv { get; set; }

        [Display(Name = "Postal Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal Code required")]
        //[RegularExpression(@"^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$",ErrorMessage ="Invalid Postal Code")]
        //[RegularExpression(@"^[ABCEGHJKLMNPRSTVXY] [0-9] [ABCEGHJKLMNPRSTVWXYZ] ?[0 - 9][ABCEGHJKLMNPRSTVWXYZ][0 - 9]$", ErrorMessage = "Invalid Postal Code")]
        //[RegularExpression(@"/^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/", ErrorMessage = "Invalid Postal Code")]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)", ErrorMessage = "That postal code is not a valid US or Canadian postal code.")]
        public string CustPostal { get; set; }

        [Display(Name = "Country")]
        [StringLength(25, MinimumLength = 1)]
        public string CustCountry { get; set; }

        [Phone]
        [Display(Name ="Home Phone Number")]
        [StringLength(20, MinimumLength = 1)]
        public string CustHomePhone { get; set; }

        [Phone]
        [Display(Name ="Business Phone Number")]
        [StringLength(20, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Business Phone required")]
        public string CustBusPhone { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 5)]
        public string CustEmail { get; set; }

        [Display(Name = "Agent ID")]
        public Nullable<int> AgentId { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minium 6 characters required")]
        public string CustPwd { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("CustPwd", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
    }
}