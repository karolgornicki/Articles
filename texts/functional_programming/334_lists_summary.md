#Lists - summary

Lists are immutable data structures. If we want to update our collection we can’t just append new element - we have to create a completely new list. Luckily, F# has optimized this operations (and many others) so we don't have to see it as an unnecessary waste of time, or memory. 

So, we talked about the good aspects, but what are the drawbacks of lists? Well, they have a nature of linked-lists. One problem with them is accessing N-th element in a chain - for arrays it’s a constant time operation O(1), whereas in F# it’s O(N). 

It’s also worth noticing that lists are eagerly evaluated. This means that all elements are immediately turned into values. Since, for some processes, that might create a bottleneck and force our program to calculate values that we don’t really care for, F# also embraces the idea of lazy evaluation. Collection which is lazily evaluated in F# is called sequence (seq). 

Next: [Sequences](335_sequences.md)