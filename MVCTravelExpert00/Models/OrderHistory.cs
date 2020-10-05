using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models
{
    public class OrderHistory
    {
        //public int CustomerId { get; set; }
        //public string CustFirstName { get; set; }
        //public string CustLastName { get; set; }
        
        //public IEnumerator<Customer> GetEnumerator()
        //{
        //    return customer.get
        //}
        [Display(Name = "Customer ID")]
        public int customerID { get; set; }
        
        [Display(Name = "Customer Name")]
        public string customername { get; set; }

       


        [Display(Name = "Package Name")]
        public string pkgname { get; set; }
        [Display(Name = "Package Start Date")]
        public Nullable< DateTime> pkstDate { get; set; }
        [Display(Name = "Package End Date")]
        public Nullable<DateTime> pkendDate { get; set; }
        [Display(Name = "Package Description")]
        public string pkgdesc { get; set; }
        [Display(Name = "Package base price")]
        public decimal pkgbaseprice { get; set; }
        
        [Display(Name = "TtineraryNo")]
        public Nullable<double> ItineraryNo { get; set; }
        [Display(Name = "TripStart")]
        public Nullable<System.DateTime> TripStart { get; set; }
        [Display(Name = "TripEnd")]
        public Nullable<System.DateTime> TripEnd { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Destination")]
        public string Destination { get; set; }
        [Display(Name = "Base Price")]
        public Nullable<decimal> BasePrice { get; set; }

        
        [Display(Name = "Booking Date")]
        public Nullable<DateTime> bkDate { get; set; }
        [Display(Name = "Booking Number")]
        public string bkNumber { get; set; }
        [Display(Name = "Traveler Count")]
        public int travelercount { get; set; }
        
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }
        
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "Clas Description")]
        public string ClassDesc { get; set; }

        
        [Display(Name = "FeeName")]
        public string FeeName { get; set; }
        [Display(Name = "Fee Amount")]
        public decimal FeeAmt { get; set; }
        [Display(Name = "Fee Description")]
        public string FeeDesc { get; set; }
        

        
        [Display(Name = "Product Name")]
        public string ProdName { get; set; }
        
        [Display(Name = "Suplier Name")]
        public string SupName { get; set; }
        [Display(Name = "Total Package Price")]
        public Nullable<decimal> TotalPkgPrice { get; set; }
        //{
        //    get { return packages.Select(s => s.PkgBasePrice).Sum(); }
        //}
        [Display(Name = "Total Base Price")]
        public Nullable<decimal> TotalBasePrice { get; set; }
        //{
        //    get { return bookingdetails.Select(s => s.BasePrice).Sum(); }
        //}

        [Display(Name = "Total Fee")]
        public decimal TotalFee { get; set; }
        //{
        //    get { return fees.Select(s => s.FeeAmt).Sum(); }
        //}
    }
}