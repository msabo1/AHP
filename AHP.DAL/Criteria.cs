using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.DAL
{
    class Criteria
    {
        [Key]
        public Guid CriteriaId { get; set; }

        [MaxLength(50)]
        public string CriteriaName { get; set; }

        public double Score { get; set; }

        [Required]
        public Choice Choice { get; set; }

        public ICollection<Criteria_Comparison> Criteria_Comparisons { get; set; }

    }
}
