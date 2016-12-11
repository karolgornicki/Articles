#Problem: Square root algorithm

Once we have some understanding how functions should behave let’s put this in practice and solve some simple problem - let’s design a function which calculates square root. 

First we need to know what a square root is. Typical definition says that sqrt(x) = y such that y^2 = x and y >= 0. Obviously x >= 0. 

This definition doesn’t immediately help us solve our problem. It has a declarative form - it tells you what you are looking for, and not how to get there. It is our job to develop an algorithm (“how-to”)  that expresses steps that a computer can follow. 

To solve this problem we can use a very simple algorithm which is attributed to Hero of Alexandria (though the same method was known to Babylonians way before). The algorithm is very straight-forward:

* Make a guess 
* Check if your guess is good enough
  * If yes then return the guess 
  * If no then improve your guess and check again

So, how we can improve our guess? One way is to take an average of our guess, and fraction x over guess. This gives us a better estimate of our target, and actually converges quite quickly.

                 guess + x/guess
    newGuess  =  ---------------
                       2

It’s a pretty simple algorithm to let’s write it up. At first let’s do it in C#, and then let’s try to see how it can be changed into F# code. 

```csharp
double sqrt (double x) 
{
    double guess = 1;
    while (Math.Abs(guess * guess - x) >= 0.001)
    {
        guess = (guess + x/guess) / 2;
    }
    return guess;
}
```

That’s a pretty much 1-1 matching with our algorithm from the description. As an initial guess we can take 1, though, any other number would be fine too. 

Let’s analyze this short code. There are few problems with it:

* While statement - in the introduction we said that everything is a function and that there is no flow-control statements. 
* We use a variable (guess) to store intermediate states of our calculations, which value changes over the lifetime of the function.
* This function defines many subtasks at the same time (it’s not criticism of a language, just bad design on my part - which we will shortly improve):
  * It defines how to check if a guess is good enough.
  * It defines how to improve the guess.
  * It is in charge of deciding whether the guess is good enough and should be returned, or whether it requires further improvements.

Let’s now rewrite this function in F#. We have immediately to solve 2 problems:

* While loop 
* Storing intermediate values 

Later we will take care that our function only has a single responsibility. 

Just for full transparency, although F# provides us with a while … do expression we are not going to use it in this example, and for a very simple reason. It encourages bad design using mutable states - which is precisely what we want to avoid. Instead, we can write our own functions that accomplishes the same goal in an elegant way. 

The while step is actually quite interesting - it says check a condition - if it is true do some procedure and then check again, otherwise return a value. Each time we run a procedure we update some value (in our case it is intermediate value - a guess). This can be almost literally translated into function

```fsharp
    let rec whileLoop guess x = 
        if (Math.Abs(guess * guess - x) >= 0.001)
        then whileLoop ((guess + x/guess)/2) x
        else guess 
```

The sqrt function would now look like 

```fsharp
    let sqrt x = 
        whileLoop 1 x 
```

We used rec keyword in our definition. It marks that our function can be called recursively - without this keyword compiler would throw an error. 

It is interesting to notice that “if” in F# is an expression, meaning that it behaves just like a function and returns a value. That’s why it is also necessary to also include the else part. 

With this one function we also solved the other problem - storing an intermediate value. Since we cannot really update values we always have to create a new ones. That’s why we are passing an improved guess as a parameter to the function when we call it recursively. 
So now we have only one problem left - this function defines few different jobs. The principle of single responsibility is relevant in functional programming as much as in object-oriented world. It’s just a good practice. In our case we can create functions:

* improveGuess
* isGoodEnough

The final outcome would look like this. 

```fsharp
let isGoodEnough (guess:double) (x:double) :bool =
    Math.Abs(guess * guess - x) >= 0.001

let improveGuess (guess:double) (x:double) :double =
    (guess + x/guess)/2.0

let rec whileLoop (guess:double) (x:double) :double =
    if isGoodEnough guess x
    then whileLoop (improveGuess guess x) x
    else guess

let sqrt (x:double) :double =
    whileLoop 1.0 x
```

Although F# is able to infer types in most cases, it's a good pracice to explicitly provide them. It increases the readability of your code. In the code above, after defining all arguments we always have :[type] - this denotes the return type of the whole function.

As a rule of thumb, if it's practical and improves readability, include explicit type definitions.

Next: [Functions are data](24_functions_are_data.md)