#Sequences

Sequences are quite similar to lists. The main differences are:

* Sequences are lazily evaluated, whereas lists are eagerly.
* Sequences don’t support pattern matching, whereas lists do. 

Sequences however support crucial aspect when it comes to interoperability (exchange of information) between other .NET applications - sequences implement IEnumerable<T> interface. Which means that if any DLL written in C# or VB has IEnumerable type it is automatically seen from F# perspective as sequence.