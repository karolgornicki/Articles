#Option type

In F# we also use option type. There is nothing special about it - in fact, it’s just a discriminated union. It can be one of 2 kinds:

* Something 
* Nothing 

```fsharp
    type Option<T> = 
    | Some of T
    | None
```

As you can see it has a generic parameter - just like in C#. The meaning is exactly the same. 

This type is widely used as a result of operations that may, or may not, succeed. As an example, let’s consider parsing a string to an int. In C# we have TryParse function. It returns a boolean value (true if we successfully parsed, otherwise false), and as a side effect it changes a value of a variable that was passed as an argument. The argument is passed with “out” keyword so it’s in a way a controlled, or explicit side effect. In F# if we would like to implement similar function it would look something like this - for simplicity we will implement a function parsetoTen.

```fsharp
    let parseToTen x =
        match x with
        | "10" -> Some 10
        | _ -> None
```

There is no side effects, and the returned type contains all the information needed - whether the parsing operation was successful, and if so, its underlying value.

Some people favor using lists where empty list would indicate that the operation failed, otherwise the function would return a list containing a single element (often list containing a single element is referred to as singleton - don’t confuse it the design pattern from OO). It’s not a completely crazy idea - it has some merits. It’s actually quite widely used in Haskell, where we also have list comprehensions. When we get to the point when we’ll be solving real problems we will discuss this issue in more details. 