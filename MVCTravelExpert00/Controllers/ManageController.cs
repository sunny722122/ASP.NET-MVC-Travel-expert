using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTravelExpert00.Models;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Security;
using System.Web.Helpers;
using AutoMapper;

namespace MVCTravelExpert00.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            if (Session["CustomerId"] != null)
            {


                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult ManageProfile()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            using (TravelExpertsEntities dc = new TravelExpertsEntities())
            {
                Customer cust = dc.Customers.Find(Convert.ToInt32(Session["CustomerId"]));
                ManageProfileModel custprofile = new ManageProfileModel();
                custprofile.CustomerId = Convert.ToInt32(Session["CustomerId"]);
                custprofile.CustFirstName = cust.CustFirstName;
                custprofile.CustLastName = cust.CustLastName;
                custprofile.CustAddress = cust.CustAddress;
                custprofile.CustCity = cust.CustCity;
                custprofile.CustProv = cust.CustProv;
                custprofile.CustPostal = cust.CustPostal;
                custprofile.CustCountry = cust.CustCountry;
                custprofile.CustHomePhone = cust.CustHomePhone;
                custprofile.CustBusPhone = cust.CustBusPhone;
                custprofile.CustEmail = cust.CustEmail;
                custprofile.AgentId = cust.AgentId;
                return View(custprofile);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProfile(ManageProfileModel cust)
        {
            bool Status = false;
            string message = "";
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //
            //Model Validation
            if (ModelState.IsValid)
            {
                try
                {
                    #region Save data to Database
                    using (TravelExpertsEntities dc = new TravelExpertsEntities())
                    {
                        Customer cust1 = dc.Customers.Find(Convert.ToInt32(Session["CustomerId"]));
                        cust1.CustFirstName = cust.CustFirstName;
                        cust1.CustLastName = cust.CustLastName;
                        cust1.CustAddress = cust.CustAddress;
                        cust1.CustCity = cust.CustCity;
                        cust1.CustProv = cust.CustProv;
                        cust1.CustPostal = cust.CustPostal;
                        cust1.CustCountry = cust.CustCountry;
                        cust1.CustHomePhone = cust.CustHomePhone;
                        cust1.CustBusPhone = cust.CustBusPhone;
                        cust1.CustEmail = cust.CustEmail;
                        cust1.AgentId = cust.AgentId;
                        cust1.ConfirmPassword = cust1.CustPwd;
                        dc.Entry(cust1).State = EntityState.Modified;
                        dc.SaveChanges();


                        message = "Customer Information successfully updated.Please remember your customer ID: " + cust1.CustomerId;
                        Status = true;
                    }
                    #endregion
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string msg = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(msg, raise);
                        }
                    }
                    throw raise;
                }
            }
            else
            {
                message = "Invalid request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(cust);
        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            bool Status = false;
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                using (TravelExpertsEntities dc = new TravelExpertsEntities())
                {
                    var user = dc.Customers.Find(Convert.ToInt32(Session["CustomerId"]));
                    if (user != null)
                    {
                        user.CustPwd = Crypto.Hash(model.NewPassword);

                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                        Status = true;
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(model);
        }

        public ActionResult OrderHistory()
        {
            var message = "";
            bool Status = false;
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            using (TravelExpertsEntities dc = new TravelExpertsEntities())
            {
                Customer user = dc.Customers.Find(Convert.ToInt32(Session["CustomerId"]));
                if (user != null)
                {
                    List<Package> packages = dc.Packages.ToList();
                    List<Customer> customers = dc.Customers.ToList();
                    List<Booking> bookings = dc.Bookings.ToList();
                    List<BookingDetail> bookingDetails = dc.BookingDetails.ToList();
                    List<Region> regions = dc.Regions.ToList();
                    List<Class> classes = dc.Classes.ToList();
                    List<Fee> fees = dc.Fees.ToList();
                    List<TripType> tripTypes = dc.TripTypes.ToList();

                    List<Products_Suppliers> products_Suppliers = dc.Products_Suppliers.ToList();
                    List<Product> products = dc.Products.ToList();
                    List<Supplier> suppliers = dc.Suppliers.ToList();

                    OrderHistory orderhis = new OrderHistory();
                    OrderHistoryViewModel orderview = new OrderHistoryViewModel();
                    orderview.customer = customers.Find(a => a.CustomerId == user.CustomerId);
                    orderview.bookings = bookings.FindAll(a => a.CustomerId == user.CustomerId);
                    orderhis.customerID = user.CustomerId;
                    orderhis.customername = orderview.customer.CustFirstName + " " + orderview.customer.CustLastName;

                    var pkg = from c in customers
                              join b in bookings on c.CustomerId equals b.CustomerId into table1
                              from b in table1.DefaultIfEmpty(new Booking())
                              join p in packages on b.PackageId equals p.PackageId into table2
                              from p in table2.DefaultIfEmpty(new Package ())
                              join bd in bookingDetails on b.BookingId equals bd.BookingId into table3
                              from bd in table3.DefaultIfEmpty(new BookingDetail())
                              join r in regions on bd.RegionId equals r.RegionId into table4
                              from r in table4.DefaultIfEmpty(new Region())
                              join cl in classes on bd.ClassId equals cl.ClassId into table5
                              from cl in table5.DefaultIfEmpty(new Class ())
                              join f in fees on bd.FeeId equals f.FeeId into table6
                              from f in table6.DefaultIfEmpty(new Fee ())
                              join t in tripTypes on b.TripTypeId equals t.TripTypeId into table7
                              from t in table7.DefaultIfEmpty(new TripType ())
                              where (c.CustomerId == user.CustomerId)
                              select new OrderHistoryModel
                              {
                                  customer = (c != null) ? c : null,
                                  package = (p.PackageId != 0) ? p:null,
                                  booking=(b.BookingId!=0)?b:null,
                                  bookingdetail=(bd.BookingId!=0)?bd:null,
                                  region=(r.RegionId!="")?r:null,
                                  pdclass=(cl.ClassId!="")?cl:null,
                                  fee=(f.FeeId!="")?f:null,
                                  tripType=(t.TripTypeId!="")?t:null
                              };
                    

                    if (orderview.bookings != null)
                    {
                        //foreach (Booking b in orderhis.bookings)
                        //{
                        //    orderhis.bookingdetails
                        //        =( bookingDetails.FindAll(a => a.BookingId ==b.BookingId ));
                        //}
                        //try
                        {
                            var pkgs = from b in orderview.bookings
                                       join p in packages on b.PackageId equals p.PackageId into table1
                                       from p in table1.DefaultIfEmpty()
                                       select new Package
                                       {
                                           
                                           PackageId =(p!=null)? p.PackageId:default(int),
                                           PkgName = (p!=null)?p.PkgName:null,
                                           PkgStartDate = (p != null) ? p.PkgStartDate:null,
                                           PkgEndDate = (p != null) ? p.PkgEndDate : null,
                                           PkgDesc = (p != null) ? p.PkgDesc : null,
                                           PkgBasePrice = (p != null) ? p.PkgBasePrice:default(decimal),
                                           PkgAgencyCommission = (p != null) ? p.PkgAgencyCommission:null
                                       };
                            if(pkgs!=null)
                            {
                                foreach(Package p in pkgs)
                                {
                                    if(p.PackageId!=0)
                                    {
                                        orderview.packages = new List<Package>();
                                        orderview.packages.Add(p);
                                        orderhis.pkgname = p.PkgName;
                                        orderhis.pkstDate = p.PkgStartDate;
                                        orderhis.pkendDate = p.PkgEndDate;
                                        orderhis.pkgdesc = p.PkgDesc;
                                        orderhis.pkgbaseprice = p.PkgBasePrice;
                                    }
                                }
                                if(orderview.packages!=null)
                                orderhis.TotalPkgPrice= orderview.packages.Select(s => s.PkgBasePrice).Sum();
                            }
                            //orderhis.packages = pkgs.ToList();
                        }
                        //catch(NullReferenceException ex)
                        //{
                        //    throw (ex);
                        //    //orderhis.packages = null;
                        //}

                        var bkdetails = from bd in orderview.bookings
                                        join b in bookingDetails on bd.BookingId equals b.BookingId into table1
                                        from b in table1.DefaultIfEmpty()
                                        select new BookingDetail
                                        {
                                            BookingDetailId = (b != null) ? b.BookingDetailId:default(int),
                                            ItineraryNo = (b != null) ? b.ItineraryNo:default(double),
                                            TripStart = (b != null) ? b.TripStart:null,
                                            TripEnd = (b != null) ? b.TripEnd:null,
                                            Description = (b != null) ? b.Description:null,
                                            Destination = (b != null) ? b.Destination : null,
                                            BasePrice = (b != null) ? b.BasePrice:default(decimal),
                                            AgencyCommission = (b != null) ? b.AgencyCommission:default(decimal),
                                            BookingId = (b != null) ? b.BookingId:default(int),
                                            RegionId = (b != null) ? b.RegionId : null,
                                            ClassId = (b != null) ? b.ClassId : null,
                                            FeeId = (b != null) ? b.FeeId : null,
                                            ProductSupplierId = (b != null) ? b.ProductSupplierId : null
                                        };
                        if (bkdetails != null)
                        {
                            foreach (BookingDetail p in bkdetails)
                            {
                                if (p.BookingDetailId != 0)
                                {
                                    orderview.bookingdetails = new List<BookingDetail>();
                                    orderview.bookingdetails.Add(p);
                                    orderhis.ItineraryNo = p.ItineraryNo;
                                    orderhis.TripStart = p.TripStart;
                                    orderhis.TripEnd = p.TripEnd;
                                    orderhis.Description = p.Description;
                                    orderhis.Destination = p.Destination;
                                    orderhis.BasePrice = p.BasePrice;
                                }
                            }
                            if(orderview.bookingdetails!=null)
                            orderhis.TotalBasePrice = orderview.bookingdetails.Select(s => s.BasePrice).Sum();
                        }
                        //orderhis.bookingdetails = bkdetails.ToList();
                        if (orderview.bookingdetails != null)
                        {
                            var reg = from bd in orderview.bookingdetails
                                      join r in regions on bd.RegionId equals r.RegionId into table1
                                      from r in table1.DefaultIfEmpty()
                                      select new Region
                                      {
                                          RegionId = (r != null) ? r.RegionId:null,
                                          RegionName = (r != null) ? r.RegionName : null
                                      };
                            if (reg != null)
                            {
                                foreach (Region p in reg)
                                {
                                    if (p.RegionId != null)
                                    {
                                        orderview.regions = new List<Region>();
                                        orderview.regions.Add(p);
                                        orderhis.RegionName = p.RegionName;
                                        
                                    }
                                }
                            }
                            //orderhis.regions = reg.ToList();

                            var cl = from bd in orderview.bookingdetails
                                     join c in classes on bd.ClassId equals c.ClassId into table1
                                     from c in table1.DefaultIfEmpty()
                                     select new Class
                                     {
                                         ClassId = (c != null) ? c.ClassId : null,
                                         ClassName = (c != null) ? c.ClassName : null,
                                         ClassDesc = (c != null) ? c.ClassDesc : null
                                     };
                            if (cl != null)
                            {
                                foreach (Class p in cl)
                                {
                                    if (p.ClassId != null)
                                    {
                                        orderview.classes = new List<Class>();
                                        orderview.classes.Add(p);
                                        orderhis.ClassName = p.ClassName;
                                        orderhis.ClassDesc = p.ClassDesc;
                                    }
                                }
                            }
                            //orderhis.classes = cl.ToList();
                            var fe = from bd in orderview.bookingdetails
                                     join f in fees on bd.FeeId equals f.FeeId into table1
                                     from f in table1.DefaultIfEmpty()
                                     select new Fee
                                     {
                                         FeeId = (f != null) ? f.FeeId : null,
                                         FeeName = (f != null) ? f.FeeName : null,
                                         FeeDesc = (f != null) ? f.FeeDesc : null,
                                         FeeAmt = (f != null) ? f.FeeAmt:default(decimal)
                                     };
                            if (fe != null)
                            {
                                foreach (Fee p in fe)
                                {
                                    if (p.FeeId != null)
                                    {
                                        orderview.fees = new List<Fee>();
                                        orderview.fees.Add(p);
                                        orderhis.FeeName = p.FeeName;
                                        orderhis.FeeDesc = p.FeeDesc;
                                        orderhis.FeeAmt = p.FeeAmt;
                                    }
                                }
                                if(orderview.fees!=null)
                                orderhis.TotalFee= orderview.fees.Select(s => s.FeeAmt).Sum();
                            }
                            //orderhis.fees = fe.ToList();

                            var ps = from bd in orderview.bookingdetails
                                     join p in products_Suppliers on bd.ProductSupplierId equals p.ProductSupplierId into table1
                                     from p in table1.DefaultIfEmpty()
                                     select new Products_Suppliers
                                     {
                                         ProductSupplierId = (p != null) ? p.ProductSupplierId:default(int),
                                         ProductId = (p != null) ? p.ProductId : default(int),
                                         SupplierId = (p != null) ? p.SupplierId : default(int)
                                     };
                            if (ps != null)
                            {
                                foreach (Products_Suppliers p in ps)
                                {
                                    if (p.ProductSupplierId != 0)
                                    {
                                        orderview.prod_sups = new List<Products_Suppliers>();
                                        orderview.prod_sups.Add(p);

                                    }
                                }
                            }
                            //orderhis.prod_sups = ps.ToList();
                            if(orderview.prod_sups!=null)
                            {
                                var p = from pss in orderview.prod_sups
                                        join pp in products on pss.ProductId equals pp.ProductId into table1
                                        from pp in table1.DefaultIfEmpty()
                                        select new Product
                                        {
                                            ProductId = (pp != null) ? pp.ProductId : default(int),
                                            ProdName = (pp != null) ? pp.ProdName:null
                                        };
                                if (p != null)
                                {
                                    foreach (Product pd in p)
                                    {
                                        if (pd.ProductId != 0)
                                        {
                                            orderview.products = new List<Product>();
                                            orderview.products.Add(pd);
                                            orderhis.ProdName = pd.ProdName;
                                        }
                                    }
                                }
                                //orderhis.products = p.ToList();
                                var s = from pss in orderview.prod_sups
                                        join ss in suppliers on pss.SupplierId equals ss.SupplierId into table1
                                        from ss in table1.DefaultIfEmpty()
                                        select new Supplier
                                        {
                                            SupplierId = (ss != null) ? ss.SupplierId : default(int),
                                            SupName = (ss != null) ? ss.SupName : null
                                        };
                                if (s != null)
                                {
                                    foreach (Supplier pd in s)
                                    {
                                        if (pd.SupplierId != 0)
                                        {
                                            orderview.suppliers = new List<Supplier>();
                                            orderview.suppliers.Add(pd);
                                            orderhis.SupName = pd.SupName;
                                        }
                                    }
                                }
                                //orderhis.suppliers = s.ToList();
                            }
                        }
                    }

                    message = "New password updated successfully";
                    Status = true;

                    // return View(orderhis);
                    //return PartialView(orderhis);
                    return View(pkg);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}