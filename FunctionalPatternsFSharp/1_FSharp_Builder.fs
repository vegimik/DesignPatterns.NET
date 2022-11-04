module _1_FSharp_Builder

let p args =
    let allArgs = args |> String.concat "\n"
    ["<p>"; allArgs; "</p>"] |> String.concat "\n"

let img url = "<img src=\"" + url + "\"/>"

let _1_FSharp_Builder_Drive argv =
  let html =
    p [
      "Check out this picture";
      img "pokemon.com/pikachu.png"
    ]
  printfn "%s" html
  0