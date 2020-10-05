using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MVCTravelExpert00.Models;

namespace MVCTravelExpert00.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Customer cust)
        {
            bool Status = false;
            string message = "";
            //
            //Model Validation
            if (ModelState.IsValid)
            {

                #region//CustomerID is already exist
                var isExist = IsCustomerIDExit(cust.CustomerId);
                if (isExist)
                {
                    ModelState.AddModelError("CustIDExist", "Customer ID already exist");
                    return View(cust);
                }
                #endregion

                #region Password Hashing
                cust.CustPwd = Crypto.Hash(cust.CustPwd);
                cust.ConfirmPassword = Crypto.Hash(cust.ConfirmPassword);
                #endregion

                #region Save data to Database
                using (TravelExpertsEntities dc = new TravelExpertsEntities())
                {
                    dc.Customers.Add(cust);
                    dc.SaveChanges();


                    message = "Registration successfully done.Please log in with youur customer ID: " + cust.CustomerId;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(cust);
        }

        public bool IsCustomerIDExit(int custID)
        {
            using (TravelExpertsEntities dc = new TravelExpertsEntities())
            {
                var v = dc.Customers.Where(a => a.CustomerId == custID).FirstOrDefault();
                return v != null;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLoginModel login)
        {
            string message = "";
            
                using (TravelExpertsEntities dc = new TravelExpertsEntities())
            {
                var v = dc.Customers.Where(a => a.CustomerId == login.CustomerId).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.CustPwd), v.CustPwd) == 0)
                    {
                        
                        var ticket = new FormsAuthenticationTicket(login.CustomerId.ToString(), true, 60);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(60);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        Session["CustomerId"] = v.CustomerId;
                        Session["CustomerName"] = v.CustFirstName +" "+ v.CustLastName;
                        if (Session["PackageId"] != null)
                        {
                            return RedirectToAction("Book", "Bookings");
                        }
                        else
                            return RedirectToAction("Index", "Home");
                        
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }
}