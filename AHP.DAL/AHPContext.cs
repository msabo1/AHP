using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AHP.DAL
{ 

    
    class AHPContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Criteria> Criterias { get; set; }
        public virtual DbSet<Criteria_Comparison> Criteria_Comparisons { get; set; }
        public virtual DbSet<Alternative> Alternatives { get; set; }
        public virtual DbSet<Alternative_Comparison> Alternative_Comparisons { get; set; }
    }
}
