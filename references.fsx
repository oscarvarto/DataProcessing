#r @"packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r @"packages/FSharpPlus/lib/net40/FSharpPlus.dll"
#r @"packages/FsControl/lib/net40/FsControl.Core.dll"
#r @"packages/Deedle/lib/net40/Deedle.dll"
#r @"packages/FSharp.Charting/lib/net45/FSharp.Charting.dll"
#r @"packages/MathNet.Numerics/lib/net40/MathNet.Numerics.dll"
#r @"packages/MathNet.Numerics.FSharp/lib/net40/MathNet.Numerics.FSharp.dll"

#r @"/Library/Frameworks/Mono.framework/Versions/5.2.0/lib/mono/4.6.2-api/System.Core.dll"
#r @"/Library/Frameworks/Mono.framework/Versions/5.2.0/lib/mono/4.6.2-api/System.Numerics.dll"
#r @"/Library/Frameworks/Mono.framework/Versions/5.2.0/lib/mono/4.6.2-api/System.Xml.Linq.dll"
#r @"/Library/Frameworks/Mono.framework/Versions/5.2.0/lib/mono/4.6.2-api/System.dll"

//#load @"/Users/oscarvarto/gitRepos/fsharpWork/DataProcessing/src/DataProcessing/DataProcessing.fs"
#load "packages/Deedle/Deedle.fsx"

open FSharp.Data
open System
open System.Linq
//open FSharpPlus
open Deedle
open FSharp.Charting

open MathNet.Numerics
open MathNet.Numerics.Random

let isSortedBy f = Seq.pairwise >> Seq.forall (fun (a, b) -> f a b)

let [<Literal>] MSFT = "./src/DataProcessing/data/MSFT.csv"

type Stock = CsvProvider<MSFT>
let msft = Stock.GetSample()

(*
let dates = msft.Rows |> Seq.map (fun r -> r.Date)
let isDatesSortedDescending = dates |> isSortedBy (>=)

// Compute some statistics for Open Column: Average, Std Deviation.
// Plot data

let openNs = msft.Rows |> Seq.map (fun r -> r.Open)

//let openNsMean = openNs.Average()
*)

let dates =
    [ DateTime(2013, 1, 1);
      DateTime(2013, 1, 4);
      DateTime(2013, 1, 8) ]
 
let values =
  [ 10.0; 20.0; 30.0 ]

let first = Series(dates, values)

(*
let s1 =
  Series.ofObservations
    [ DateTime(2013, 1, 1) => 0.0;
      DateTime(2013, 1, 4) => 0.0;
      DateTime(2013, 1, 8) => 0.0  ]
*)

let dateRange (first: DateTime) count = Enumerable.Range(0, count) |> Seq.map (float >> first.AddDays )

let rand (count: int) = SystemRandomSource.Doubles(length = count, seed = 42)

let second = Series(dateRange (DateTime(2013, 1, 1)) 10, rand 10)

let df1 = Frame(["first"; "second"], [first; second])