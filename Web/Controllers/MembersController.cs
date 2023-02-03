using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Models;
using Newtonsoft.Json;

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
            try
            {
                if (ModelState.IsValid)
                {
                    if (data.Id > 0)
                    {
                        var currentMember = _context.Members.FirstOrDefault(x => x.Id == data.Id);

                        if (currentMember != null)
                        {
                            if (_context.Members.Any(x => x.Email == data.Email && x.Id != data.Id))
                            {
                                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, "Duplicate email.");
                            }
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

                // TODO: Add errors to the view
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList();
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, JsonConvert.SerializeObject(errors));
            }
            catch
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, "Unexpected Error.");
            }
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