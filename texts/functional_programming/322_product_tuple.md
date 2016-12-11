#Product (tuples)

In mathematical sense product is something and something. For instance point on a plane is represented by coordinate x and coordinate y. This x and y is often represented as a pair - (x, y). For example (1, 2). If we would have 3-dimensional hyperplane if would be (x, y, z), e.g. (1, 2, 3).

This is the notion of a product - 2, 3, or more values bundled together. Notation is very simple

```fsharp
let a = (1, 2, 3)
```

Type of a is int * int * int - pretty obvious. This data structure is called tuple.  Tuple can hold values of any type. 

Tuples however should be used with caution. It is sensible to apply them to represent things that only makes sense together - like coordinates of a point.

The drawback is that its building types cannot be named - they are not like properties. Therefore, we always have to refer to them by their order - first, second, etc. So, you can easily imagine that creating tuples that bound together more than 3 types can lead to a confusion of what is what. 

F# supplies few functions to extract values:

```fsharp
let a = (1, 2)
let ax = fst a 
let ay = snd a
```

They are only applicable to 2-element tuples. 

We can also use match expression:

```fsharp
let a = (1, 2) 
match a with 
| (3, _) -> "a"
| (x, y) when x = 2 * y -> "b"
| _ -> "c"
```

In just few examples we can already see how relevant the concept of data abstraction is. Data is completely decoupled from operations. 

Next: [Creating custom types](323_creating_custom_types.md)