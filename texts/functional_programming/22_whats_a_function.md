#What’s a function?

Functional programming is intended to resemble mathematics, and for a good reason. Mathematical notation enforces rigor, and is usually very concise. As a result it’s easy to read, and reason about it - and with a bit of practice also write. 

At the core of functional programming are functions. For the purpose of this series we are going to adopt mathematical definition of a function. Function is a mapping of every element of a domain to one and only one element of co-domain. Moreover, a function for the same input data always returns the same output. What that means is that if we call the same function multiple times, always with the same parameters, it will each time return the same value. 

This sounds pretty obvious so let’s create a function which adds 2 numbers.

```fsharp
    let add x y =
        x + y 
```

Functions have 3 important properties:

* They are **consistent** - if we call the function multiple times, each with the same parameters, every time we get the same result 
* They **don’t change arguments** - if we look at function add which takes 2 arguments, x and y, it doesn’t change the value of any of them. Instead it creates a new value representing a sum of x and y.
* They give **names** to processes.

These properties may seem like something utterly obvious, but I’m sure on many occasions we all witnessed functions which violate them. Just as a demo, let’s look at the below C# function

```csharp
    int globalVar = 0;
    int AddToGlobal (int x)
    {
        globalVar += x;
        return globalVar;
    }
```

This function adds x to the global variable and then returns the current value of global. Obviously, if we call this function few times with an argument other than 0 each time we will get a different result. This kind of behavior can be frequently seen in procedural languages, like C# and it has a name - side effect. 

One may ask - what’s the problem with that? After all, the name of the function says it’s updating global. Well, yes and no. In this example it is pretty obvious what this function does, but just imagine a more complicated example. You as a developer, should be able to read the code and know what it does - you should now be able to debug it just to find out what it’s doing. Debugging is the last resource. 

This leads us to another problem with this function - it relies on some global variable. In our example, out global variable is only updated by this one function. However, we could easily write other functions that can also update the value of that variable - this would make it even harder to deduce from the code how the program behaves, not to mention asses if it’s the correct behavior. 

This is the root cause of many bugs (the other one is null) and functional languages either disallow this behavior altogether, or at least make it quite cumbersome to write code like that (in case of hybrid languages like F#). 

The last property is usually undermined but it’s actually quite fundamental - functions give names to processes. This allows us to be more declarative in our programs - you simply read the name of the function and instantly know what it’s doing. Thus it is very important to give meaningful names. Comments are also helpful, especially to explain business logic. 