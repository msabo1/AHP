using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.DAL
{
    class Choice
    {
        [Key]
        public Guid ChoiceId { get; set; }

        [MaxLength(50)]
        public string ChoiceName { get; set; }

        public DateTime Date_creation { get; set; }

        public DateTime Date_update { get; set; }


        [Required]
        public User User { get; set; }

        public ICollection<Alternative> Alternatives { get; set; }

        public ICollection<Criteria> Criteria { get; set; }
    }
}
