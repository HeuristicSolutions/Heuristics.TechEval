using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heuristics.TechEval.Core.Models
{
    public class Member {

		public int Id { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public DateTime LastUpdated { get; set; }
	}
}


