#Structure of programs 

F# programs are made up of modules. Module is a container that aggregates definitions of your functions and types (which are synonymous to data structures). If it help - module is like a static class. The syntax is very simple:

```fsharp
    module <module-name>
```

F# belongs to the .NET family of languages and it also embraces the concept of namespaces (which is good). The syntax is 

```fsharp
    namespace <namespace-name>

    module <module-name> = 
```

Note the "=" after module name when we declare a namespace, it’s necessary. 

Let’s now define a function. Functions are one of the important elements of this language.

```fsharp
    namespace Demo 

    module Functions = 

        let add x y = 
            x + y 
```

In the example above we defined a module Functions, and in that module we defined a function called add. It takes 2 arguments - x and y. In F# we don’t have to wrap arguments with parentheses, and we don’t have to specify their types (F# can infer types). The let keyword means that we are binding 2-argument function to the name "add". Later in the course we will see how this function can be redefined using anonymous functions (which is what F# is doing under the hood). Function body can be seen as a sequence of steps. There is no return keywords - simply the last line of the function produces its return value (in our example we only have a single line, x + y). What you can immediately spot is that there are no curly braces marking the scope of the function. Instead indentations indicate where function begins and where it ends. Please notice that let defining this function is also indented with respect to module definition - for exactly the same purpose. 