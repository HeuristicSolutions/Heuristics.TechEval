using Heuristics.TechEval.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics.TechEval.Core.Repositories
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
        Member AddMember(Member member);
        void UpdateMember(Member member);
        bool IsExistingEmail(string email);
    }
    public class MemberRepository : IMemberRepository
    {
        private readonly DataContext _context;

        public MemberRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetMembers()
        {
            var members = _context.Members.Include("Category");
            return members;
        }

        public Member GetMember(int id)
        {
            return _context.Members.Where(mem => mem.Id == id).FirstOrDefault();
        }

        public Member AddMember(Member member)
        {
            var newMember = _context.Members.Add(member);
            _context.SaveChanges();
            return newMember;
        }

        public void UpdateMember(Member member)
        {
            //var memberToUpdate = _context.Members.Where(m => m.Id == member.Id).FirstOrDefault();

            //memberToUpdate.Name = member.Name;
            //memberToUpdate.CategoryId = member.CategoryId;
            //memberToUpdate.LastUpdated = DateTime.Now;
            _context.SaveChanges();
        }

        public bool IsExistingEmail(string email)
        {
            var existingEmail = _context.Members.Where(m => m.Email == email).FirstOrDefault();
            return existingEmail != null;
        }
    }
}
