using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Web.Models
{
	/// <summary>
	/// DTO representing the creation of a new Member
	/// </summary>
	public class NewMember {
        public int Id { get; set; }
		[Required]
		[RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Only alphanumeric characters allowed")]
        public string Name { get; set; }
        [Required]
		[EmailAddress]
        public string Email { get; set; }
	}
}