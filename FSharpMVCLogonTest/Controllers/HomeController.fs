namespace FSharpMVCLogonTest.Controllers

open System.Web.Mvc
open FSharpMVCLogonTest.Infrastructure
open FSharpMVCLogonTest.Models

[<HandleError>]
type HomeController() =
  inherit Controller()

  member this.Index() = this.View()
  member this.Logon() = this.View()

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Logon(model : UsernamePasswordModel) =
    let isValid = [this.ModelState.IsValid; model.ValidateUser()]
                  |> List.forall ((=) true)

    match isValid with
    | true -> 
      this.Session.["CurrentUser"] <- model.UserName
      this.RedirectToAction("Index", "Home") :> ActionResult
    | false -> 
      this.ModelState.AddModelError("", "Invalid login attempt.")
      this.View(model) :> ActionResult

  [<AuthorizeActionFilter>]
  member this.Logout() =
    this.Session.["CurrentUser"] <- null
    this.RedirectToAction("Index", "Home")

  [<AuthorizeActionFilter>]
  member this.Secret() = this.View()