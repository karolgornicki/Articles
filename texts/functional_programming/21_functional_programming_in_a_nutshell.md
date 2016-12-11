#Functional programming in a nutshell

There is a stigma attached to functional programming which says that it is complex, and often you need a PhD to understand it. Nothing can be further from the truth - the basics of functional programming are as simple as basics of procedural programming. However, learning in itself is never an end goal - it is the application of knowledge that counts. It is the ability to use what you have learned to build something new. And this is the hard part - because it requires practice, which takes time. Moreover, it requires you to think about problems in a slightly different way. 

Functional programming (in its purest form) can be summarized in just few points:

* **Everything is defined as a function** - a function in a mathematical sense. It takes an input value and maps it to an output value. This also implies that there are no flow-control statements like if-else, or while in typical procedural sense. 
* **Data can never change** - once we bind some value to a name it stays like that for the lifetime of the application. There is no concept of variables. 
* **Functions themselves are treated like data too** - they can be passed to functions as arguments, as well as being returned. 

That’s pretty much it. The aim of this series is to illustrate these concepts in details. For the demonstration purpose we will be using F#, however, the same concepts hold for other languages, like Haskell or Clojure.

We will start by defining what a function is, and show how it can be used in practice. Next we will improve our solution by abstracting common patterns. We will cap this series with analysis of few techniques that you can use when designing your functions. 