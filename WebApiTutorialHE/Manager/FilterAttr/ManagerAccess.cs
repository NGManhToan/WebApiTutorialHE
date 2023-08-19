using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiTutorialHE.Models.UtilsProject;
using Newtonsoft.Json;
using WebApiTutorialHE.Manager.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTutorialHE.Manager.FilterAttr
{
    public class ManagerAccess: ActionFilterAttribute
    {
        public ManagerAccess()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var headers = filterContext.HttpContext.Request.Headers;

            if (headers.ContainsKey("AuthorizationSwagger"))
            {
                headers["Authorization"] = headers["AuthorizationSwagger"];
            }

            if (headers.ContainsKey("Authorization"))
            {
                string token = headers["Authorization"].First();

                var managerRole = Utils.GetRoleFromToken(filterContext.HttpContext.Request);

                if (!String.IsNullOrEmpty(token) && token.Contains("Bearer "))
                {
                    token = token.Replace("Bearer ", "");

                    if (!Signature.CheckTokenValid(token))
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        filterContext.Result = new ContentResult
                        {
                            ContentType = "application/json",
                            Content = JsonConvert.SerializeObject(new ObjectResponse
                            {
                                result = 0,
                                message = "Xác thực thông tin thất bại. Vui lòng thử lại!"
                            })
                        };
                    }
                    else
                    {
                        //if (Signature.CheckTokenValid(token) && managerRole != 1)
                        //{
                        //    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        //    filterContext.Result = new ContentResult
                        //    {
                        //        ContentType = "application/json",
                        //        Content = JsonConvert.SerializeObject(new ObjectResponse
                        //        {
                        //            result = 0,
                        //            message = "Tài khoản không có quyền thực hiện. Vui lòng thử lại!"
                        //        })
                        //    };
                        //}

                        if (Signature.CheckTokenValid(token) && managerRole != 1 && managerRole != 2)
                        {
                            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            filterContext.Result = new ContentResult
                            {
                                ContentType = "application/json",
                                Content = JsonConvert.SerializeObject(new ObjectResponse
                                {
                                    result = 0,
                                    message = "Tài khoản không có quyền thực hiện. Vui lòng thử lại!"
                                })
                            };


                        }
                        //if (Signature.CheckTokenValid(token) && managerRole != 2)
                        //{
                        //    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        //    filterContext.Result = new ContentResult
                        //    {
                        //        ContentType = "application/json",
                        //        Content = JsonConvert.SerializeObject(new ObjectResponse
                        //        {
                        //            result = 0,
                        //            message = "Tài khoản không có quyền thực hiện. Vui lòng thử lại!"
                        //        })
                        //    };


                        //}
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    filterContext.Result = new ContentResult
                    {
                        ContentType = "application/json",
                        Content = JsonConvert.SerializeObject(new ObjectResponse
                        {
                            result = 0,
                            message = "Xác thực thông tin thất bại. Vui lòng thử lại!"
                        })
                    };
                }
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                filterContext.Result = new ContentResult
                {
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new ObjectResponse
                    {
                        result = 0,
                        message = "Xác thực thông tin thất bại. Vui lòng thử lại!"
                    })
                };
            }
        }
    }
}
