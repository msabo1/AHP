using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.DAL
{
    class Alternative
    {
        [Key]
        public Guid AlternativeId { get; set; }

        public string Name { get; set; }
        
        public double Score { get; set; }

        [Required]
        public Choice Choice { get; set; }

        public ICollection<Alternative_Comparison> Alternative_Comparisons { get; set; }
    }
}
