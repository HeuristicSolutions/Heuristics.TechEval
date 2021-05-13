using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[Required(ErrorMessage = "Member Name cannot be empty.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Member Email cannot be empty.")]
		public string Email { get; set; }
	}
}