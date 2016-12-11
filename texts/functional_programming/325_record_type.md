#Record type

F# also allows you to create structure-like (C# struct) types. In F# they are called records, and F# is pretty cleverly designed. 

```fsharp
type Person = {name:string; age:int}

let p = {name = “Steve”; age = 20}
```

F# is able to deduce that p is a Parson type. This is a good alternative to creating tuples that combine many types. For one, all building blocks have names by which they can be referred to, like p.age.

F# provides means to implement all sorts of concepts from object-oriented world like inheritance, interfaces, cyclical dependencies (horrible idea, but you can really do it) and many others. They don’t really belong to functional paradigm, so we will intentionally skip them. If you’d like to learn more about them I would recommend checking [Expert F# 4.0](https://www.amazon.co.uk/Expert-F-4-0-Don-Syme/dp/1484207416/ref=sr_1_1?ie=UTF8&qid=1481466276&sr=8-1&keywords=f%23+expert) (see [References](refereces.md) for more suggestions).

Next: [Processing data structures](330_processing_data_structures.md)