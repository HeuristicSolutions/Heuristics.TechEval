using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;

namespace Heuristics.TechEval.Web.Controllers {

	public class CategoriesController : Controller {

		private readonly DataContext _context;
        private readonly MemberService _memberService;

        public CategoriesController() {
			_context = new DataContext();
            _memberService = new MemberService(_context);
        }

        public ActionResult List() {
            var categories = _memberService.FetchCategories().ToList();
            var members = _memberService.FetchMembers().ToList();

            foreach (var category in categories)
            {
                category.MemberCount = members.Count(x => x.CategoryId == category.Id);
            }

            return View(new MembersViewModel()
            {
                Members = members,
                Categories = categories
            });
		}
	}
}