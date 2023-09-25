using System;
using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Core.Models
{

    public class Member {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
} 


