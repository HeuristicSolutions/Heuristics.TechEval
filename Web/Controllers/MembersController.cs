using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;

namespace Heuristics.TechEval.Web.Controllers
{

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
            if (data.Id > 0)
            {
                var currentMember = _context.Members.FirstOrDefault(x => x.Id == data.Id);
                if (currentMember != null)
                {
                    currentMember.Name = data.Name;
                    currentMember.Email = data.Email;
                    currentMember.LastUpdated = DateTime.UtcNow;
                }
            }
            else
            {
                if (_context.Members.Any(x => x.Email == data.Email))
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, "Duplicate email.");
                }
                var newMember = new Member
                {
                    Name = data.Name,
                    Email = data.Email,
                    LastUpdated = DateTime.UtcNow
                };
                _context.Members.Add(newMember);
            }

			_context.SaveChanges();

			return Json("");
		}

        [HttpGet]
        public ActionResult Details(int Id)
        {
            var member = new Member();
            if (Id > 0)
            {
                member = _context.Members.FirstOrDefault(x => x.Id == Id);
            }
            return PartialView("_Details", member);
        }
    }
}