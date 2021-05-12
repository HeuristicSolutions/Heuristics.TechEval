using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Heuristics.TechEval.Web.Services.Implementations
{
    public class MemberService
    {
        private readonly DataContext _context;

        public MemberService()
        {
            _context = new DataContext();
        }

        public List<Member> GetMembers() => _context.Members.ToList();

        public Member GetMember(int memberId) => _context.Members.FirstOrDefault(m => m.Id == memberId);

        public Member AddMember(MemberModel member)
        {
            var newMember = new Member
            {
                Name = member.Name,
                Email = member.Email
            };

            _context.Members.Add(newMember);
            _context.SaveChanges();

            return newMember;
        }

        public Member UpdateMember(MemberModel member)
        {
            var memberToUpdate = _context.Members.FirstOrDefault(m => m.Id == member.Id);

            _context.Entry(memberToUpdate).CurrentValues.SetValues(member);

            _context.SaveChanges();

            return memberToUpdate;
        }

    }
}