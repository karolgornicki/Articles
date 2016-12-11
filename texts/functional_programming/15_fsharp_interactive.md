#How to use F# Interactive?

In the above section we explained how you can organize your code in your library (there’s more to that - it’s just to get you off the ground). F# also provides a quick way to prototype your code before you put it into your library - by using scripts and F# interactive. 

Let’s open a script file and delete its content (when you create a project Visual Studio pre-populates it with some sample code. Let’s create a function add that adds two numbers (this time without a namespace and module). 

```fsharp
    let add x y = 
        x + y 
```

Now select the entire function (or just Ctrl + A) and next send this code to F# interactive - Alt + Enter (alternatively right-click on the selected text and choose Execute In Interactive from the drop-down list. 

This will open a new section at the bottom of your Visual Studio and prints 

    val add : x:int -> y:int -> int

This is the signature of your function. It means your function was successfully loaded to F# Interactive (this is how F# calls REPL - Read Eval Print Loop that you might be familiar from other languages like Haskell or Clojure). It allows you to test your functions.

You can now type 

    > add 1 2;;

It prints 

    val it : int = 3

We simply applied 1 and 2 to function add. It produces value 3 of type int. 

In fact, we can do the whole lot more in F# interactive. You can define functions, open import files and many more. I wrote some tips on how to be productive when using F# interactive - LINK.