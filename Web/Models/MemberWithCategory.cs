namespace Heuristics.TechEval.Web.Models
{
    /// <summary>
    /// DTO representing the creation of a new Member
    /// </summary>
    public class MemberWithCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CategoryName { get; set; }
    }
}