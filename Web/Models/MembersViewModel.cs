using System.Collections.Generic;

namespace Heuristics.TechEval.Web.Models
{
    public class MembersViewModel
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }

}
