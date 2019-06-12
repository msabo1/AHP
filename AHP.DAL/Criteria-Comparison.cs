using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.DAL
{
    class Criteria_Comparison
    {
        [Key, Column(Order = 0)]
        public Criteria CriteriaId1 { get; set; }
        [Key, Column(Order = 1)]
        public Criteria CriteriaId2 { get; set; }

        public double ratio { get; set; }
    }
}
