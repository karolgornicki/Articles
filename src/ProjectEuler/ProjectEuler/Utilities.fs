namespace ProjectEuler

module Utilities = 

    /// Removes the last element from given list.
    let init (xs:list<'a>) :list<'a> = 
        let rec doInit acc col =
            match col with 
            | x1::x2::[] -> x1::acc |> List.rev
            | x1::[] -> []
            | x1::xs -> doInit (x1::acc) xs 
            | _ -> []

        doInit [] xs

    /// Transposes given matrix. 
    let transpose (xss:list<list<'a>>) :list<list<'a>> = 
        let extract f xs = 
            xs 
            |> List.filter (not << List.isEmpty)
            |> List.map f
        let rec doTranspose (xss:list<list<'a>>) :list<list<'a>> = 
            match xss with 
            | [] -> [] 
            | _ -> 
                let firstRow = xss |> extract List.head
                let rest = xss |> extract List.tail
                firstRow :: (doTranspose rest)
        doTranspose xss |> init 

    // Calculates a product of a list of integers.
    let product (xs:list<int>) :int = 
        List.fold (fun acc x -> acc * x) 1 xs

    // Zips 2 lists together and then maps.
    let zipWith f xs ys = 
        let rec zip list1 list2 = 
            match list1, list2 with 
            | (x::xs, y::ys) -> (x, y) :: zip xs ys
            | _ -> []
        List.map f (zip xs ys)

    // Removes number of elements from the list, starting from head.
    let rec drop (n:int) (xs:list<'a>) :list<'a> = 
        match (n, xs) with 
        | n, _::ys when n > 0 -> drop (n-1) ys
        | _ -> xs
