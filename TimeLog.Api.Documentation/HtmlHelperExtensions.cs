using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation
{
    public static class HtmlHelperExtensions
    {
        public static string DetectActiveMenu(this HtmlHelper helper, string controller, bool isDefault = false)
        {
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains(controller.ToLower()))
            {
                return "current";
            }

            if (isDefault && HttpContext.Current.Request.Url.AbsolutePath.ToLower().Trim('/') == new UrlHelper(HttpContext.Current.Request.RequestContext).Content("~/").ToLower().Trim('/'))
            {
                return "current";
            }

            return string.Empty;
        }
    }
}