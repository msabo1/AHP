using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.DAL
{
    class Alternative_Comparison
    {
        [Key, Column(Order = 0)]
        public Criteria CriteriaId { get; set; }
        [Key, Column(Order = 1)]
        public Alternative AlternativeId1 { get; set; }
        [Key, Column(Order = 2)]
        public Alternative AlternativeId2 { get; set; }
        
        public double Ratio { get; set; }
    }
}
