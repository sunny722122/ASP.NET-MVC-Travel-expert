using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models
{
    public class OrderHistoryModel
    {
        public Customer customer { get; set; }
        public Package package { get; set; }
        public Booking booking { get; set; }
        public BookingDetail bookingdetail { get; set; }
        public Region region { get; set; }
        public Class pdclass { get; set; }
        public TripType tripType { get; set; }
        public Fee fee { get; set; }

        //[Display(Name = "Total Package Price")]
        //public Nullable<decimal> TotalPkgPrice { get; set; }
        //[Display(Name = "Total Base Price")]
        //public Nullable<decimal> TotalBasePrice { get; set; }
        //[Display(Name = "Total Fee")]
        //public decimal TotalFee { get; set; }
        //{
        //    get
        //    {
        //        return package.Select(s => s.PkgBasePrice).Sum();
        //    }
        //}
    }
}