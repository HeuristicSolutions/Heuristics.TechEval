using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Repositories;
using Heuristics.TechEval.Web.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Heuristics.TechEval.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IResponseService, ResponseService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}