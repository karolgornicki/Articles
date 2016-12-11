#List.map

Since list is a module, map is like a static functions defined in this module. Map is a function that takes 2 arguments:
A function that is used to transform objects 
A collection of those objects. 
Let’s look at the example. Say we want to multiply by 2 each element in the list. A function to multiply a single element would look like 

```fsharp
    fun x -> 2 * x 
```

In order to apply this function to every element of a list

```fsharp
    List.map (fun x -> 2 * x) [1; 2; 3]
```

F# offers some syntactic sugar here too. Let’s give a name to our anonymous function 

```fsharp
    let multBy2 = fun x -> 2 * x
```

Now we can use it as a parameter

```fsharp
    List.map multBy2 [1; 2; 3]
```

Since map expects a function that takes a single argument and multBy2 takes one argument we can just provide the name of the function.

Map function could be defined as:

```fsharp
    let rec map f list =
        match list with
        | [] -> []
        | x::xs -> (f x)::map f xs
```

F# does some clever optimization but this is the general idea of it. The original implementation can be found here [LINK]