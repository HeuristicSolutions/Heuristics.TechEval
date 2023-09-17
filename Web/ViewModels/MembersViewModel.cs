using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.ViewModels
{
    public class MembersViewModel
    {
        public NewMember NewMember { get; set; }
        public EditMember EditMember { get; set; }
        public List<Member> Members { get; set; }
        public List<Category> Categories { get; set; }
    }
}