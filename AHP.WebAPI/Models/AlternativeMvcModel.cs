using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHP.WebAPI.Models
{
    public class AlternativeMvcModel
    {
        public Guid ChoiceID;
        [Display(Name = "Name")]
        public string AlternativeName;
        public Nullable<double> Score;
        [Display(Name = "Updated")]
        public DateTime DateUpdated;
    }
}