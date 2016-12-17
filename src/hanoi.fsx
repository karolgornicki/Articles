open System 

let rec move n fromS toS auxS = 
    if n = 0 
    then []
    else
        move (n - 1) fromS auxS toS 
        |> List.append [sprintf "%s -> %s" fromS toS]
        |> List.append (move (n - 1) auxS toS fromS)

let hanoi n fromS toS auxS = 
    move n fromS toS auxS 
    |> List.rev

hanoi 3 "1" "2" "3"