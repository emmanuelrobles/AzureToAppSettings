namespace Azure_to_AppSettings

type Node =
    | Val of string
    | Children of Map<string, Node>

module AzureToNode =

    let processData (splitString: string) (data: (string * string) list) =
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
                    (process' (Array.toList (k.Split(splitString))) v acc) in processList t temp

        processList data (Children(Map.empty))

    // Node to string
    let nodeToString (keyWrapper:string * string) (valueWrapper:string * string) (nodeWrapper:string * string) (nodeConcatString: string) (keyValueConcat: string) (node: Node)  =
        let rec nodeToString' (node: Node) acc =
            match node with
            | Val s -> $"{fst valueWrapper}{s}{snd valueWrapper}"
            | Children node ->
                let temp =
                    node
                    |> Map.toSeq
                    |> Seq.map (fun (k, v) -> $"{fst keyWrapper}{k}{snd keyWrapper}{keyValueConcat}{(nodeToString' v acc)}")
                    |> String.concat nodeConcatString

                acc + $"{fst nodeWrapper}{temp}{snd nodeWrapper}"
        nodeToString' node ""
    
    // node to json
    let toJson =
        nodeToString ("\"","\"") ("\"","\"") ("{","}") "," ":"

    // node to vue settings
    let toVueSettings =
        nodeToString ("","") ("","") ("","") "\n" ":"