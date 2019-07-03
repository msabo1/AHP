using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP.WebAPI.Models
{
    public class ChoiceMvcModel
    {
        public Guid ChoiceID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}