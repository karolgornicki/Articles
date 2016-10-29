# Lecture 1: Data structures

In R everything is an object. Object is a realization of a class. 
We have following classes for data structures:

* character (i.e. "a")
* numeric (i.e. 3.14)
* integer (i.e. 1L)
* complex (i.e. 1+4i)
* logical (i.e. TRUE)

Some basic objects.

* vectors (they only contains objects of the same class)
* lists (represented as a vector, but can contain objects of different classes)

## 1. Shortcuts 

In order to execute line from the scrip in the console set cursor on the line (or highlight multiple lines) which you want to execute, and press Ctrl + Return

In order to clear the console view press Ctrl + L

## 2. Basic entering data 

Create a variable x and assign to it (<-) value 5. Assignment can be done also with (=). Traditionally (<-) was used, later on (=) was introduced to resemble other programming languages, like C++. It is a good practice to follow Google's style guide and they advocate to use (<-), reference: [https://google-styleguide.googlecode.com/svn/trunk/Rguide.xml](https://google-styleguide.googlecode.com/svn/trunk/Rguide.xml)

    x <- 5

We can print value in 2 ways
    
    x
    print(x)

Both methods produce the exact same output. In the console R prints

    [1] 5
 
R prints results as sequences. In our case (x) we have only one element, and the value in square brackets describes index of first element. When we will be defining sequences explicitly it will make more sense. 

## 3. Creating sequences 

Let's create a sequence of numbers, from 1 to 100. Rather than writing them 1, 2, 3, ... we can use a shortcut

    x <- 1:100

## 4. Creating vectors

We can create vectors by using c function (c for concatenate).

    x <- c(0.5, 0.6)      # creates a numeric vector
    x <- c(T, F)          # creates a bool/logical vector
    x <- c(TRUE, FALSE)   # creates a bool/logical vector
    x <- c(1+4i, 2+7i)    # creates a complex vector

We can also initialize a sort of default, or empty vector. In this instance we have to explicitly specify what kind of objects we will be storing.

    x <- vector("numeric", length = 10)

Note: If we try to create a vector from the sequence of different objects, R won't throw an error, but instead it will apply various castings and most likely contaminate the data.

## 5. Matrices

We can create a matrix by using matrix function. As arguments we have to supply its dimensions. nrow denotes number of rows and ncol number of columns.

    m <- matrix(nrow = 2, ncol = 3)
    m

When we print the matrix we see NAs. NA stands for Not Available and it's one way to describe a missing data. On this note, integers has NaN which is Not a Number.

We can initialize the matrix by using:

    m2 <- matrix(1:6, nrow = 2, ncol = 3)
    m2

As we can see, the matrix is populated by columns.

But when we think if it, matrix is just a vector with 2 dimensions. And this is how R treats them. We can create a vector and then add an attribute describing dimensions to it and R will treat it as a matrix.

Create a vector of numbers from 1 to 6.

    m3 <- 1:6

Add dimension attribute to vector. Dimension is a vector itself of length 2.

    dim(m3) = c(2, 3)
    m3

We can also create matrices by binding columns, or by binding rows. Let's suppose we have 2 vectors.

    vector.one <- c(1:3)
    vector.two <- c(10:12)

We would like to combine them together to create a matrix in which vector.one constitutes the first row, and vector.two the second row. We would do it by:

    m4 <- rbind(vector.one, vector.two)

Alternatively, we can bind them as columns, vector.one will create the first columns, and vector.two the second column.

    m5 <- cbind(vector.one, vector.two)

Matrices are quite important as big part of statistical machine learning can be reduced to inversing, transposing and multiplying matrices. Primary examples are Linear Regression and Support Vector Machine.

## 6. Lists

We can create a list by using list function. We can pass as many attributes as we like.

    sample.list <- list("a", 1, TRUE, 1+2i)
    sample.list

When we print the list and we see that each element is associated with an index. Indices are described in double square brackets [[ ]]. We can think of lists as collection of vectors, where each vector contains only 1 element.

To [ ] and [[ ]] we'll come back later when we talk about extracting subsets.

## 7. Factors

Factors are used to represent categorical data. If you are familiar with C++ or C#, they are sort of like enums - vector of integers where each has a label.

Say we conducted a survey and people provided following answers:

    survey.answers <- c("yes", "no", "no", "yes", "maybe", "yes", "yes")
    survey.factors <- factor(survey.answers)

What does it do - it finds distinct values, and associates each with an integer. We can accomplish the same by using table function. Additionally it will show counts for each category.

    table(survey.factors)
    table(survey.answers)

## 8. Missing Values

Previously we mentioned missing values. Basically we have:

* NA (not available/undefined math formualtion)
* NaN (not a number)

    x <- 5
    y <- NA
    z <- NaN

When we have a variable (with a single value) we can inspect whether it's NA or NaN by applying these functions:

    is.na(x)  # F
    is.na(y)  # T
    is.na(z)  # T

    is.nan(x)  # F
    is.nan(y)  # F
    is.nan(z)  # T

## 9. Data Frames

Probably the most important part of the course. They are used to store tabular data. Think of it as collection of lists, where each list has the same length.

We can create data frame by ourselves. I broke declaration of attributes into multiple lines to make it more readable.

    x <- data.frame(
      name = c("Adam", "Brian", "Celine", "Dave"),
      age = c(1:4), 
      is.friend = c(T, F, T, F))

To view data frame as a grid call View function. Alternatively, you can click on grid icon in Environment section in RStudio.

    View(x)

In order to find out about dimensions of the data frame call.

    nrow(x)
    ncol(x)
    dim(x)

## 10. Names

R objects can have names. How does it work? Let's suppose we have a sequence 1, 2, 3

    x <- 1:3

In order to get names associated with a sequence of elements execute this line

    names(x)

Since it didn't have any names (NULL), we can add few. Let's associate 1 with "first", etc.

    names(x) <- c("first", "second", "last")

Now when we print x we see each element with its name.

    x

It can be useful to get particular element by name, rather their index. We'll talk more about extacting subsets from variables in the next lecture.

    x["last"]
