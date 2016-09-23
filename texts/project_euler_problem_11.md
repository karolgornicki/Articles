# Abstract

We present a trivial problem of calculating products in a matrix and finding the largest one. This problem originally featured on Project Euler. Next, we outline a candidate solution in C# which exposes typical drawbacks of straight-forward solution which uses procedural apprach. Then, we explain in detail how we can apprach the same problem using principles of functional programming. In the end we show how functional approach addresses previously highlighted flaws with procedural technique - namely maintainability and readability of the solution. 

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

# Functional apprach - objectives

So, the question is what alternative apprach we can use that will not only produce correct answer to the problem but also address raised concerns when using procedural apprach:
* not using indices 
* not having guards making sure we are not attempting access outside array boundaries
* using generic (= multipurpose) functions that are already available in standard libraries rather than writing everything from scratch

Other thing that we want to keep in mind is code readability. How readable our code is has direct impact on how easy it is to maintain it and extend in the future. 

# Functional apprach - step by step 

We'll look at this problem from functional programming perspective, Haskell in particular. The same technique (slightly adjusted) could be applied to F#, Clojure or Scala.

Let's start with data representation. In Haskell we would representat 2D array as list of lists which would have [[Int]] type. Let's say that each sub-list represent a row.

We know that operation of getting i-th element from the list in a linear operation, therefore we won't use it. Moreover, this was one of our cure issues with procedural solution. Richard Bird calls this a disease in one of his books - indexities. We can represent our data in the following format:

    dss = [[08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08],[49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00],[81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65],[52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91],[22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80],[24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50],[32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70],[67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21],[24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72],[21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95],[78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92],[16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57],[86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58],[19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40],[04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66],[88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69],[04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36],[20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16],[20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54]]
    
Our strategy is following: first we will solve a simple version of a problem and then we will convert more complex examples to use our solution for simple case. In other words we start simple and work our way up to more complex cases.

Let's first calculate horizontal products in a single row. To make things really easy, say our row contains numbers from 1 to 10
    
    [1,2,3,4,5,6,7,8,9,10]
    
In that case we expect products to be
    
    [24,120,360,840,1680,3024,5040]
    
Is is clear that 24 = 1 * 2 * 3 * 4 and 120 = 2 * 3 * 4 * 5. So, what we have to create is  alist of lists where each sub-list contains 4 consecutive numbers, like:

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

    getGroups::[Int] -> [[Int]]
    getGroups xs = 
        let xs1 = drop 1 xs 
            xs2 = drop 1 xs1
            xs3 = drop 1 xs2
        in [xs, xs1, xs2, xs3]
        
Yes, it can be written in a much smarter way, but for now let's keep things as simple as possible. We can later refactor it.

All we have to do next, it to take result of this function and transpose it.

    transpose . getGroups
    
This operation produces list of lists. However, not all sub-lists have the sme length. We are only interested in those having 4 elements, so we can apply a filter

    filter (\xs -> length xs == 4) . transpose . getGroups
    
The last thing we have to do is to calculate products - we simply map each element using product function.

    getProducts =
        map product . filter (\xs -> length xs == 4) . transpose . getGroups
        
Quick note: transpose and product are functions which belong to standard library supplied out of the box with Haskell. 

OK, so now we can calculate product in a single row. In our case we've got 20 rows. All we have to do it to map across this operation across all 20 rows. 

    getHorizontalProducts = 
        map getProducts

Next, let's calculate vertical products in our matrix. Actually, it's very easy. If we transpose our original matrix - which means that columns now become rows we can use exactly the same function.

    getVerticalProducts = 
        getHorizontalProducts . transpose 
        
All what is left id to calculate diagonal products, and in the end find the largest one.

In order to easily visualize the problem let's assume we have a 10x10 matrix where each row has numbers from 1 to 10.

    [
        [1,2,3,4,5,6,7,8,9,10],
        [1,2,3,4,5,6,7,8,9,10],
        [1,2,3,4,5,6,7,8,9,10],
        [1,2,3,4,5,6,7,8,9,10]
        ...
    ]

Now let's to what format we need to transform this data set in order to calculate products on diagonal. Our diagonal products should look like [1,2,3,4],[2,3,4,5], etc. So we could remove the first element from second row, the first 2 elements from 3rd row, and so on. That leads us to
    
    [
        [1,2,3,4,5,6,7,8,9,10],
        [2,3,4,5,6,7,8,9,10],
        [3,4,5,6,7,8,9,10],
        [4,5,6,7,8,9,10]
        ...
    ]

This looks exactly the same as previous example when we were calculating vertical products. All we have to do is to transform original data into this formal and we can apply previously defined function and we're almost done.

How to implement this? Let's first pair each row with its number, first row is 0, second is 1 and so on. Next we will drop first n elements from each row, where n is number of that row. Let's call this function shift:

    shift xss = 
        let f1 = zip [0..(length xss - 1)] xss
        in map (\(y, ys) -> drop y ys) f1 

Finally, calculating diagonal products is done by:
    
    calcDiag1 = concat . getVerticalProducts . shift 
    
Since it produces list of lists we apply concat function to flatten it. 

How are we going to calculate products on the other diagonal - very simple. All we have to do it reverse order of rows - using yet again function from standard library - reverse, and then calculate vertical products on shifted matrix:

    calcDiag2 = calcDiag1 . map reverse 
    
Now, all we have to do is to concatenate all lists and get max value.

    maxProduct = maximum (concat (productsHorizontal dss) ++ concat (productsVertical dss) ++ calcDiag1 dss ++ calcDiag2 dss)
    
# Summary 

This quick example show how easy it it to tackle complexity by organizing your solution using composition of functions where each function is only doing one thing. Moreover, we largely use functions which were supplied with Haskell.

Full Haskell code can be found [here] (https://github.com/karolgornicki/Articles/blob/master/src/pe_11.hs)
