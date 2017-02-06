#Largest Product (function composition) 

In this essay we will solve a problem of finding the largest product in a matrix to which a solution is a great example of function composition. It demonstrates how it can be used and what are its benefits. This puzzle originally featured on Project Euler, which we would encourage you to check for more challenging puzzles - especially when you’re learning a new language. 

##The problem

We are given a matrix, 20 x 20, populated with integer values. The goal is to find the largest product in that matrix. By product we understand multiplication of 4 consecutive integers, either horizontally, vertically or diagonally. 

```
08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48
```

Original description: [LINK](https://projecteuler.net/problem=11)

##Solution - intuitive approach

The puzzle is quite simple, and if you are coming from C# background you probably already in your head have a solution. You are probably thinking about 2 “for” loops to iterate through the grid, and at each point we are calculating product of this and the next 3 elements horizontally, vertically, and on 2 diagonals. Obviously we will have to add few “if” statements to make sure we won’t try to access outside of our arrays. And we’ll keep track of the largest product so far in some variable, which value will mutate over the lifetime of our program. 

This solution will work but it has drawbacks, namely:

* We access elements of the array using indices, which makes code, over time, difficult to understand.
* We add “if” guards to make sure we’re not attempting to access indices outside of arrays. This adds complexity to our code.
* We mutate variable which stores the largest product. In a simple example like this it’s relatively easy to manage, but generally speaking mutable variables are source of complexity. Very often you are forced to debug the program in order to truly understand what it’s doing only because its internal state is changing so often. 

##Solution - functional approach 

We can approach this puzzle employing principles of functional programming and design solution which is readable and free of any “if” guards. To people who are just starting with functional programming it might look a bit strange at first, but that’s only because we’re trying to solve a problem in a new and somewhat different way.

Let’s start with data representation. It’s typical in any functional language to represent matrices as lists of list - single list represents a row. List of lists makes a matrix. Using lists is convenient for processing. In our case that would be

```fsharp
let data = [[08; 02; 22; 97; 38; 15; 00; 40; 00; 75; 04; 05; 07; 78; 52; 12; 50; 77; 91; 08];[49; 49; 99; 40; 17; 81; 18; 57; 60; 87; 17; 40; 98; 43; 69; 48; 04; 56; 62; 00];[81; 49; 31; 73; 55; 79; 14; 29; 93; 71; 40; 67; 53; 88; 30; 03; 49; 13; 36; 65];[52; 70; 95; 23; 04; 60; 11; 42; 69; 24; 68; 56; 01; 32; 56; 71; 37; 02; 36; 91];[22; 31; 16; 71; 51; 67; 63; 89; 41; 92; 36; 54; 22; 40; 40; 28; 66; 33; 13; 80];[24; 47; 32; 60; 99; 03; 45; 02; 44; 75; 33; 53; 78; 36; 84; 20; 35; 17; 12; 50];[32; 98; 81; 28; 64; 23; 67; 10; 26; 38; 40; 67; 59; 54; 70; 66; 18; 38; 64; 70];[67; 26; 20; 68; 02; 62; 12; 20; 95; 63; 94; 39; 63; 08; 40; 91; 66; 49; 94; 21];[24; 55; 58; 05; 66; 73; 99; 26; 97; 17; 78; 78; 96; 83; 14; 88; 34; 89; 63; 72];[21; 36; 23; 09; 75; 00; 76; 44; 20; 45; 35; 14; 00; 61; 33; 97; 34; 31; 33; 95];[78; 17; 53; 28; 22; 75; 31; 67; 15; 94; 03; 80; 04; 62; 16; 14; 09; 53; 56; 92];[16; 39; 05; 42; 96; 35; 31; 47; 55; 58; 88; 24; 00; 17; 54; 24; 36; 29; 85; 57];[86; 56; 00; 48; 35; 71; 89; 07; 05; 44; 44; 37; 44; 60; 21; 58; 51; 54; 17; 58];[19; 80; 81; 68; 05; 94; 47; 69; 28; 73; 92; 13; 86; 52; 17; 77; 04; 89; 55; 40];[04; 52; 08; 83; 97; 35; 99; 16; 07; 97; 57; 32; 16; 26; 26; 79; 33; 27; 98; 66];[88; 36; 68; 87; 57; 62; 20; 72; 03; 46; 33; 67; 46; 55; 12; 32; 63; 93; 53; 69];[04; 42; 16; 73; 38; 25; 39; 11; 24; 94; 72; 18; 08; 46; 29; 32; 40; 62; 76; 36];[20; 69; 36; 41; 72; 30; 23; 88; 34; 62; 99; 69; 82; 67; 59; 85; 74; 04; 36; 16];[20; 73; 35; 29; 78; 31; 90; 01; 74; 31; 49; 71; 48; 86; 81; 16; 23; 57; 05; 54]]
```

Let’s take a pause and think carefully what we have to do in this puzzle. We have to find the largest product either horizontally, vertically, or in any diagonal. In other words we multiply 4 adjacent numbers, in various orientations. Let’s adopt the following strategy. Let’s first solve a very simple version of the problem, and later we will see how we can accommodate more complex cases.

Let's first calculate horizontal products in a single row. To make things really easy, say our row contains numbers from 1 to 10

```fsharp
let testData = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
```

In that case we expect products to be

```
[24;120; 360; 840; 1680; 3024; 5040]
```

Is is clear that 24 = 1 * 2 * 3 * 4 and 120 = 2 * 3 * 4 * 5. So, we have to create isa list of lists where each sublist contains 4 consecutive numbers, like:

```
[[1; 2; 3; 4]; [2; 3; 4; 5]; [3; 4; 5; 6]; [4; 5; 6; 7]; [5; 6; 7; 8]; [6; 7; 8; 9]; [7; 8; 9; 10]]
```

Then each sublist can be mapped using product function. Thus, the next question is how we can produce this list of lists. We can observe that those sublists can be re-arranged:

```
[1; 2; 3; 4];
[2; 3; 4; 5];
[3; 4; 5; 6];
[4; 5; 6; 7];
[5; 6; 7; 8];
[6; 7; 8; 9];
[7; 8; 9; 10]
```

If we look at columns we can see right away the pattern - the first column is our original list, only transposed. Second column is our first list, only without the first element, etc.

Therefore, we could create a function that produces those 4 lists and then simply transpose them. Let's call this function getGroups. It's definition could be:

```fsharp
let getGroups (xs:list<'a>) :list<list<'a>> =
    let xs1 = drop 1 xs
    let xs2 = drop 1 xs1
    let xs3 = drop 1 xs2
    [xs; xs1; xs2; xs3]
```

Yes, it can be written in a much smarter way, but for now let's keep things as simple as possible. We can later refactor it. We also applied previously mentioned technique - wishful thinking - and used “drop” function. It’s just naturally fits our function.

"drop" function removes number of elements from the list, starting from the head. In fact it’s very similar to List.skip with one distinction - List.skip throws an exception when called List.skip 1 [] - “drop” function on the other hand will return empty list. We are going to skip the implementation of typical utility functions as we want to focus on the core idea of calculating products. Source code of this function and few others is available HERE. 

All we have to do next, it to take result of this function and transpose it. Unfortunately F# doesn’t supply this function either, so we wrote our own implementation.Rather than apply argument to getGroups and then pass its result to transpose we can compose these 2 functions - just like in maths: g(f(x)) ⇔ (g . f) (x) which in F# translates into f >> g

```fsharp
getGroups >> transpose
```

This operation produces list of lists. However, not all sublists have the same length. We are only interested in those having 4 elements, so we can apply a filter

```fsharp
getGroups 
>> transpose
>> List.filter (fun (xs:list<'a>) -> List.length xs = 4)
```

As we will later see this functionality will be reused, so let's bind it with a name. We can be more generic and instead of hardcoding 4 we can expose it as an argument.

```fsharp
let groupByN n =
    getGroups
    >> transpose
    >> List.filter (fun (xs:list<'a>) -> List.length xs = n)
```

Now we have a function. Newcomers to F# might find it strangely looking, so let’s review it. We define a name “groupByN”, using let keyword, with which we bind a function that takes a single argument - n - and returns a new function which is a composition of 3 other functions. Alternatively this function could looks like this.

```fsharp
let groupByN n xs =
    xs
|> getGroups
    |> transpose
    |> List.filter (fun (xs:list<'a>) -> List.length xs = n)
```

The last thing we have to do is to calculate products - we simply map each element using product function (yet another basic function missing from the standard library - so we wrote it ourselves. It’s a simple fold on a list).

```fsharp
let getProducts =
    groupByN 4
    >> List.map product
```

OK, so now we can calculate product in a single row. In our case we've got 20 rows. All we have to do it to map this operation across all 20 rows. 

```fsharp
let getHorizontalProducts =
    List.map getProducts
```

Next, let's calculate vertical products in our matrix. Actually, it's very easy. If we transpose our original matrix - which means that columns now become rows we can use exactly the same function.

```fsharp
let getVerticalProducts =
    Utilities.Functions.transpose >> getHorizontalProducts 
```

This is very important aspect of programming, pretty much in any modern language. Functions are reusable. What functional programming in particular makes very convenient is glueing functions together, and thus creating new functions, with very little syntax. 

All what is left is to calculate diagonal products, and in the end find the largest one.

In order to easily visualize the problem let's assume we have a 9x9 matrix populated with numbers as below.

```
[
    [11; 12; 13; 14; 15; 16; 17; 18; 19];
    [21; 22; 23; 24; 25; 26; 27; 28; 29];
    [31; 32; 33; 34; 35; 36; 37; 38; 39];
    [41; 42; 43; 44; 45; 46; 47; 48; 49];
    ...
]
```

Now let's think to what format we need to transform this data set in order to calculate products on diagonal. For now let’s only focus on a diagonal going from top-left to bottom-right. Our diagonal products should look like [11,22,33,44],[12,23,34,45], etc. So we could drop the first element from second row, the first 2 elements from 3rd row, and so on. That leads us to

```
[
    [11; 12; 13; 14; 15; 16; 17; 18; 19];
    [22; 23; 24; 25; 26; 27; 28; 29];
    [33; 34; 35; 36; 37; 38; 39];
    [44; 45; 46; 47; 48; 49];
    ...
]
```

This looks exactly the same as previous example when we were calculating vertical products. All we have to do is to transform original data into this format and we can apply previously defined functions and we're almost done.

How to implement this? Let's first pair each row with its number, first row with 0, second with 1 and so on. Next, we will drop first N elements from each row, where N is number of that row. Let's call this function shift:

```fsharp
let shift xss =
    zipWith (fun (x, xs) -> drop x xs) [0..List.length xss] xss
```

In order to calculate products on this transformed matrix we can use one of our earlier functions. However, in order to cover all cases we have to evaluate each group of 4 consecutive rows - and for that we are going to use groupByN function. Because of its generic definition we can use it for grouping lists, as well as lists of lists. Therefore, the final calculation is: group rows in groups where each contains 4 consecutive rows, then apply shift function to each row in a group, and then calculate products. Finally, concatenate final results.

```fsharp
let calcDiag1 =
    groupByN 4
    >> List.map shift
    >> List.map getVerticalProducts
    >> List.concat
```

At this point we can make one observation

```
map (f . g) = map f . map g
```

So our simplified function will now look like this

```fsharp
let calcDiag1 =
    groupByN 4
    >> List.map (shift >> getVerticalProducts)
    >> List.concat
```

Since it produces list of lists we apply concat function to flatten it.

How are we going to calculate products on the other diagonal? - it's actually very simple too. All we have to do is to reverse order of elements of each rows - using function from standard library - List.rev - and then calculate vertical products on shifted matrix again:

```fsharp
let calcDiag2 =
    List.map List.rev
    >> calcDiag1
```

Final step is to calculate all horizontal products, all vertical products, and all products on both diagonals. If we have them all aggregated on one list, we call max function on this list and we are done. Rather than calculating each group of products and storing them in separate lists (which in fairness is quite intuitive) we can actually express this in a quite an elegant way. Let’s create a list of all 4 functions calculating products.

```fsharp
let fs = [productsHorizontal; productsVertical; calcDiag1; calcDiag2]
```

We’ve learned that in F# functions are treated as data. Each function, when applied to our matrix (represented as list of lists) produces list of lists of products. So, all we have to do is apply each function to our matrix, flatten the list structure, and find max value. 

```fsharp
let calc xss =
    List.map ((fun f -> f xss) >> List.concat)
    >> List.concat
    >> List.max
```

Lastly, we apply fs to calc to get the final result

```fsharp
let maxProduct = calc data fs 
```

The source code can be found [**HERE**](https://github.com/karolgornicki/Articles/blob/master/src/ProjectEuler). Remember, before you run the script, build the solution first. Code in the script references functions which are defined in a source file.
