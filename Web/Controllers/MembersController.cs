using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using Heuristics.TechEval.Web.ExtensionMethods;

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
		public ActionResult New(NewMember model) {
            if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }

			var existingEmail = _context.Members.Where(m => m.Email == model.Email).FirstOrDefault();

			//if (existingEmail != null)
			//{

			//	return 
			//}

            var newMember = new Member
            {
                Name = model.Name,
                Email = model.Email,
                LastUpdated = DateTime.Now
            };

            _context.Members.Add(newMember);
            _context.SaveChanges();

            return Json(JsonConvert.SerializeObject(newMember));
        }

   //     [HttpPost]
   //     public ActionResult Edit(EditMember data)
   //     {
   //         var newMember = new Member
   //         {
   //             Name = data.Name,
   //             Email = data.Email,
   //             LastUpdated = DateTime.Now
   //         };

			//var member = _context.Members.Where(m => m.Id == data.Id).FirstOrDefault();
			//member.Email = data.Email;
			//member.Name = data.Name;
			//member.LastUpdated = DateTime.Now;

   //         _context.SaveChanges();

   //         return Json(JsonConvert.SerializeObject(member));
   //     }
    }
}