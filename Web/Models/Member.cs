using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Web.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
