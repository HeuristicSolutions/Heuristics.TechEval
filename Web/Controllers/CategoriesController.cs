using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Core.Repositories;
using Heuristics.TechEval.Web.ViewModels;

namespace Heuristics.TechEval.Web.Controllers {

	public class CategoriesController : Controller {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

		public ActionResult List() {
            var categoriesWithMembers = _categoryRepository.GetCategories().ToList();

            var viewModel = new CategoriesViewModel()
			{
				Categories = categoriesWithMembers
            };
			return View(viewModel);
		}
	}
}