using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTravelExpert00.Models;

namespace MVCTravelExpert00.Controllers
{
    public class BookingsController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities();

        // GET: Bookings
        [HttpGet]
        public ActionResult Index()
        {
            var packages = db.Packages.Include(b => b.Bookings);
            ViewBag.TripType = new SelectList(db.TripTypes, "TTName", "TTName");
            ViewBag.ClassName = new SelectList(db.Classes, "ClassName", "ClassName");
            ViewBag.RegionName = new SelectList(db.Regions, "RegionName", "RegionName");
            //ViewBag.FeeName = new SelectList(db.Fees, "FeeName", "FeeName");
            List< BookPkgModel> bookPkg = new List<BookPkgModel> ();
            if (Session["PackageId"] != null)
            {
                Session["PackageId"] = null;
                Session["ClassName"] = null;
                Session["RegionName"] = null;
                Session["TripType"] = null;
                Session["TravelerCount"] = null;
            }
                foreach (Package pk in db.Packages)
            {
                BookPkgModel bm = new BookPkgModel();
                bm.StartDate = pk.PkgStartDate;
                bm.EndDate = pk.PkgEndDate;
                bm.PkgName = pk.PkgName;
                bm.Description = pk.PkgDesc;
                bm.PkgId = pk.PackageId;
                bm.PkgBasePrice = pk.PkgBasePrice;
                bm.TravelerCount = 1;
                bm.Triptype = db.TripTypes.First().TTName;
                bm.ClassName = db.Classes.First().ClassName;
                bm.RegionName = db.Regions.First().RegionName;
                bm.imgsrc = "~/Content/Img/product/pkg" + pk.PackageId.ToString() + ".jpeg";
                bookPkg.Add(bm);
            }
            

            return View(bookPkg);//bookings.ToList()packages.ToList()
        }

       
            public ActionResult Book(BookPkgModel bm)
        {
            bool Status = false;
            string message = "";
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            
            
            if (Session["CustomerId"] == null)
            {
                //save into book table session bookId
                if (Session["PackageId"] != null)
                {
                    bm.PkgId = Convert.ToInt32(Session["PackageId"]);
                    Package pk = db.Packages.Find(bm.PkgId);
                    bm.PkgName = pk.PkgName;
                    bm.Description = pk.PkgDesc;
                    bm.PkgBasePrice = pk.PkgBasePrice;
                    bm.StartDate = pk.PkgStartDate;
                    bm.EndDate = pk.PkgEndDate;
                    if (Session["ClassName"] != null)
                        bm.ClassName = Session["ClassName"].ToString();
                    if (Session["RegionName"] != null)
                        bm.RegionName = Session["RegionName"].ToString();
                    if (Session["TripType"] != null)
                        bm.Triptype = Session["TripType"].ToString();
                    bm.TravelerCount = Convert.ToInt32(Session["TravelerCount"]);
                    bm.imgsrc = "~/Content/Img/product/pkg" + bm.PkgId.ToString() + ".jpeg";
                    Status = true;
                    message = "Your order detail.";
                }
                else if (bm.PkgId != 0)
                {
                    Session["PackageId"] = bm.PkgId;

                    Session["TravelerCount"] = bm.TravelerCount;
                    Session["Class"] = bm.ClassName;
                    Session["Region"] = bm.RegionName;
                    Session["Triptype"] = bm.Triptype;
                    Package pk = db.Packages.Find(bm.PkgId);
                    bm.PkgName = pk.PkgName;
                    bm.Description = pk.PkgDesc;
                    bm.PkgBasePrice = pk.PkgBasePrice;
                    bm.StartDate = pk.PkgStartDate;
                    bm.EndDate = pk.PkgEndDate;
                    bm.imgsrc = "~/Content/Img/product/pkg" + bm.PkgId.ToString() + ".jpeg";
                }
                else
                {

                    message = "Data Error!";
                }

                return RedirectToAction("Registration", "Customer");
            }
            else
            {
                if (Session["PackageId"] != null)
                {
                    bm.PkgId = Convert.ToInt32(Session["PackageId"]);
                    Package pk = db.Packages.Find(bm.PkgId);
                    bm.PkgName = pk.PkgName;
                    bm.Description = pk.PkgDesc;
                    bm.PkgBasePrice = pk.PkgBasePrice;
                    bm.StartDate = pk.PkgStartDate;
                    bm.EndDate = pk.PkgEndDate;
                    if (Session["ClassName"] != null)
                        bm.ClassName = Session["ClassName"].ToString();
                    if (Session["RegionName"] != null)
                        bm.RegionName = Session["RegionName"].ToString();
                    if (Session["TripType"] != null)
                        bm.Triptype = Session["TripType"].ToString();
                    bm.TravelerCount = Convert.ToInt32(Session["TravelerCount"]);
                    bm.imgsrc = "~/Content/Img/product/pkg" + bm.PkgId.ToString() + ".jpeg";
                    Status = true;
                    message = "Your order detail.";
                }
                else if (bm.PkgId != 0)
                {
                    Session["PackageId"] = bm.PkgId;

                    Session["TravelerCount"] = bm.TravelerCount;
                    Session["Class"] = bm.ClassName;
                    Session["Region"] = bm.RegionName;
                    Session["Triptype"] = bm.Triptype;
                    Package pk = db.Packages.Find(bm.PkgId);
                    bm.PkgName = pk.PkgName;
                    bm.Description = pk.PkgDesc;
                    bm.PkgBasePrice = pk.PkgBasePrice;
                    bm.StartDate = pk.PkgStartDate;
                    bm.EndDate = pk.PkgEndDate;
                    bm.imgsrc = "~/Content/Img/product/pkg" + bm.PkgId.ToString() + ".jpeg";
                }
                else
                { 

                    message = "Data Error!";
                }
                return View(bm);
            }

           
            
        }

        public ActionResult Confirm(BookPkgModel bm)
        {
            bool Status = false;
            string message = "";
            //remove Session
            //display success message
            //save data to database
            if (Session["PackageId"] != null && Session["CustomerId"] != null)
            {
                bm.PkgId = Convert.ToInt32(Session["PackageId"]);
                Package pk = db.Packages.Find(bm.PkgId);
                bm.PkgName = pk.PkgName;
                bm.Description = pk.PkgDesc;
                bm.PkgBasePrice = pk.PkgBasePrice;
                bm.StartDate = pk.PkgStartDate;
                bm.EndDate = pk.PkgEndDate;
                if (Session["ClassName"] != null)
                    bm.ClassName = Session["ClassName"].ToString();
                if (Session["RegionName"] != null)
                    bm.RegionName = Session["RegionName"].ToString();
                if (Session["TripType"] != null)
                    bm.Triptype = Session["TripType"].ToString();
                bm.TravelerCount = Convert.ToInt32(Session["TravelerCount"]);
                Booking bk = new Booking();
                bk.BookingDate = DateTime.Now;
                bk.BookingNo = "S543";
                bk.CustomerId = Convert.ToInt32(Session["CustomerId"]);
                bk.TravelerCount = bm.TravelerCount;
                bk.TripTypeId = db.TripTypes.Where(t=>t.TTName==bm.Triptype).FirstOrDefault().TripTypeId;
                bk.PackageId = bm.PkgId;
                db.Bookings.Add(bk);
                db.SaveChanges();
                
            }
                return RedirectToAction("OrderHistory", "Manage");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
