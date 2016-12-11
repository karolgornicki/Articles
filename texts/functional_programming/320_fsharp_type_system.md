#F# type system

In this segment we’ll discuss in detail how you can create your own types in F#. Obviously, F# belongs to the larger family of .NET ecosystem. Thus, everything that is available in CLI, like libraries developed in C# and VB can be ported to F# and used. However, it doesn’t mean it’s always a good idea.

F# provides us with some primitive types, such as int, single, double, etc. Pretty much the same set as we have in C#. 

F# is a strongly typed language. That has one obvious advantage - compiler can check whether our function has the right type - does what we return match with function declaration? This sounds awfully similar to C#. There is one distinction, F# type system is a lot more powerful than its C# counterpart. 

F# brings in algebraic types:

* Sum (A + B)
* Product (A * B)
