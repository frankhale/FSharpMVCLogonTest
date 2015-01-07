namespace FSharpMVCLogonTest.Infrastructure

open System.Web.Mvc
open System.Web.Routing

type AuthorizeActionFilter() = 
  inherit ActionFilterAttribute()
    override this.OnActionExecuting (filterContext : ActionExecutingContext) = 
      if filterContext.HttpContext.Session.["CurrentUser"] = null then do        
        let redirectDictionary = new RouteValueDictionary()
        redirectDictionary.Add("action", "Logon")
        redirectDictionary.Add("controller", "Home")
        filterContext.Result <- new RedirectToRouteResult(new RouteValueDictionary(redirectDictionary)) 
