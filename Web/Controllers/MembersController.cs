using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI;

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
            ViewBag.MyCategories = new SelectList(_context.Categories, "Id", "Name");
            return View(allMembers);
        }

        [HttpPost]
        public ActionResult New(NewMember data)
        {
            if (ModelState.IsValid)
            {
                var newMember = new Member
                {
                    Name = data.Name,
                    Email = data.Email,
                    LastUpdated = DateTime.Now,
                    CategoryId = data.CategoryId
                };
                if (!EmailExists(newMember.Email))
                {

                    _context.Members.Add(newMember);
                    _context.SaveChanges();

                    return Json(new { status = "success" });
                }
                return Json(new { status = "Email already exists" });
            }
            return Json(new { status = "Entry Invalid" });
        }

        [HttpPut]
        public ActionResult UpdateMember(EditMember updatedMember)
        {
            if (ModelState.IsValid)
            {
                var member = _context.Members.Find(updatedMember.Id);

                if (member.Email.ToLower() != updatedMember.Email.ToLower())
                {
                    if (EmailExists(updatedMember.Email))
                        return Json(new { status = "Email already exists" });
                }

                member.Name = updatedMember.Name;
                member.Email = updatedMember.Email;
                member.LastUpdated = DateTime.Now;
                _context.SaveChanges();

                return Json(new { status = "success" });
            }
            return Json(new { status = "Entry invalid" });
        }

        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            return Json(EmailExists(email));
        }
        private bool EmailExists(string email)
        {
            return _context.Members.AsNoTracking().Any(x => x.Email.ToLower() == email.ToLower());
        }
    }
}