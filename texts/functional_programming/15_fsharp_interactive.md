#How to use F# Interactive?

In the previous essay we explained how you can organize your code in your library (there’s more to that - it’s just to get you off the ground). F# also provides you with a quick way to prototype code before you put it into your library - by using scripts and F# interactive. 

Let’s open a script file and delete its content we don;t need it for now. When you create a project Visual Studio pre-populates it with some sample code. Let’s create a function add that adds two numbers (in scripts we write quick prototypes to test our code so we don't have to use namespaces and modules). 

```fsharp
let add x y = 
    x + y 
```

Now select the entire function (or just Ctrl + A) and next send this code to F# interactive - Alt + Enter (alternatively right-click on the selected text and choose "Execute In Interactive" from the drop-down list. 

This will open a new section at the bottom of your Visual Studio and prints 

    val add : x:int -> y:int -> int

This is the signature of your function. It means your function was successfully loaded to F# Interactive (this is how F# calls REPL - Read Eval Print Loop that you might be familiar from other languages like Haskell or Clojure). You can send your code to it and it will evaluate it and keep in memory - so you can use immediatelly afterwards.

Let's use our new function and add two numbers. Simply type the name of the function, its arguments split by a space and finish the line with ;; (it tells F# interactive to evaluate) and hit Enter.

    > add 1 2;;

It prints 

    val it : int = 3

We simply applied 1 and 2 to function add. It produces value 3 of type int. 

In fact, we can do the whole lot more in F# interactive. You can define functions, import files and many more. In fact, using F# interactive can make you very productive. We wrote some tips on how to be productive when using F# interactive - LINK.

Next: [Functional progamming in a nutshell](21_functional_programming_in_a_nutshell.md)