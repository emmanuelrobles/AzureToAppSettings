//First argument = path to azure json, gotta be a file
//Second argument = output file

open Azure_to_AppSettings
open Newtonsoft.Json

[<EntryPoint>]
let main argv =
    let json =
        JsonConvert.DeserializeObject<Map<string, string>>(System.IO.File.ReadAllText argv.[0])

    // write to file
    let writeToFile loc data = System.IO.File.WriteAllText(loc, data)



    writeToFile argv.[1] (AzureToNode.toJson (AzureToNode.processData (Map.toList json)))
    0 // return an integer exit code
