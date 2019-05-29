using Shop.Model.Model;
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
    [RoutePrefix("api/posttag")]
    public class PostTagController : ApiControllerBase
    {
        IPostTagService _TagService;
        public PostTagController(IErrorService errorService, IPostTagService pTagService) : base(errorService)
        {
            this._TagService = pTagService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {

            return CreateHttpRespone(request, () =>
            {
                var listpost = _TagService.Getall();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listpost);
                return response;
            });


        }
        [HttpPost]
        [AllowAnonymous]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostTag tag)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _TagService.Add(tag);
                    _TagService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.Created);
                }
                return respone;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage request, PostTag tag)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _TagService.Update(tag);
                    _TagService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.OK);
                }
                return respone;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _TagService.Delete(id);
                    _TagService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.OK);
                }
                return respone;
            });
        }
    }
}
