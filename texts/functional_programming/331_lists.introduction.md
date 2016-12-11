#Lists - introduction

Our first stop is list. F# list type is defined in FSharp.Core assembly (notice that C# lists are defined in System.Collections.generic - these are 2 completely different entities, they only have the same name). 

List represents a chain of elements, all of the same type, of arbitrary length. In other words a collection. However, F# and C# implementation are fundamentally different. 

In C# list is just a form of abstraction on top of an array. That’s why getting Nth value in a list is a constant time operation - O(1). It starts with some fixed length array, initially empty and starts populating it. After adding a new element it updates information which index was last time populated. When the array gets full, it simply creates twice as big array, copies all elements across and continues the same process again. If at some point array is populated only in ¼ it gets resized to the half of its length in order to preserve memory. That’s all.

As you can, the nature of C# list is to constantly update its content.

F# approach is completely different. F# favors immutability. That’s why F# list is designed using discriminated union (for that, and in order to make it easy to process it recursively - more about that in a bit). List of elements of type T can be either

* An empty list - []
* Pair of head and tail - referred as head::tail - where head is the element  (of type T) and tail is a list of remaining elements of type T.

That’s right - that’s a recursive definition. I wanted to start with the hardest bit first as it truly explains the nature of lists. Let’s look at few examples

List that is empty is simply 

```fsharp
    let a1 = []
```

List that has only one element would be a list made up of a head and a tail, where head is our element (say a number 5) and a tail is empty list.

```fsharp
    let s2 = 5::[]
```

F# uses :: operator to join head and tail.

List of 2 elements 5 and 10 would be

```fsharp
    let a3 = 5::(10::[])
```

F# team realized that defining lists in such way would be pretty painful, so it offer some syntactic sugar. We can also define the same list using 

```fsharp
    let a3 = 5::10::[]
```

Or just 

```fsharp
    let a3 = [5; 10]
```

Now. Why would we ever want to define a collection in such a way? One reason - recursive processing. Since we know that list can be of any 2 kinds (head + tail, or empty) we can discriminate against them.

Let’s consider a function summing elements in a list.

```fsharp
    let rec sum (acc:int) (list:list<int>) =
        match list with
        | [] -> acc
        | x::xs -> sum (acc + x) xs
```

And we can call it with 

```
    sum 0 [1; 2; 3]
```

It uses the same technique as when we were approximating sqrt. The only difference is that this time we chop the head of the list and add it to accumulator argument. Once we processed the entire list - meaning we arrived to an empty list, we simply return the value of accumulator. 

It’s a good to rather than calling head and tail by their names to use x::xs notation. It’s clear that x refers to the single element and xs to a collection. As a rule of thumb - list of lists should be named xss. 

It would be very tedious if we would have to implement all functions for processing collections from scratch. F# team did it already for us. They are stored in List, Seq, Array modules just to name few, which are part of FSharp.Core assembly. We are going to present that all should know about. They all share similarities, and once you are familiar with one or two all others become very intuitive to you too. 