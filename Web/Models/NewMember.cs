using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.Models {

	/// <summary>
	/// DTO representing the creation of a new Member
	/// </summary>
	public class NewMember {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
	}
}