#Implementation analysis 

In this segment we would like to focus on how functions are used what impact it may have on performance and memory consumption. 

Let’s say we are in charge of implementing a very basic library for F# and our task is to create add function which adds two natural numbers. To demonstrate the point, let’s assume that + operator is not available to us. We can use inc and dec functions, which respectively produce a new number, either incremented or decremented by one. For example

```fsharp
    let x = inc 5 
```

x will have value 6.

What’s interesting some architectures were actually using this technique (also called Peano arithmetics). Just think how simple it is - the number is stored as a binary code, using 0s and 1s. All computer can do is flick bits - from 0 to 1 and the other way around. As an architect of a processor you may provide some basic operations, but that’s about it. It’s a job of a programming language to abstract away some basic operations that everyone needs. And this is what we are going to show here. 

So, back to our original problem - implementing add function. We can assume that functions comparing 2 numbers is also provided.

Let’s look at the example - say we want to add 2 and 3.How would you do it given inc, dec and = functions? One way to approach this problem is this. Let’s call the first number x and the second y. We can construct our algorithm in a way that at first it checks if x is equal to 0. If it is, then return y. Otherwise subtract 1 from x and increment y by 1, and repeat the whole process. This way at some point we will arrive with x reduced to 0, and then we will return y. Pretty simple. The code for this would look like this 

```fsharp
    let rec add x y =
        if x = 0
        then y
        else add (dec x) (inc y)
```

That’s one way to write it, let’s now look at an alternative implementation 

```fsharp
    let rec add’ x y =
        if x = 0
        then y
        else inc (add’ (dec x) y)
```

What this function does it builds an expression and only at the end it evaluates it. To see this clearly let’s examine all intermediate steps when each function is called 

First function:

    add 2 3 
    add 1 4 
    add 0 5 
    5

Second function 

    add’ 2 3 
    inc (add’ 1 3)
    inc (inc (add’ 0 3))
    inc (inc 3)
    inc 4
    5

In both cases we arrive with the same results, but the ways in which function computes are very different . The first implementation of add can produce result in 4 steps. Another thing to notice is that during each step it needs to know the function (add) and its 2 arguments (that’s all). We call that functions computes in constant space O(1) - it always require the same amount of space on stack. Complexity is O(N) - one number must be reduced to 0.

On the other hand, the alternative implementation requires in this example 6 steps in order to arrive with the answer.Moreover, the number of information that is required to be stored at each step varies - it’s proportional to the size of the number we add. That means that this function computes in O(N) space. Computation complexity is O(N) - it has to build the expression which is proportional to the number (N) and then reduce it (which is also proportional to N). 

All these information is stored on the stack - which has a limited capacity. First implementation we can easily call

    > add 999999 1;;
    val it : int = 1000000

However, our alternative implementation cannot cope with such large number.

    > add' 999999 1;;

Process is terminated due to StackOverflowException.

To put is simply, the expression we were building filled all the available space before it was fully constructed and then evaluated. 

We wanted to mention this this concept early in the course because recursive functions are very common in functional programming. When we design our functions it is good to take all factors into account. It will become more apparent when we get to functional data structures and talk more about processing lists.

One implication of that code we can notice right away. The first function evaluates intermediate steps right away. This technique is called eager evaluation. On the other hand add’ delays evaluation till the very end. This is called lazy evaluation. 

We will talk more about eager and lazy evaluation later in the course and discuss their advantages and drawbacks. Now we can see one practical implication - debugging. If we would like to debug add function we wouldn’t have any problems. On the other hand, add’ is impossible to inspect - expression it builds only gets evaluated at the end of the process. The function might have arguments set to 0 and 3, however, debugger is unaware that it’s part of the larger expression.

One can conclude that add is clearly better than add’ - why we would even bother with add’ implementation? 

In this particular instance yes. However, there are situations in which delaying evaluation is advantageous. It may brings performance gains at the price of memory consumption. Sometimes we will be happy to accept this trade-off. 