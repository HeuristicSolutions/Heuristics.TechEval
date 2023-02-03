using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Heuristics.TechEval.Core.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdated { get; set; }
        [ForeignKey("Category")]
        [DefaultValue(1)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}


