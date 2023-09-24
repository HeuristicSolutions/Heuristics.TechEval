using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly DataContext _context;

		public MembersController(DataContext context = default) {
			if (context != null)
            {
				_context = context;
            }
            else
            {
				_context = new DataContext();
            }
		}

		public ActionResult List() {
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpPost]
		public ActionResult New(NewMember data)
        {
            var duplicateEmails = _context.Members.Where(m => m.Email == data.Email).ToList();
			if (duplicateEmails.Any())
			{
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.Conflict, $"Email {data.Email} already in use");
			}
			var newMember = new Member
			{
				Name = data.Name,
				Email = data.Email,
				LastUpdated = DateTime.Now
			};

			_context.Members.Add(newMember);
			_context.SaveChanges();

			return RedirectToAction("List");
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
            _context.SaveChanges();
            return RedirectToAction("List");
        }
	}
}