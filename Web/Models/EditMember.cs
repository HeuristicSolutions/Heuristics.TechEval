using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Web.Models
{
	/// <summary>
	/// DTO representing the basic details of a Member for creating or editing.
	/// </summary>
	public class EditMember
	{
		public int Id { get; set; }
		[Required]
		[RegularExpression(@"^[A-Z][a-zA-Z0-9\s]*$", ErrorMessage = "Only alphanumeric and whitespace characters allowed")]
		public string Name { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
	}
}