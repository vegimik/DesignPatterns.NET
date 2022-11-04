// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
let _1_FSharp_Builder_Drive= _1_FSharp_Builder._1_FSharp_Builder_Drive
let _2_FSharp_Decorator_Drive= _2_FSharp_Decorator.main

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    printfn "\n"
    _1_FSharp_Builder_Drive argv
    printfn "\n"
    _2_FSharp_Decorator_Drive argv
    0 // return an integer exit code