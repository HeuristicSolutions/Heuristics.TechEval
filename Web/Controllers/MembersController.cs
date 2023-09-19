using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Heuristics.TechEval.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly DataContext _context;
        private readonly MemberService _memberService;

        //TODO: Injection
        public MembersController()
        {
            _context = new DataContext();
            _memberService = new MemberService(_context);
        }

        public ActionResult List()
        {
            return View(new MembersViewModel() 
            { 
                Members = _memberService.FetchMembers().ToList(),
                Categories = _memberService.FetchCategories().ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult> New(Member data)
        {
            return !_memberService.IsMemberDataValid(data, errors: out var errors) ?
                JsonResultFormatted(errors, HttpStatusCode.InternalServerError)
                : JsonResultFormatted(await _memberService.SaveMember(data));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, Member data)
        {
            return !_memberService.IsMemberDataValid(data, Id: Id, errors: out var errors) ?
                JsonResultFormatted(errors, HttpStatusCode.InternalServerError)
                : JsonResultFormatted(await _memberService.SaveMember(data));
        }

        private ContentResult JsonResultFormatted(object obj, HttpStatusCode httpStatusCode = HttpStatusCode.OK) 
        {
            HttpContext.Response.StatusCode = (int)httpStatusCode;

            return Content(JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), "application/json");
        }





    }
}