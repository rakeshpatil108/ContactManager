using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace API.COMMON
{
    public class CustomAuthorize : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IEnumerable<string> token = new string[] { };
            if (actionContext.Request.Headers.TryGetValues("authenticationToken", out token))
            {
                if (Configuration.AuthToken != token.FirstOrDefault())
                {
                    HttpContext.Current.Response.AddHeader("authenticationToken", token.FirstOrDefault());
                    HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                    return false;
                }

                HttpContext.Current.Response.AddHeader("authenticationToken", token.FirstOrDefault());
                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
                return true;
            }
            actionContext.Response =
              actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            actionContext.Response.ReasonPhrase = "Please provide valid inputs";
            return false;
        }
    }
}