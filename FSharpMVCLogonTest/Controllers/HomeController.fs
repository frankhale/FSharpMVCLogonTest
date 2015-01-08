namespace FSharpMVCLogonTest.Controllers

open System.Web.Mvc
open FSharpMVCLogonTest.Infrastructure
open FSharpMVCLogonTest.Models

[<HandleError>]
type HomeController() =
  inherit Controller()

  member private this.InvalidLoginAttempt(model : UsernamePasswordModel) =
    this.ModelState.AddModelError("", "Invalid login attempt.")
    this.View(model) :> ActionResult

  member private this.LogUserIn(model : UsernamePasswordModel) =
    this.Session.["CurrentUser"] <- model.UserName
    this.RedirectToAction("Index", "Home") :> ActionResult

  /////////////
  // Actions //
  /////////////

  member this.Index() = this.View()
  member this.Logon() = this.View()

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Logon(model : UsernamePasswordModel) =
    [this.ModelState.IsValid; model.ValidateUser()]
    |> List.forall ((=) true)
    |> function
       | true -> this.LogUserIn(model)
       | _ -> this.InvalidLoginAttempt(model)

  [<AuthorizeActionFilter>]
  member this.Logout() =
    this.Session.["CurrentUser"] <- null
    this.RedirectToAction("Index", "Home")

  [<AuthorizeActionFilter>]
  member this.Secret() = this.View()