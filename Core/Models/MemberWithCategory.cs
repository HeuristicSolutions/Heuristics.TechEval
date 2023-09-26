namespace Heuristics.TechEval.Core.Models
{
    public class MemberWithCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
    }
}
