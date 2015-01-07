namespace FSharpMVCLogonTest.Models

open System.ComponentModel.DataAnnotations

type UsernamePasswordModel(un : string, pw : string) =

  [<Required>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)>]
  member val UserName = un with get, set

  [<Required>]
  [<DataType(DataType.Password)>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)>]
  member val Password = pw with get, set

  new() = UsernamePasswordModel("", "")