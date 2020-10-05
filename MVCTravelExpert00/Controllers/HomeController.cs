using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVCTravelExpert00.Models;
using System.Net;
using System.Data.Entity;
using System.Net.Mail;

namespace MVCTravelExpert00.Controllers
{
    public class HomeController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities();
        public ActionResult Index()
        {
            var packages = db.Packages;
            List<PkgHomeDisplayModel> pkgdis = new List<PkgHomeDisplayModel>();
            foreach(Package pk in db.Packages)
            {
                PkgHomeDisplayModel pd = new PkgHomeDisplayModel();
                pd.pkgName = pk.PkgName;
                pd.imgsrc= "~/Content/Img/product/pkg" + pk.PackageId.ToString() + ".jpeg";
                pkgdis.Add(pd);
            }
            return View(pkgdis);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contactus(string receiver, string subject, string message)
        {
            try
            {
                bool Status = false;
                string msg = "";

                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("sunnywang722@outlook.com", "Sunny");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "1234Wang";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                        msg="Email sent successfully! Please check your inbox.";
                        Status = true;
                    }
                    ViewBag.Message = msg;
                    ViewBag.Status = Status;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}