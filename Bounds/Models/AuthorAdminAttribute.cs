using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Bounds.Models
{
    public class AuthorAdminAttribute : FilterAttribute, IAuthenticationFilter
    {
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["User"];
            if (user == null)
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var url_target = url.Action("Logon", "b_User", new { area = "" });
                filterContext.Result = new RedirectResult(url_target);
            }
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}