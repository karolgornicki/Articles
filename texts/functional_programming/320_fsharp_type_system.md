#F# type system

In this series we’ll discuss in detail existing types in F# and also how you can create your own types. Obviously, F# belongs to the larger family of .NET ecosystem. Thus, everything that is available in CLI, like libraries developed in C# and VB can be ported to F# and used. However, it doesn’t mean it’s always a good idea.

F# provides us with some primitive types, such as int, single, double, etc. Pretty much the same set as we have in C#. 

F# is a strongly typed language. That has one obvious advantage - compiler can check whether our function has the right type - does what we return match with function declaration? This sounds awfully similar to C#. There is one distinction, F# type system is a lot more powerful than its C# counterpart. 

What's so unique about F# is that it introduces algebraic types:

* Sum (A + B)
* Product (A * B)

Let's first discuss them in detail and then we'll move to other types. In this series we'll discuss:

* [Sum (discriminated union)](321_sum_discriminated_union.md)
* [Product (tuple)](322_product_tuple.md)
* [Creating custom types](323_creating_custom_types.md)
* [Option type](324_option_type.md)
* [Record type](325_record_type.md)

Next: [Sum (discriminated union)](321_sum_discriminated_union.md)