using System.Collections.Generic;

namespace Heuristics.TechEval.Core.Models
{
    public class MemberListViewModel
    {
        public List<MemberWithCategory> Members { get; set; }
        public List<Category> Categories { get; set; }
    }
}
