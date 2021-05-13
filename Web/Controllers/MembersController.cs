using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using Heuristics.TechEval.Web.Services;
using Heuristics.TechEval.Web.Services.Implementations;
using System.Data;
using System;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly MemberService _memberService;

		public MembersController() {
			_memberService = new MemberService();
		}

		public ActionResult List() {

			var allMembers = _memberService.GetMembers();

			return View(allMembers);
		}

		[HttpGet]
		public ActionResult New()
		{
			return PartialView("_New");
		}

		[HttpPost]
		public ActionResult New(MemberModel data) {

			if(!ModelState.IsValid)
            {
				return View();
			}

			var newMember = _memberService.AddMember(data);

			return Json(JsonConvert.SerializeObject(newMember));
		}

		[HttpGet]
		public ActionResult Edit(int Id)
		{
			// TODO: Add a check Id is not null

			var memberToEdit = _memberService.GetMember(Id);

			return PartialView("_Edit", memberToEdit);
		}

		[HttpPost]
		public ActionResult Edit(MemberModel data)
		{
			var updatedMember = _memberService.UpdateMember(data);

			return Json(JsonConvert.SerializeObject(updatedMember));
		}
	}
}