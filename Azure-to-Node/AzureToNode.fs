namespace Azure_to_AppSettings

type Node =
    | Val of string
    | Children of Map<string, Node>

module AzureToNode =

    let processData (data: (string * string) list) =
        let rec processList (data: (string * string) list) (acc: Node) =
            let rec process' (keys: string list) value (node: Node) : Node =
                match (keys, node) with
                | [], _ -> (Val(value))
                | element :: tail, Children children ->
                    if Map.containsKey element children then
                        Children(Map.add element (process' tail value (Map.find element children)) children)
                    else
                        Children(Map.add element (process' tail value (Children(Map.empty))) children)
                | _ -> failwith "Error"

            match data with
            | [] -> acc
            | (k, v) :: t ->
                let temp =
                    (process' (Array.toList (k.Split("__"))) v acc) in processList t temp

        processList data (Children(Map.empty))

    let toJson (node: Node) =
        let rec toJson' (node: Node) acc =
            match node with
            | Val s -> $"\"{s}\""
            | Children node ->
                let temp =
                    node
                    |> Map.toSeq
                    |> Seq.map (fun (k, v) -> $"\"{k}\":  {(toJson' v acc)}")
                    |> String.concat ","

                acc + $"{{{temp}}}"

        toJson' node ""


    let toVueSettings (node: Node) =
        let rec toVueSettings' (node: Node) acc =
            match node with
            | Val s -> $"{s}"
            | Children node ->
                let temp =
                    node
                    |> Map.toSeq
                    |> Seq.map (fun (k, v) -> $"{k}:{(toVueSettings' v acc)}")
                    |> String.concat "\n"

                acc + $"{temp}"
        toVueSettings' node ""
