using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models
{
    public class ProdBooking
    {
        public string ProdImage { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public decimal PkgBasePrice { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string RegionName { get; set; }
        public string TripName { get; set; }
        public string FeeName { get; set; }
        public string FeeDescription { get; set; }
        public decimal FeeAmount { get; set; }
    }
}