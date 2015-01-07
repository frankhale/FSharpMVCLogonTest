namespace FSharpMVCLogonTest.Controllers

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax
open System.Web.Routing
open FSharpMVCLogonTest.Infrastructure
open FSharpMVCLogonTest.Models

[<HandleError>]
type HomeController() =
  inherit Controller()

  let ValidateUser(model : UsernamePasswordModel) =
    // Just do something simple so we can see if it works, LOL!
    match model.UserName.ToLower(), model.Password with
    | "frank", "123456" -> true
    | "matt", "123456" -> true
    | _ -> false

  member private this.InvalidLoginAttempt(model : UsernamePasswordModel) =
    this.ModelState.AddModelError("", "Invalid login attempt.")
    this.View(model) :> ActionResult

  member this.Index() = this.View()
  member this.Logon() = this.View()

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Logon(model : UsernamePasswordModel) =
    match this.ModelState.IsValid with
    | false -> this.InvalidLoginAttempt(model)
    | true ->
      match ValidateUser(model) with
      | true ->
        this.Session.["CurrentUser"] <- model.UserName
        this.RedirectToAction("Index", "Home") :> ActionResult
      | false -> this.InvalidLoginAttempt(model)

  [<AuthorizeActionFilter>]
  member this.Logout() =
    this.Session.["CurrentUser"] <- null
    this.RedirectToAction("Index", "Home")

  [<AuthorizeActionFilter>]
  member this.Secret() = this.View()