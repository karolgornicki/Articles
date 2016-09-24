# Abstract

We present a trivial problem of calculating products in a matrix and finding the largest one. This problem originally featured on Project Euler. Next, we outline a candidate solution in C# which exposes typical drawbacks of straight-forward solution which uses procedural approach. Then, we explain in detail how we can approach the same problem using principles of functional programming. In the end we show how functional approach addresses previously highlighted flaws with procedural technique - namely maintainability and readability of the solution. 

Haskell was our language of choice.

# Definition of the problem

We are given a matrix, 20 x 20, populated with integer values. The goal is to find the largest product in that matrix. By product we understand multiplication of 4 consecutive integers, either horizontally, vertically or diagonally. 

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
    
Original definition of the problem: [Project Euler: Problem #11] (https://projecteuler.net/problem=11)

# Typical solution

Most people with some experience in mainstream languages like C++ or C# will see this as 2D array which needs to be traversed using 2 for loops and each visited element in the matrix will become a pivot to calculate products in all possible directions. 

# Typical solution - analysis 

It's a relatively quick, and very straight-forward solution. However, are there any drawbacks?

Since we're using indices to access data in 2D array we have to pay extra attention to not access index array outside of its boundaries. Therefore, we will have to add some guards - which adds unnecessary complexity to the solution. 

Next, in order to calculate products horizontally, vertically or on a diagonal we will have to write custom function for each type of the product. All of them share some similarities, but, it's hard to abstract them away. 

The last issue that we can highlight is the fact that this solution is using functions that are specific to this problem - we are not reusing any functions that are available in standard libraries, but instead we are writing everything from scratch. Moreover, it's hard to think of another problem in which we would be able to use our new functions.

# Functional approach - objectives

So, the question is what alternative approach we can use that will not only produce correct answer to the problem but also address raised concerns when using procedural approach:
* not using indices 
* not having guards making sure we are not attempting access outside array boundaries
* using generic (= multipurpose) functions that are already available in standard libraries rather than writing everything from scratch

Other thing that we want to keep in mind is code readability. How readable your code is has direct impact on how easy it is to maintain it and extend in the future. 

# Functional approach - step by step 

We'll look at this problem from functional programming perspective, Haskell in particular. The same technique (slightly adjusted) could be applied to F#, Clojure and Scala.

Let's start with data representation. In Haskell we would representat 2D array as list of lists which would have [[Int]] type. Let's say that each sublist represents a row.

We know that operation of getting i-th element from the list in a linear operation, therefore we won't use it. Moreover, this was one of our core issues with procedural solution. Richard Bird calls this a disease in one of his books - indexities. We can represent our data in the following format:

    dss = [[08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08],[49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00],[81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65],[52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91],[22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80],[24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50],[32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70],[67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21],[24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72],[21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95],[78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92],[16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57],[86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58],[19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40],[04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66],[88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69],[04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36],[20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16],[20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54]]
    
Our strategy is following: first we will solve a simple version of a problem and then we will convert more complex examples to use our solution for simple case. In other words we start simple and work our way up to more complex cases.

Let's first calculate horizontal products in a single row. To make things really easy, say our row contains numbers from 1 to 10
    
    [1,2,3,4,5,6,7,8,9,10]
    
In that case we expect products to be
    
    [24,120,360,840,1680,3024,5040]
    
Is is clear that 24 = 1 * 2 * 3 * 4 and 120 = 2 * 3 * 4 * 5. So, what we have to create is a list of lists where each sublist contains 4 consecutive numbers, like:

    [[1,2,3,4],[2,3,4,5],[3,4,5,6],[4,5,6,7],[5,6,7,8],[6,7,8,9],[7,8,9,10]]
    
Then each sublist can be mapped using product function. Thus, the next question is how we can produce this list of lists. We can observe that those sublists can be re-arranged:

    [1,2,3,4],
    [2,3,4,5],
    [3,4,5,6],
    [4,5,6,7],
    [5,6,7,8],
    [6,7,8,9],
    [7,8,9,10]
    
If we look at columns we can see right away the pattern - the first column is our original list, only transposed. Second column is our first list, only without the first element, etc.

Therefore, we could create a function that produces those 4 lists and then simply transposes them. Let's call this function getGroups. It's definition could be:

    getGroups :: [a] -> [[a]]
    getGroups xs = 
        let xs1 = drop 1 xs 
            xs2 = drop 1 xs1
            xs3 = drop 1 xs2
        in [xs, xs1, xs2, xs3]
        
Yes, it can be written in a much smarter way, but for now let's keep things as simple as possible. We can later refactor it.

All we have to do next, it to take result of this function and transpose it.

    transpose . getGroups
    
This operation produces list of lists. However, not all sublists have the same length. We are only interested in those having 4 elements, so we can apply a filter

    filter (\xs -> length xs == 4) . transpose . getGroups
    
As we will later see this functionality will be reused, so let's bind it with a name. We can be more generic and instead of hardcoding 4 we can expose it as an argument.

    groupByN :: Int -> [a] -> [[a]]
    groupByN n = 
        filter (\xs -> length xs == n) . transpose . getGroups

The last thing we have to do is to calculate products - we simply map each element using product function.

    getProducts :: [Int] -> [Int]
    getProducts =
        map product . groupByN 4
        
Quick note: transpose and product are functions which belong to standard library supplied out of the box with Haskell. 

OK, so now we can calculate product in a single row. In our case we've got 20 rows. All we have to do it to map this operation across all 20 rows. 

    getHorizontalProducts :: [[Int]] -> [[Int]]
    getHorizontalProducts = 
        map getProducts

Next, let's calculate vertical products in our matrix. Actually, it's very easy. If we transpose our original matrix - which means that columns now become rows we can use exactly the same function.

    getVerticalProducts :: [[Int]] -> [[Int]]
    getVerticalProducts = 
        getHorizontalProducts . transpose 
        
All what is left is to calculate diagonal products, and in the end find the largest one.

In order to easily visualize the problem let's assume we have a 9x9 matrix populated with numbers as below.

    [
        [11,12,13,14,15,16,17,18,19],
        [21,22,23,24,25,26,27,28,29],
        [31,32,33,34,35,36,37,38,39],
        [41,42,43,44,45,46,47,48,49],
        ...
    ]

Now let's think to what format we need to transform this data set in order to calculate products on diagonal. Our diagonal products should look like [11,22,33,44],[12,23,34,45], etc. So we could remove the first element from second row, the first 2 elements from 3rd row, and so on. That leads us to
    
    [
        [11,12,13,14,15,16,17,18,19],
        [22,23,24,25,26,27,28,29],
        [33,34,35,36,37,38,39],
        [44,45,46,47,48,49],
        ...
    ]

This looks exactly the same as previous example when we were calculating vertical products. All we have to do is to transform original data into this format and we can apply previously defined function and we're almost done.

How to implement this? Let's first pair each row with its number, first row with 0, second with 1 and so on. Next, we will drop first N elements from each row, where N is number of that row. Let's call this function shift:

    shift xss = 
        let f1 = zip [0..] xss
        in map (\(y, ys) -> drop y ys) f1 

This is a very verbose definition. Operations zip and map can be combined togather using zipWith function

    shift xss = 
        zipWith (\x xs -> drop x xs) [0..] xss

We can make 2 more observations: (1) we can drop the xss, and (2) both arguments in lambda expression are applied to drop function, thus this lambda expression can be simplified. Finally we arrive with:

    shift :: [[a]] -> [[a]]
    shift = 
        zipWith drop [0..]

In order to calculate products on this transformed matrix we can use one of our earlier developed functions. However, in order to cover all cases we have to evaluate each group of 4 consecurive rows - and for that we are going to use groupByN function. Because of its generic definition we can use it for grouping lists, as well as lists of lists. Therefore, the final calculation is: group rows in groups where each contains 4 consecutive rows, then apply shift function to each row in a group, and then calculate products. Finally, concatenate final results.
    
    calcDiag1 = 
        concat . map getVerticalProducts . map shift . groupByN 4

At this point we can make one observation 
    
    map (f . g) = map f . map g

So our simplified function will now look like this

    calcDiag1 :: [[Int]] -> [[Int]]
    calcDiag1 = 
        concat . map (getVerticalProducts . shift) . groupByN 4

Since it produces list of lists we apply concat function to flatten it. 

How are we going to calculate products on the other diagonal? - it's actuallyvery simple. All we have to do is to reverse order of elements of each rows - using yet again function from standard library - reverse, and then calculate vertical products on shifted matrix again:

    calcDiag2 :: [[Int]] -> [[Int]]
    calcDiag2 = calcDiag1 . map reverse 
    
Now, all we have to do is to concatenate all lists and get max value.

    maxProduct = maximum (concat (productsHorizontal dss) ++ concat (productsVertical dss) ++ concat calcDiag1 dss ++ concat calcDiag2 dss)

Since this looks pretty ugly we can refactor it right away. This code exhibits few flaws: we apply dss to each function, results of each function are concatenated and at the end we append results from each list to aggregate all results in one list. We can do it in a much smarter way by creating list of functions (in Haskell functions are treated the same way as data), to each applying our matrix and finally aggregate results in one list and search for max value.

List defining how to calculate product in each direction:

    fs :: [[[Int]] -> [[Int]]]
    fs = [productsHorizontal, productsVertical, calcDiag1, calcDiag2]

Function calculating all products and finding max:

    calc xss = 
        maximum . concat . map concat . map (\f -> f xss) 

Which can be reduced to 

    calc :: [[Int]] -> [[[Int]] -> [[Int]]] -> Int
    calc xss = 
        maximum . concat . map (concat . (\f -> f xss))

Lastly, we apply fs to cal to get the final result

    maxProduct = calc dss fs

We can do one more refactoring - tidy up getGroups. We left the function in a pretty messy state, however, we can easily spot that its result looks very similar to what shift function does. Therefore, we can re-define it as

    getGroups :: [a] -> [[a]]
    getGroups = 
        shift . take 4 . repeat 
    
# Summary 

This quick example shows how easy it is to tackle complexity by organizing your solution using composition of functions where each function is responsible for doing single job. Moreover, we largely used functions which were supplied with Haskell.

Full Haskell code can be found [here] (https://github.com/karolgornicki/Articles/blob/master/src/pe_11.hs)

