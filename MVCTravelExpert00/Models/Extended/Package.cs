using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models.Extended
{
    [MetadataType(typeof(PackageMetaData))]
    public partial class Package
    {
        public Nullable<decimal> PkgAgencyCommission { get; set; }
    }
    public class PackageMetaData
    { 
        [Display(Name ="Package ID")]
        public int PackageId { get; set; }

        [Display(Name ="PackageName")]
        public string PkgName { get; set; }
        [Display(Name ="Package Start Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> PkgStartDate { get; set; }
        [Display(Name = "Package End Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> PkgEndDate { get; set; }
        [Display(Name ="Package Description")]
        public string PkgDesc { get; set; }
        [Display(Name ="Package Base Price")]
        public decimal PkgBasePrice { get; set; }
        [Display(Name ="Package Agency Commission")]
        public Nullable<decimal> PkgAgencyCommission { get; set; }

    }
}