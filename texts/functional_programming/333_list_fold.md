#List.fold

Fold is another important function. Pretty much any processing of a list (or for that reason any other collection) can be done with fold. What it does is very simple - it traverses the list and during that process it updates accumulator. Let’s look at example, say we want to sum elements on the list. We can use fold function

```fsharp
let sum xs = List.fold (fun acc x -> x + acc) 0 xs
```

First argument is a function that takes, first an accumulator and then an element of a list, and returns a new accumulator. Then we provide a default (starting) value for our accumulator and at the last the list itself. 

We can add minor improvement to our initial definition of sum function. 

```fsharp
let sum = List.fold (fun acc x -> x + acc) 0
```

We dropped xs on both ends. Which is fine - sum is simply an abbreviation for the function on the right-hand side of =. This function takes one argument - a list of integers. 

Haskell programmers do that all the time - it’s a less common practice in F#. 

Next: [Lists - summary](334_lists_summary.md)