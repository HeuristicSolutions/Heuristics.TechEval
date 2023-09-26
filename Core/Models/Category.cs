using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Core.Models
{

    public class Category {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}


