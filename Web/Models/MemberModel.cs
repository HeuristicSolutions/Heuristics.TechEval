using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.Models
{
	/// <summary>
	/// DTO representing a Member
	/// </summary>
	public class MemberModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}