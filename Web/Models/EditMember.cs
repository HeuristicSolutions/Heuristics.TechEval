using System.ComponentModel.DataAnnotations;

namespace Heuristics.TechEval.Web.Models
{
	/// <summary>
	/// DTO representing the basic details of a Member for creating or editing.
	/// </summary>
	public class EditMember
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}