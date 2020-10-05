using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravelExpert00.Models
{
    public class OrderHistoryViewModel
    {
        [Display(Name = "Customer Information")]

        public Customer customer { get; set; }
        [Display(Name = "Packages")]
        public List<Package> packages { get; set; }
        public IEnumerator<Package> GetEnumerator()
        {
            return packages.GetEnumerator();
        }
        [Display(Name = "Booking details")]
        public List<BookingDetail> bookingdetails { get; set; }

        [Display(Name = "Bookings")]

        public List<Booking> bookings { get; set; }
        [Display(Name = "Regions")]
        public List<Region> regions { get; set; }
        [Display(Name = "Class")]
        public List<Class> classes { get; set; }

        [Display(Name = "Fee")]
        public List<Fee> fees { get; set; }
        public List<Products_Suppliers> prod_sups { get; set; }
        [Display(Name = "Products")]
        public List<Product> products { get; set; }
        [Display(Name = "Suppliers")]
        public List<Supplier> suppliers { get; set; }
    }
}