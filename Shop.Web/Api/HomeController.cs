using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/home")]   
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TestMethod")]
        public HttpResponseMessage TestMethod(HttpRequestMessage request)
        {
            return CreateHttpRespone(request, () =>
            {

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, "OK test");
                return response;
            });
        }
    }
}
