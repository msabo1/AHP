﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Model
{
    public class Choice
    {
        public System.Guid ChoiceID { get; set; }
        public string ChoiceName { get; set; }
        public System.Guid UserID { get; set; }
        public virtual ICollection<Criterion> Criteria { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
