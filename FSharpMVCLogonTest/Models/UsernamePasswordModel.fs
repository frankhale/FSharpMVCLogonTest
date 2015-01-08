namespace FSharpMVCLogonTest.Models

open System
open System.ComponentModel.DataAnnotations
  
type UsernamePasswordModel() =
  [<Required>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)>]
  member val UserName = String.Empty with get, set

  [<Required>]
  [<DataType(DataType.Password)>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)>]
  member val Password = String.Empty with get, set

  member this.ValidateUser() =
    // Just do something simple so we can see if it works, LOL!
    match this.UserName.ToLower(), this.Password with
    | "frank", "123456" -> true
    | "matt", "123456" -> true
    | _ -> false