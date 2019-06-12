using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AHP.DAL
{
    class User
    {
        [Key] public Guid UserId { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Choice> Choices { get; set; }
    }
}
