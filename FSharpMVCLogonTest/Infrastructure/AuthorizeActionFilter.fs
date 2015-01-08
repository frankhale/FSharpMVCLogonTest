namespace FSharpMVCLogonTest.Infrastructure

open System.Web.Mvc
open System.Web.Routing

open Collections

type AuthorizeActionFilter() =
  inherit ActionFilterAttribute()
  override this.OnActionExecuting(filterContext : ActionExecutingContext) =
    // This is a bit lame / naive / silly ... we could do much better here!
    if filterContext.HttpContext.Session.["CurrentUser"] = null then
      let routeValueDict = init ["action", "Logon"; "controller", "Home"] : RouteValueDictionary
      filterContext.Result <- RedirectToRouteResult(RouteValueDictionary(routeValueDict))