using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using System.Linq;
using System.Web.Mvc;

namespace Heuristics.TechEval.Web.Controllers
{

    public class CategoriesController : Controller {

		private readonly DataContext _context;

		public CategoriesController() {
			_context = new DataContext();
		}

		public ActionResult CategoryList()
        {

            var memberCounts = _context.Members
                .GroupBy(m => m.CategoryId)
                .Select(g => new { Id = g.Key, MemberCount = g.Count() });

            var categoryWithCount = _context.Categories.Join(
                memberCounts, 
                c => c.Id, 
                c => c.Id,
                (c, mc) => new CategoriesWithCount { Id = c.Id, Name = c.Name, MemberCount = mc.MemberCount }
                ).ToList();

			return View(categoryWithCount);
		}
	}
}