namespace FSharpMVCLogonTest.Infrastructure

open System.Web.Mvc
open System.Web.Routing

type AuthorizeActionFilter() =
  inherit ActionFilterAttribute()
  override this.OnActionExecuting(filterContext : ActionExecutingContext) =
    // This is a bit lame / naive / silly ... we could do much better here!
    if filterContext.HttpContext.Session.["CurrentUser"] = null then
      do let redirectDictionary = new RouteValueDictionary()
         ["action", "Logon"; "controller", "Home"] |> Seq.iter redirectDictionary.Add
         filterContext.Result <- new RedirectToRouteResult(new RouteValueDictionary(redirectDictionary))