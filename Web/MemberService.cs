using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace Heuristics.TechEval.Web
{
    public class MemberService
    {
        private readonly DataContext _context;

        public MemberService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Member> FetchMembers(int? Id = null)
        {
            var query = _context.Members.Select(x => x);

            if (Id != null)
            {
                query = query.Where(x => x.Id == Id.Value);
            }
            //TODO: Automapper
            return query.Select(x => new Member()
            {
                Id = x.Id,
                CategoryId = x.Category.Id,
                CategoryName = x.Category.Name,
                Email = x.Email,
                Name = x.Name
            });
        }
        public Member FetchMember(int Id)
        {
            return FetchMembers(Id).FirstOrDefault();
        }

        public IEnumerable<Category> FetchCategories()
        {
            return _context.Categories.Select(x => new Category()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public async Task<Member> SaveMember(Member member)
        {
            var entity = new Core.Models.Member()
            {
                Id = member.Id,
                CategoryId = member.CategoryId,
                Email = member.Email,
                Name = member.Name,
                LastUpdated = DateTime.Now
            };

            _context.Members.AddOrUpdate(entity);
            await _context.SaveChangesAsync();

            return FetchMember(entity.Id);
        }
        public bool IsMemberDataValid(Member data, out List<string> errors, int? Id = null)
        {
            errors = new List<string>();

            data.Name = data.Name.Trim();
            data.Email = data.Email.Trim();

            //TODO: probably not needed since we're checking in the view model
            if (string.IsNullOrEmpty(data.Name))
            {
                errors.Add($"{nameof(data.Name)} is required");
            }

            if (data.CategoryId < 0)
            {
                errors.Add($"{nameof(data.CategoryId)} is required");
            }

            if (string.IsNullOrEmpty(data.Email))
            {
                errors.Add($"{nameof(data.Email)} is required");
            }
            else if (_context.Members.Any(x => x.Id != Id && x.Email.ToLower() == data.Email.ToLower()))
            {
                errors.Add($"{nameof(data.Email)} is currently in use");
            }

            return !errors.Any();
        }
    }
}
