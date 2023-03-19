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
		[Required(ErrorMessage = "Name Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Email Required")]
		public string Email { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public int CategoryId { get; set; }
    }
}