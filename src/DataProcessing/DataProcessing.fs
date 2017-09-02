module DataProcessing

open FSharp.Data
open FSharpPlus

let [<Literal>] MSFT = "./data/MSFT.csv"

type Stock = CsvProvider<MSFT>

[<EntryPoint>]
let main argv =
    printfn "%A" argv

    let msft = Stock.GetSample()
    //let firstRow = msft.Rows |> Seq.head
    for row in msft.Rows do
        let d = row.Date
        printfn "%A" d
    let dates = msft.Rows |> map (fun r -> r.Date)
    0 // return an integer exit code
