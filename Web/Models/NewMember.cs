namespace Heuristics.TechEval.Web.Models
{

	/// <summary>
	/// DTO representing the creation of a new Member
	/// </summary>
	public class NewMember {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
	}
}