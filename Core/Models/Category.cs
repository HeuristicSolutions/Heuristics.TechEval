using System.Collections.Generic;

namespace Heuristics.TechEval.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}


