using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using Heuristics.TechEval.Web.ExtensionMethods;
using Heuristics.TechEval.Web.ViewModels;
using Heuristics.TechEval.Core.Repositories;
using Heuristics.TechEval.Web.Services;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

        private readonly IMemberRepository _memberRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IResponseService _responseService;

		public MembersController(IMemberRepository memberRepository, ICategoryRepository categoryRepository, IResponseService responseService) {
            _memberRepository = memberRepository;
            _categoryRepository = categoryRepository;
            _responseService = responseService;
		}

		public ActionResult List() {
            var allMembers = _memberRepository.GetMembers().ToList();
            var allCategories = _categoryRepository.GetCategories().ToList();

			var viewModel = new MembersViewModel()
			{
				Members = allMembers,
				Categories = allCategories,
			};
            return View(viewModel);
		}

		[HttpPost]
		public ActionResult New(NewMember model) {
            if (!ModelState.IsValid)
			{
                _responseService.SetStatusCode(Response, 400);
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }
			
            var isExistingEmail = _memberRepository.IsExistingEmail(model.Email);

			if (isExistingEmail == true)
			{
                _responseService.SetStatusCode(Response, 400);
                ModelState.AddModelError("Email", "This email is already in the system.");
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }

			var newMember = new Member
            {
                Name = model.Name,
                Email = model.Email,
				CategoryId = model.CategoryId,
                LastUpdated = DateTime.Now
            };

            _memberRepository.AddMember(newMember);

            var category = _categoryRepository.GetCategory(model.CategoryId);
            newMember.Category = category;

            return Json(JsonConvert.SerializeObject(newMember));
        }

        [HttpPost]
        public ActionResult Edit(EditMember model)
        {
            if (!ModelState.IsValid)
            {
                _responseService.SetStatusCode(Response, 400);
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }

            var member = _memberRepository.GetMember(model.Id);

            if (member.Email != model.Email)
            {
                var isExistingEmail = _memberRepository.IsExistingEmail(model.Email);

                if (isExistingEmail == true)
                {
                    _responseService.SetStatusCode(Response, 400);
                    ModelState.AddModelError("Email", "This email is already in the system.");
                    var modelErrors = ModelState.AllErrors();
                    return Json(modelErrors);
                }
                member.Email = model.Email;
            }

            member.Name = model.Name;
            member.CategoryId = model.CategoryId;
            member.LastUpdated = DateTime.Now;
            _memberRepository.UpdateMember(member);

            var category = _categoryRepository.GetCategory(model.CategoryId);
            member.Category = category;

            return Json(JsonConvert.SerializeObject(member));
        }
    }
}