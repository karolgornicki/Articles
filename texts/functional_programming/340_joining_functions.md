#Joining functions - |> and >> 

In functional programming functions and data structures are just a basic building blocks. In this last segment of this series we’ll talk about how you can glue those building blocks together. 

When we first introduced the notion of a function we said that functions are expressions of data flow - from input to output. Or that they are like electric systems. 

Let’s say that we want to write a function which takes a list of integers as an input, raises each element of that list to the power of 2, and then summarizes all elements. One implementation of that function could be:

```fsharp
let f xs =
    let tmpXs = List.map (fun x -> x * x) xs
    let sum = List.fold (fun acc x -> acc + x) 0 tmpXs
    sum
```

We could alternatively use List.sum. Some say that if something that we are trying to do has a name (in this case sum) and is already implemented in API - we should simply use it. The counter argument is that whatever your background is - everyone knows about fold. If you’re coming from Haskell or Clojure, or any other functional language you know what’s fold and how it works. Sum is a simple example, but there might be some more specialized functions which are specific to only one API, and not others. We favor using fold, but there are people who disagreed with this approach. 

OK - our implementation, as of now, is pretty ugly. We create some intermediate step - tmpXs, and immediately pass it to some other function. F# has a solution for that, and it’s called pipe. Pipe is a binary operator |>. Well, in fairness it is a function that takes 2 arguments, and because its name is made up of special characters only, i.e. no digits or characters, it can be used between the 2 arguments, just like + or *. Pipe’s definition is very simple 

```fsharp
let (|>) x f = f x 
```

All it does it applies first argument to the second one. Thus, the second argument must be a function that takes x as an argument. As an example, this expression

```fsharp
List.map (fun x -> x * x) xs 
```

Can be rewritten as 

```fsharp
xs |> List.map (fun x -> x * x)
```

You can think of it in another way - take this data (xs) and push it to be processed by the function on the right to |>. This is the exhibit A where partial application really shines. 

On a side note, please notice that list argument is the last argument of List.map function. This is intentional. When you design your functions think about how they can be used with pipes and make sure that the object encapsulating data is last. We’ll see more of that in the upcoming segments. 

But that’s not all. We can now pipe our result to another function too. 

```fsharp
xs
|> List.map (fun x -> x * x)
|> List.fold (fun acc x -> acc + x) 0
```

In F# we tend to write |> operator on the new line - it improves the readability of our code (you probably do the same in LINQ - same idea here).

Now, this is much more functional. However, we can improve it even further. Recall from the math class operation called function composition. 

If we have 2 function f and g:

* f: X -> Y 
* g: Y -> Z

Function f takes elements from X and maps them to elements in Y (say it takes ints and converts them to strings), and similarly function g takes elements from Y and maps them to elements in Z. 

These functions can be composed (f composed with g): g o f, which means (g o f)(x) = g(f(x)).

We can express the same notion in F# using >> operator. It would be 

```fsharp
f >> g
```

The only difference is that in maths (and Haskell) we read this from right to left, whereas F# expression reads from left to right. Using >> operator, our function could be expressed as 

```fsharp
let f xs =
    let compoF =
        List.map (fun x -> x * x)
        >> List.fold (fun acc x -> acc + x) 0
    compoF xs
```

Or even simpler 

```fsharp
let f =
    List.map (fun x -> x * x)
    >> List.fold (fun acc x -> acc + x) 0
```

This is one of the most, if not the most fundamental feature of functional programming. It sets the stage for equational reasoning. Equational reasoning is an optimization technique. When we look at our code we basically can treat it as mathematical functions. And since we operate on immutable data structures, we know that they have properties of mathematical functions. Therefore, we can develop various laws and apply them.

##Equational reasoning

Just to give you a flavor of it, let’s imagine that we have a function which is processing a list. First is maps this list using f function, and then maps again using g function. 

```fsharp
let process xs =
    xs
    |> List.map f
    |> List.map g
```

Let’s use function composition (it looks a bit nicer). 

```fsharp
let process =
    List.map f
    >> List.map g
```

Everyone can see that this function has to traverse the list twice in order to accomplish the task. That’s a waste - you could do this operation in one traversal. 

Intuitively we can see that whether we first apply f to all elements and then g is no different if we first apply f and then g to the first element of xs, and in the next steps repeat the same process to subsequent elements. We can do that because our list is immutable. So, we would like to do something like 

```fsharp
xs
|> List.map (f and then g)
```

Which is exactly what >> does. Thus we can say 

```fsharp
let process =
    List.map (f >> g)
```

We can develop a law, which would say map f >> map g is equal to map (f >> g). We done it intuitively here, however, it can be mathematically proved that this phenomenon holds. Now, we can use it anywhere we see it in our code. It’s just reducing expressions that you did in arithmetics class in primary school where a^2 + 2ab + b^2 can be substituted with (a+b)^2 (and the other way around).

##In summary

These 2 operations are vital for functional programming to accomplish its chief mission - tackle complexity. They are predicated on

* Immutable data structures 
* Encapsulating routines using functions 
* Combining functions using |> and >>

Because our data structures are immutable it’s also trivially simple to parallelize our algorithms. 

That’s another aspect of F# - it provides us with a handful of basic data structures and functions to process them. If we can define our solutions using them, F# can take it an parallelize it for us, automatically. It’s almost like SQL to some degree - you are saying to select some records with some constraints (where clause) and SQL engine will decide what’s the best way to do it - here, F# will parallelize the process for you. You have to use a special functions, for example:

* Process sequentially: Array.map …
* Process in parallel: Array.Parallel.map … 

Yes, it’s that simple. 