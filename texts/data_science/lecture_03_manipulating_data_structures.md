# Lecture 3: Manipulating data structures

## 1. Removing missing data

Very often we come across data sets which are incomplete, where we miss data. Let's suppose we have the following vector.

    x <- c(1, 2, NA, 4, 5, NA)

As we can see, some data is missing. If we would like to calculate a mean value, we couldn't because R doesn't know some values.

    mean(x)  # The result is NA

To overcome this limitation we could remove NAs from the vector. Following pattern is often used in this situations.

First we create a vector denoting which elements are passing our filter.

    bad <- is.na(x)

Second, use "bad" vector to filter elements from x.

    x <- x[!bad]

Now we can calculate the mean value, as all elements are properly defined.

    mean(x)

The same approach can be applied to a data frame objects.

## 2. Vectorized Operations

Say we have 2 vectors:

    x <- c(1:4)
    y <- c(10:13)

We can add 2 vectors, which means that x[1] + y[1] and similarly for all other indices.

    x + y 

We can apply a restriction to the vector, and it will create array of bools, each denoting if restriction was met.

    x > 2

We can also perform some matrix operations. Let's create 2 matrices.

    x <- matrix(1:4, 2, 2)
    y <- matrix(rep(10, 4), 2, 2)  # rep is a func which repeats value in vector

We can multiply matrices (element-wise). Not that useful.

    x * y

Proper matrix multiplication.

    x %*% y