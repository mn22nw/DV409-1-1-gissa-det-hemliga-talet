using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboration1._1.Models
{
    public class GuessViewModel
    {
        [Required(ErrorMessage = "Nummer måste anges.")]
        [Range(0, 100)]
        public int Number 
        {
            get; 
            private set;
        }
    }
}