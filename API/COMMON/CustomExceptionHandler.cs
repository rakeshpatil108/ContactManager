using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;

using System.Web.Http.Filters;
using BusinessLogic;

namespace API.COMMON
{
    public class CustomExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogHandler.Instance.WriteLog((actionExecutedContext.Exception.InnerException == null)? 
                                            actionExecutedContext.Exception.Message: 
                                            actionExecutedContext.Exception.InnerException.Message);

           var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
               Content=new StringContent("An unhandled exception was thrown by service."),
               ReasonPhrase= "Internal Server Error.Please Contact your Administrator."
            };
            actionExecutedContext.Response = response;
        }
        
    }
}