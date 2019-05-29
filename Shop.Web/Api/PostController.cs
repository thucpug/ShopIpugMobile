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
    [RoutePrefix("api/post")]
    public class PostController : ApiControllerBase
    {
        IPostService _PostService;
        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._PostService = postService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get")]
        public HttpResponseMessage Get(HttpRequestMessage request, string api)
        {

            return CreateHttpRespone(request, () =>
            {
                var listpost = _PostService.Getall();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listpost);
                return response;
            });


        }
        [HttpPost]
        [AllowAnonymous]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Post post)
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
                    _PostService.Add(post);
                    _PostService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.Created);
                }
                return respone;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage request, Post post)
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
                    _PostService.Update(post);
                    _PostService.SaveChanges();
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
                    _PostService.Delete(id);
                    _PostService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.OK);
                }
                return respone;
            });
        }
    }
}
