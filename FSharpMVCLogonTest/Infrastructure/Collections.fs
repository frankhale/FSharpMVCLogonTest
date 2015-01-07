namespace FSharpMVCLogonTest.Infrastructure

// Thanks to http://stackoverflow.com/a/5341186/170217 for this!
module Collections = 
  let inline init s =
    let coll = new ^t()
    Seq.iter (fun (k,v) -> (^t : (member Add : 'a * 'b -> unit) coll, k, v)) s
    coll