using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Heuristics.TechEval.Web.Controllers
{
	public class MembersController : Controller
	{
		private readonly DataContext _context;

		public MembersController()
		{
			_context = new DataContext();
		}

		public ActionResult List()
		{
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			if (id == 0) return PartialView("_Details", new EditMember()); // no id, adding a new new member

			var member = _context.Members.FirstOrDefault(_ => _.Id == id);
			if (member == null) throw new Exception(string.Format("Member id {0} not found", id));

			return PartialView("_Details", new EditMember
			{
				Id = member.Id,
				Name = member.Name,
				Email = member.Email
			});
		}

		[HttpPost]
		public ActionResult Edit(EditMember data)
		{
			if (data == null) BadRequest("No data provided");

			if (data.Id == default)
			{
				// new member
				_context.Members.Add(new Member
				{
					Name = data.Name,
					Email = data.Email,
					LastUpdated = DateTime.Now
				});
			}
			else
			{
				// updating an existing member
				var existing = _context.Members.FirstOrDefault(_ => _.Id == data.Id);
				if(existing == null) return BadRequest(string.Format("Member id {0} not found", data.Id));

				existing.Name = data.Name;
				existing.Email = data.Email;
				existing.LastUpdated = DateTime.Now;
			}

			_context.SaveChanges();

			var allMembers = _context.Members.ToList();
			return PartialView("_List", allMembers);
		}

		private static ActionResult BadRequest(string message)
		{
			return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, message);
		}
	}
}