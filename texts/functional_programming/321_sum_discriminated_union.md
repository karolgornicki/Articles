#Sum (discriminated union)

Sum means that our type can be of one of few types. Let’s put it another way - think about enums. 

```csharp
    enum Fruit
    {
        Apple,
        Banana,
        Orange
    }

    Fruit x = …
```

x can be one of 3 defined enums - either Apple, Banana, or Orange. F# takes this concept to the whole new level. In F# you can define a type which is one of few other types. Let’s look at the example

```fsharp
    type Trade =
    | NewTrade of volume:int * pricePerUnit:decimal
    | Update of id:int * volume:int * pricePerUnit:decimal
    | Cancel of id:int
```

Here we created a type Trade. It can take one of three forms - we can either create a new trade, update one or cancel. What’s even more significant - each type has different set of parameters. Why this is useful? It allows us to model our data in such a way that in order to, for example, cancel a trade all we have to do is to provide its ID. This way we only supply information that is needed.

Let’s look at an example and it will become obvious what benefits it brings

```fsharp
    let processOrder (o:Trade) =
        match o with
        | NewTrade(v, p) -> createOrder v p
        | Update(id, v, p) -> updateTrade id v p
        | Cancel(id) -> cancel id
```

What we did here is we discriminated against our type. Since our type is defined as one of 3 sub-types we can create a kind of switch-case expression what type object o is. This is what match expression dose. Notice, that we we said expression. This means that it returns a value. Therefore, each branch (case) must return the same type.

What’s even more powerful about this technique is support from the compiler. If, for example, you omit one type the compiler will warn you about that. 

Match expression is also called pattern matching. It can be used with any data structure. For example we can check someone’s age:

```fsharp
    let getPrize age =
        match age with
        | x when x <18 -> "toy"
        | 18 -> "beer"
        | _ -> "too old"
```

In this example we pretty much covered all typical use cases. F# tries each of them one by one, starting from the top. The first case that is true is returned. Let’s work through this example. Say we pass age = 20. 

At first it goes to 

```fsharp
    | x when x < 18 -> ...
```

It binds x with 20 (our age name) and then checks the when expression. If it’s true the code after the arrow is evaluated and returned. In our case it is false, so it moves to another case. 

```fsharp
    | 18 -> ...
```

Here we check if our age is equal to 18. It’s not so we move to the last option

```fsharp
    | _ -> ...
```

Here we used a wildcard which simply always evaluates to true. It’s kind of like default in switch-case. Notice that if we would do something like 

```fsharp
    match age with
    | _ -> "too old"
    | 18 -> "beer"
```

The compiler will warn you that some cases can’t be reached. 

In examples we also used "as" keyword. It allows us to bind value to a new name (locally). 