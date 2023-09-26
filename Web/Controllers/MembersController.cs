using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Heuristics.TechEval.Web.Controllers
{

    public class MembersController : Controller {

		private readonly DataContext _context;

        public MembersController()
        {
            _context = new DataContext();
        }

        public MembersController(DataContext context = null) {
            if (context == null)
            {
                _context = new DataContext();
            }
            else
            {
                _context = context;
            }
        }

		public ActionResult MemberList()
        {
            var categories = _context.Categories.ToList();
            var membersWithCategories = _context.Members.GroupJoin(
                _context.Categories,
                member => member.CategoryId,
                category => category.Id,
                (member, category) => new
                {
                    Member = member,
                    Category = category
                }
            ).SelectMany(result => result.Category, (member, category) => new MemberWithCategory
              {
                  Id = member.Member.Id,
                  Name = member.Member.Name,
                  Email = member.Member.Email,
                  Category = (member.Member.CategoryId == category.Id) ? category : null,

              }).ToList();
            var viewModel = new MemberListViewModel
            {
                Members = membersWithCategories,
                Categories = categories
            };
            return View(viewModel);
        }

		[HttpPost]
		public ActionResult New(NewMember data)
        {
			if (_context.Members.Where(m => m.Email == data.Email).ToList().Any())
			{
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.Conflict, $"Email {data.Email} already in use");
			}

			var newMember = new Member
			{
				Name = data.Name,
				Email = data.Email,
                CategoryId = data.CategoryId
			};
            newMember.LastUpdated = DateTime.Now;

            _context.Members.Add(newMember);
			_context.SaveChanges();

			return RedirectToAction("MemberList");
        }

		[HttpPost]
		public ActionResult Update(Member data)
        {
            if (_context.Members.Where(m => m.Email == data.Email && m.Id != data.Id).ToList().Count() > 0)
            {
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.Conflict, $"Email {data.Email} already in use");
			}

            var member = _context.Members.Find(data.Id);

            if (member == null)
            {
                return HttpNotFound($"Member {data.Id} not found");
            }
            member.LastUpdated = DateTime.Now;
            member.Name = data.Name;
            member.Email = data.Email;
            member.CategoryId = data.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("MemberList");
        }
	}
}