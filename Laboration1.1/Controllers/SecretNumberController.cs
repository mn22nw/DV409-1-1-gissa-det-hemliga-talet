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

        public ActionResult Index()
        {   
            Session["StartGame"] = "StartGame";
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int number)
        {
            
            if (Session["StartGame"] == null)
            {
                return View("sessionExpired");
            }

            if (ModelState.IsValid)
            {
                SecretNumber model;
                if ((SecretNumber)Session["SecretNumber"] == null)
                {
                    model = new SecretNumber();
                }
                else
                {
                    model = (SecretNumber)Session["SecretNumber"];
                }

                try
                {
                    model.MakeGuess(number);
                    
                    if (model.LastGuessedNumber.Outcome == SecretNumber.Outcome.Right || model.LastGuessedNumber.Outcome == SecretNumber.Outcome.NoMoreGuesses)
                    {
                        Session.Abandon();
                    } 
                }
                catch (ArgumentOutOfRangeException e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);
                    return View("Index_post", model);
                }
                Session["SecretNumber"] = model;

                return View("Index_post", model);
            }
            return View();
        }
    }
}