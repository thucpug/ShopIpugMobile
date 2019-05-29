using Shop.Model.Model;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        IProductService _TagService;
        public ProductController(IErrorService errorService, IProductService TagService) : base(errorService)
        {
            this._TagService = TagService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keys, int page, int pageSize = 20)
        {

            return CreateHttpRespone(request, () =>
            {
                var listpost = _TagService.Getall(keys);
                int totalRow = listpost.Count();
                var query = listpost.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var paginationSet = new PaginationSet<Product>()
                {
                    Items = query,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Product tag)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    tag.CreatedDate = DateTime.Now;
                    tag.CreatedBy = "Admin";
                    _TagService.Add(tag);
                    _TagService.SaveChanges();
                    respone = request.CreateResponse(HttpStatusCode.Created);
                }
                return respone;
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getbyid/{id:int}")]
        public HttpResponseMessage Getbyid(HttpRequestMessage request, int id)
        {

            return CreateHttpRespone(request, () =>
            {
                Product listpost = _TagService.GetById(id);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listpost);
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage request, Product tag)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
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
        [HttpDelete]
        [Route("delete")]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
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
        
        [HttpDelete]
        [Route("deletemulti")]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpRespone(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    return response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProductCategory)
                    {
                        _TagService.Delete(item);
                    }

                    _TagService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }
                return response;
            });

        }
    }
}
