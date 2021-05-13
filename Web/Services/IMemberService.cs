using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics.TechEval.Web.Services
{
    public interface IMemberService
    {
        List<Member> GetMembers();
        Member GetMember(int Id);
        Member GetMemberByEmail(string email);
        Member AddMember(MemberModel member);
        Member UpdateMember(MemberModel member);
    }
}
