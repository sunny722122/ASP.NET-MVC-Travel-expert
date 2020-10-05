using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTravelExpert00.Models
{
    public class BookPkgModel
    {
        
        public Package Ppackage { get; set; }
        public Booking Bbooking { get; set; }
        public BookingDetail Bbookingdetail { get; set; }
        //public Region Rregion { get; set; }
        //public Class Pdclass { get; set; }
        //public TripType TtripType { get; set; }
        //public Fee Ffee { get; set; }
        //public SelectList Regions { get; private set; }
        //public SelectList PdClasses { get; private set; }
        //public SelectList Tiptypes { get; private set; }
        //public SelectList Fees { get; private set; }

        //public BookPkgModel(Package package,
        //    Booking booking,
        //    BookingDetail bookingDetail,
        //    Region region,
        //    Class pdclass,
        //    TripType tripType,
        //    Fee fee)
        //{
        //    Ppackage = package;
        //    Bbooking = booking;
        //    Bbookingdetail = bookingDetail;
        //    Rregion = region;
        //    Pdclass = pdclass;
        //    TtripType = tripType;
        //    Ffee = fee;
        //    Regions = new SelectList(Regions, "RegionName", "Name", bookingDetail.RegionId);

        //}
        public string imgsrc { get; set; }
        public int PkgId { get; set; }
        [Display(Name ="Package Name")]
        public string  PkgName { get; set; }
        [Display(Name = "StartDate")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "EndDate")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name ="Destination")]
        public string  Destination { get; set; }
        public string Description { get; set; }
        [Display(Name ="Package Price")]
        public decimal PkgBasePrice { get; set; }
        [Display(Name ="Traveler Count")]
        [Range(1,int.MaxValue,ErrorMessage ="Please enter valid integer Number")]
        public int TravelerCount { get; set; }
        [Display(Name ="Trip type")]
        public string Triptype { get; set; }
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "Fee Name")]
        public string  FeeName { get; set; }
        
        public int BookingId { get; set; }
    }
}