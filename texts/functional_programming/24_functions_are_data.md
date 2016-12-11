#Functions are data 

In the previous segment we described how we can organize our function to calculate square root of x. Actually we can improve this even further, and this is what we are going to do here. In the process we will demonstrate a very important aspect of functional programming. Function, just like numerical data, can be assigned named, and passed around. We can use them as function arguments, as well as returned values. 

We appreciate that material presented in this section is quite difficult. However, it’s an investment - once you understand a bit complicated example it will be much easier for you to understand how this concept is used elsewhere. Treating functions as data is very common when processing functional data structures, so it’s really important that you have a good understanding of this. 

Observant student will notice that what this algorithm is doing it is calculating a fixed point of a function 

    f(guess) = avg (guess, x/guess)

Just as a reminder - fixed point of function f is such x for which f(x) = x holds. The technique of finding fixed point iteratively can also be used for solving equations.

OK, but how do we know that sqrt is actually calculating fixed point of this function? It’s actually very simple - in the implementation we were calling this function over and over until guess and x were close enough and we were calling our guess good enough and were reporting as a return value. If guess and x are close enough - almost equal to one another - their average would be of the same value. In other words, by calling this function over and over f(guess) its value will be gradually converting to guess. And that’s the definition of a fixed point.

So, our new design would say that we want to calculate a fixed point for that particular function. It sounds like a function itself is an argument - we have a function that calculates fixed point and all we have to do is to pass a function to it for which we want to find fixed point. Let’s start coding

```fsharp
    let isCloseEnough (a:double) (b:double) =
        Math.Abs(a - b) <= 0.001

    let fixedPoint f start =
        let rec loop oldValue newValue =
            if (isCloseEnough oldValue newValue)
            then newValue
            else loop newValue (f newValue)
        loop start (f start)
```

We simply check if our x value is close enough to f(x). If they are, meaning f(x) = x, we call x a fixed point and return it. Otherwise we apply f to our new value and call loop function again. 

The implementation of sqrt is pretty simple:

```fsharp
    let sqrt x =
        fixedPoint f 1.0
```

Now all that is left is to define f. This is the hardest part, but, it showcases a very important aspect of F# - how functions are constructed.

In F# we can define a function mult2 as 

```fsharp
    let mult2 x = 2 * x
```

However, we can accomplish exactly the same goal using anonymous functions (also called lambda functions). We simply bind an anonymous function to a name - which makes it a named function. 

```fsharp
    let mult2 = fun x -> 2 * x
```

In fact, this technique is used in F# under the hood. Recall function add from earlier part of the tutorial

```fsharp
    let add x y = x + y 
```

It is actually defined as 

```fsharp
    let add =
        fun x ->
            fun y -> x + y
```

Furthermore, the signature of our add function (however it is defined in our code) is

    val add : x:int -> y:int -> int

This fits perfectly with lambda notation - with name add we bind anonymous function which takes one argument (x) and returns anonymous function with takes one argument (y) and returns an int (sum of x and y). Now also arrows make sense in the type declaration. What’s on the left of the arrow is what function takes, what’s on the right is what function returns - in this case, another function!

Our observant student can spot one more thing - we don’t have to apply all parameters. We can use our function to create other functions. For example 

```fsharp
    let add1 = add 1 
```

Add1 is a new function that takes one parameter and adds 1 to it. When we applied only a single parameter to add function we provided x and returned anonymous function where x was bound with value 1.

```fsharp
    fun y -> 1 + y
```

This technique is called partial application, and we are going to use it now.

Coming back to our original problem. We want to define f such that

    avg(guess, x/guess)

Our avg function can be expressed as 

```fsharp
    let avg a b =
        (b + a/b) / 2.0
```

And all we have to do is to update definition of sqrt 

```fsharp
    let sqrt x =
        let f = avg x
        fixedPoint f 1.0
```

This way we decoupled the general method - finding a fixed point, from a particular function - sqrt. Now, we can also apply fixedPoint function to solve equations and whatnot.

Full code can be found here: [LINK]