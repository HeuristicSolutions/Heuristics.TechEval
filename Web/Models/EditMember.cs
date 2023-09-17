using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Heuristics.TechEval.Web.Models {

	/// <summary>
	/// DTO representing the data needed for editing a member
	/// </summary>
	public class EditMember {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Category is a required field.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}