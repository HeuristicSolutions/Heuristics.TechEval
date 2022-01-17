using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly DataContext _context;

		public MembersController() {
			_context = new DataContext();
		}

		public ActionResult List() {
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpPost]
		public ActionResult New(NewMember data) {
			var newMember = new Member {
				Name = data.Name,
				Email = data.Email
			};
            
			if (IsDupEmail(newMember))
            {
				return Json(new { status = "error", message = "That email alrady exists. Please use a different email." } );
			}

			_context.Members.Add(newMember);
			_context.SaveChanges();

			return Json(JsonConvert.SerializeObject(newMember));
		}

		[HttpPost]
		public ActionResult Update(Member data)
		{

			var originalMember = _context.Members.FirstOrDefault(m => m.Id == data.Id);
			if (originalMember != null)
			{
				originalMember.Name = data.Name;
				originalMember.Email = data.Email;
				_context.SaveChanges();
			}

			return Json(JsonConvert.SerializeObject(data));
		}

		private bool IsDupEmail(Member member)
		{
			return _context.Members.Any(m => m.Email == member.Email);
		}
	}
}