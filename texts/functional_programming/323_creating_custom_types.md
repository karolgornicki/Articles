#Creating custom types

When we talked about data abstraction or discriminated unions we actually created our own type. We accomplished that by using “type” keyword. 

For example we defined rational number as 

```fsharp
    type RationalNumber (x:int, y:int) =
        member val Numerator = x
        member val Denominator = y
```

This type (like class in C#) can be instanciated

```fsharp
    let x = new RationalNumber(1, 2)
```

Type is equivalent to C# class. Its structure can be decomposed as

```fsharp
    Type [Name] [Args-for-constructor] = 
        ...
```

Constructor values can be used to assign values to properties. The keyword “member” indicates that it is visible - kind of like public in C#. “val” says it’s a value type, thus, immutable. Think of it as a read-only property. 

F# is a hybrid language, which means, it allows you to add mutable properties as well as instance methods. Whether you should use this techniques is at least questionable. I’d say do what’s practical - however think twice about alternatives as it may make your data tightly coupled to operations.

If you have to introduce a mutable property ask yourself this - couldn’t you create this object with the right value from the beginning. And if you do have to update something - wouldn’t it be better to create a new instance with the correct value? 

Example:

```fsharp
    type Car (n:string, m:float) =
        member val Name = n
        member this.Mileage = m
        member this.ToString() =
            sprintf "%s - mileage = %f" this.Name this.Mileage
```

Name is read-only property, Mileage is a property that you can update, and we also have an instance method ToString(). Sprintf is like String.Format in C#, only better.

Type keyword can also be used to abbreviate types. What would you use it? - for brevity. Sometimes it just reads better when you have a simple type that everyone gets. For example, 

```fsharp
    Type Point2D = int * int
```