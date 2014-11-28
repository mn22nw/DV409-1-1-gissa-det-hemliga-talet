using Laboration1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboration1._1.Controllers
{
    public class SecretNumberController : Controller
    {
        private SecretNumber SecretNumber
        {    
            get { return Session["SecretNumber"] as SecretNumber ?? (SecretNumber)(Session["SecretNumber"] = new SecretNumber()); }
        }

        public ActionResult Index()
        {
            if (Session["SecretNumber"] != null)
            {
                Session.Clear();
            }
            return View(SecretNumber);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int number)
        {

            if (Session["SecretNumber"] == null)
            {
                return View("sessionExpired");
            }

            if (ModelState.IsValid)
            {
                
                var model = (SecretNumber)Session["SecretNumber"];

                try
                {
                    model.MakeGuess(number);        
                }
                catch (ArgumentOutOfRangeException e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);
                    return View("Index_post", model);
                }
                
                return View("Index_post", model);
            }
            return View();
        }
    }
}