open Newtonsoft.Json

type Node =
    | Val of string
    | Children of Map<string, Node>


[<EntryPoint>]
let main argv =
    let json =
        JsonConvert.DeserializeObject<Map<string, string>>(System.IO.File.ReadAllText argv.[0])

    let rec processJson (json: (string * string) list) (acc: Node) =
        let rec process' (keys: string list) value (node: Node) : Node =
            match (keys, node) with
            | [], _ -> (Val(value))
            | element :: tail, Children children ->
                if Map.containsKey element children then
                    Children(Map.add element (process' tail value (Map.find element children)) children)
                else
                    Children(Map.add element (process' tail value (Children(Map.empty))) children)
            | _ -> failwith "Error"
            in
        match json with
        | [] -> acc
        | (k, v) :: t ->
            let temp = (process' (Array.toList (k.Split("__"))) v acc) in
            processJson t temp

    // given a node convert it to a json
    let rec toJson (node: Node) acc =
        match node with
        | Val s -> $"\"{s}\""
        | Children node ->
            let temp = node |> Map.toSeq |> Seq.map (fun(k,v) -> $"\"{k}\":  {(toJson v acc)}" ) |> String.concat ","
            in acc + $"{{{temp}}}"
    
    // write to file
    let writeToFile loc data =
        System.IO.File.WriteAllText(loc,data)
        

    
    writeToFile argv.[1] (toJson (processJson (Map.toList json) (Children(Map.empty))) "")
    0 // return an integer exit code
