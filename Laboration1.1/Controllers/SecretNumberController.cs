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
            Session.Abandon();
            
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int number)
        {
            Session["StartGame"] = "StartGame";
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
                    if (model.LastGuessedNumber.Outcome == Laboration1._1.Models.SecretNumber.Outcome.NoMoreGuesses)
                    {
                        return View("GameOver", model);
                    }
                        
                }
                catch (ArgumentOutOfRangeException e)
                {
                    ModelState.AddModelError("Number", e.Message);
                    return View();
                }
                Session["SecretNumber"] = model;

                return View("Index_post", model);
            }
            return View();
        }
    }
}