using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;

namespace Heuristics.TechEval.Web.Controllers
{
	public class MembersController : Controller
	{
		private readonly DataContext _context;

		public MembersController()
		{
			_context = new DataContext();
		}

		[HttpGet]
		public ActionResult List(string prevSort, string sortBy)
		{
			ViewBag.SortOrder = ToggleSort(prevSort, sortBy);

			return View(GetAllMembers());
		}

		private string ToggleSort(string prevSort, string sortBy)
		{
			if (string.IsNullOrEmpty(sortBy)) return "Name";
			if (sortBy == "Name")
			{
				return prevSort == "Name" ? "name_desc" : "Name";
			}

			if (sortBy == "Email") return prevSort == "Email" ? "email_desc" : "Email";
			if (sortBy == "Category") return prevSort == "Category" ? "category_desc" : "Category";

			return "Name"; // default
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			if (id == 0) return PartialView("_Details", new EditMember()); // no id, adding a new new member

			var member = _context.Members.FirstOrDefault(_ => _.Id == id);
			if (member == null) throw new Exception(string.Format("Member id {0} not found", id));

			var categories = _context.Categories.Select(_ => new SelectListItem {
				Value = _.Name,
				Text = _.Name
			}).ToList();
			ViewBag.MemberCategories = categories;

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

			// todo: this is where we could do something like check ModelState.IsValid

			var category = _context.Categories.FirstOrDefault(_ => _.Name == data.CategoryName);
			if (category == null) return BadRequest(string.Format("Category '{0}' not found!", data.CategoryName));

			var email = data.Email.ToLowerInvariant();
			if (data.Id == default)
			{
				// new member
				if (IsEmailTaken(email)) return BadRequest(string.Format("Email '{0}' is already taken", email));

				_context.Members.Add(new Member
				{
					Name = data.Name,
					Email = data.Email,
					CategoryId = category.Id,
					LastUpdated = DateTime.Now
				});
			}
			else
			{
				// updating an existing member
				var existing = _context.Members.FirstOrDefault(_ => _.Id == data.Id);
				if (existing == null) return BadRequest(string.Format("Member id {0} not found", data.Id));

				// if the member email is changing, make sure it's not a duplicate
				if(existing.Email != email)
				{
					if (IsEmailTaken(email)) return BadRequest(string.Format("Email '{0}' is already taken", email));
				}
				existing.Email = email;
				existing.Name = data.Name;
				existing.CategoryId = category.Id;
				existing.LastUpdated = DateTime.Now;
			}

			_context.SaveChanges();

			return PartialView("_List", GetAllMembers());
		}

		private int? GetMemberCategoryByName(EditMember member)
		{
			var category = _context.Categories.FirstOrDefault(_ => _.Name == member.CategoryName);
			if (category != null) return category.Id;
			return null;
		}

		private List<EditMember> GetAllMembers()
		{
			var sortOrder = ViewBag.SortOrder;
			var allMembers = from m in _context.Members select m;
			switch (sortOrder)
			{
				case "Name":
					allMembers = allMembers.OrderBy(_ => _.Name);
					break;
				case "name_desc":
					allMembers = allMembers.OrderByDescending(_ => _.Name);
					break;
				case "Email":
					allMembers = allMembers.OrderBy(_ => _.Email);
					break;
				case "email_desc":
					allMembers = allMembers.OrderByDescending(_ => _.Email);
					break;
				case "Category":
					allMembers = allMembers.OrderBy(_ => _.Category.Name);
					break;
				case "category_desc":
					allMembers = allMembers.OrderByDescending(_ => _.Category.Name);
					break;
				default:
					allMembers = allMembers.OrderByDescending(_ => _.Name);
					break;
			}

			return allMembers.Select(_ => new EditMember
			{
				Id = _.Id,
				Name = _.Name,
				Email = _.Email,
				CategoryName = _.Category.Name,
			}).ToList();
		}

		private bool IsEmailTaken(string email)
		{
			return _context.Members.Any(_ => _.Email == email);
		}

		private static ActionResult BadRequest(string message)
		{
			return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, message);
		}
	}
}