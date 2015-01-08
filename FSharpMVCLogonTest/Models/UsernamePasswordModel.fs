namespace FSharpMVCLogonTest.Models

open System.ComponentModel.DataAnnotations
  
type UsernamePasswordModel() =
  [<Required>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)>]
  member val UserName = Unchecked.defaultof<string> with get, set

  [<Required>]
  [<DataType(DataType.Password)>]
  [<StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)>]
  member val Password = Unchecked.defaultof<string> with get, set
