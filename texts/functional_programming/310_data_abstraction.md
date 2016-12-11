#Data abstraction

We will start our conversation about functional data structures by introducing concept of data abstraction. In this essay we will present very fundamental concept explaining how data structures can be organized in F#.

Let's say we would like to implement arithmetic operations (like add and multiply) on rational numbers. For the purpose of this demo let's just say that single or double numeric types are not accurate enough. So, what we have to do it to create our own type.

Now, in C# we would normally start this task by defining a class with a constructor, properties and some methods like add. Although we could follow the same footsteps in F# most of the time we would do it the other way around.

When developing in F# (or for that matter in any functional language) we would employ technique called wishful thinking (or at least we call it that). We would simply say that if we want to create a rational number we would temporarily assume that we have a "create" function that we` can use.

```fsharp
let a = create x y
```

Obviously, at this moment compiler will tell you that it doesn't know what "create" is. Later we will have to implement it, but for now let's just assume that create exists. Let's now see how we would implement adding two rational numbers.

```fsharp
let a = create x1 y1
let b = create x2 y2
let c = add a b
```

That's pretty obvious. At the moment we have 2 undefined functions - "create" and "add". What's important though is that even we don't know how we are going to implement "create" yet we were able to use its return value in other function.

That's a very important feature - once we give something the name (e.g. create) we create an expectation what this function does. This way we can model our program in the most natural way - the way in which it is obvious to us what the program is doing without the overhead of how it is going to be implemented under the hood.

Obviously it's not always that easy - sometimes requirements that we have to work with are so poorly described that it takes a great effort to make any sense of them in the first place and then organize them in a logical manner. But once that is done the task becomes simpler.

OK - we have 2 undefined functions. Now we have to implement them. Let's start with add. Why with add? Because it's at the top of the hierarchy - first you have to use "create" function in order to create rational numbers, and then those numbers can be used in "add" function.

By first implementing "add" we can make a decision how we want to interact with our data. If, on the other hand, we would start by implementing "create" function we would have to already make a decision how other functions can interact with our data. It's simply much easier to make this decision when you writing a function that is actually using it - you are in a position where you can say "the most convenient way of extracting denominator would be ..." and simply do it.

Back to our problem, adding 2 rational numbers

    a   c   ad + bc
    - + - = -------
    b   d      bd

For example

    1   1   4 + 2   6
    - + - = ----- = -
    2   4     8     8

We would like to see 3/4 rather than 6/8. To reduce it we have to find the greatest common divisor and divide both numerator and denominator by it. So, the whole function would look like

```fsharp
let add x y =
    let num = x.Numerator * y.Denominator + y.Numerator * x.Denominator
    let den = x.Denominator * y.Denominator
    let gcd = getGcd num den
    create (num / gcd) (den / gcd)
```

I'm sure that the code above is self explanatory. You can observe that we used wishful thinking again - getGcd. So let's define it now

```fsharp
let rec getGcd a b =
    if b = 0
    then a
    else getGcd b (a % b)
```
    
% is a modulo division operator.

If you have OO background you may say that there is nothing wrong with our add implementation. Some people, especially in functional programming circles, can frown upon the idea of using properties.

As a general rule they are right - the fact that F# allows you to use properties is largely due to its close ties with C# which is object oriented. Alternatively we could have "num" and "dec" functions which job is to extract these values. Our updated definition would look like

```fsharp
let add x y =
    let numerator = (num x) * (den y) + (num y) * (den x)
    let denominator = (den x) * (den y)
    let gcd = getGcd numerator denominator
    create  (numerator / gcd) (denominator / gcd)
```
        
So, what are the benefits of this implementation? It's independent of how rational numbers x and y are represented. Previously they had to have 2 properties. Now they don't - the only way to access numerator and denominator is through "num" and "den" functions.

This way we achieved separation between data and operations on them. They are separated by creator and accessor functions. This can be depicted as layers

[PICTURE-LAYERS]

This concept of layered architecture is called data abstraction. Our goal is to decouple data from operations. In C# on the other hand the idea of object oriented design is to represent data as objects, and those objects have various behaviors. In our case that would be "add" - similarly like String object has a method "Substring".

To complete the implementation let's add all missing definitions

```fsharp
let num (x:RationalNumber) =
    x.Numerator

let den (x:RationalNumber) =
    x.Denominator
```
    
and

```fsharp
type RationalNumber (x:int, y:int) =
    member val Numerator = x
    member val Denominator = y

let create x y =
    new RationalNumber (x, y)
```
    
In the next segment we'll talk in more details how F# type system works. For now - what's between parentheses these are constructor's arguments and 2 member values are read-only properties. Type is synonymous to C# class.

This is very OO-like definition. But, could we encode the same idea without using type? Actually, we could. Have a look at this implementation

```fsharp
let create x y =
    fun pick ->
        if pick = 1
        then x
        else y

let num x =
    x 1

let den x =
    x 2
```
    
In this implementation "create" function simply returns another function. Rather than using integers for "pick" it's better to use discriminated union (only mentioning here for completeness of the example, we will discuss it in detail later).

```fsharp
type RationalNumberPart =
| Num
| Den

let create x y =
    fun op ->
        match op with
        | Num -> x
        | Den -> y

let num x =
    x Num

let den x =
    x Den
```

Although the last design is very nice it is rarely seen in practice. There is no right and wrong answer here. Whether you choose one design over the other is usually a practical application - what better works for you and what's easier to understand (for you and your team).

Next: [F# type system](320_fsharp_type_system.md)