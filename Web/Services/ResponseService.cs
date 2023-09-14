using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.Services
{
    public interface IResponseService
    {
        void SetStatusCode(HttpResponseBase response, int statusCode);
    }

    public class ResponseService : IResponseService
    {
        public void SetStatusCode(HttpResponseBase response, int statusCode)
        {
            response.StatusCode = statusCode;
        }
    }

}