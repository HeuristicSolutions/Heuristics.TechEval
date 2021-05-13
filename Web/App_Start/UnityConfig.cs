using Heuristics.TechEval.Web.Services;
using Heuristics.TechEval.Web.Services.Implementations;
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

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMemberService, MemberService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}