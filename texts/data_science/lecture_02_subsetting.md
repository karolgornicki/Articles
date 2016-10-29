# Lecture 2: Subsetting

## 1. Subsetting

Let's suppose we have a vector of few charactes.

    chars <- c("a", "b", "c", "d", "e")

And we would like to extract only some data from it - we are not interested in the entire collection. For that we will use 3 techniqies:

* [ ] - always returns object of the same class as the original
* [[ ]] - extracts single element from a list or a data frame
* $ - extracts single element from a list or a data frame 

We can return single element. Notice that sequence is indexed starting with 1.

    chars[1]

When you prefix the element with the minus sign it will return the entire vector except for the element you mentioned. So below will return all items, except for the second.

    chars[-2]

We can return a range.

    chars[2:4]

The minus rule also applies to ranges.

    chars[-(2:4)]
    chars[-seq(2, 4)]  # seq function generates sequence of numbers, from-to

We can also filter elements.

    chars[chars > "b"]

The last one requires a bit more explanation. Let's examine what inner clause do. It maps each element of the sequence of boolean values, whether they meet our restriction or not.

    chars > "b"

Next we pass this sequence (of TRUE/FALSE elements) as a restriction to chars and R only prints those which are TRUE.

In order to extract data from matrix we would do the following.

    m <- matrix(1:6, 2, 3)

Get single element.

    m[1, 2]

Get row.

    m[1,]

Get column.

    m[,2]

Let's now crate a list.

    sample.list <- list(foo = 1:5, bar = 12.3)

We can extract the first element from the list in 2 ways.

We see that it shows first element's name - $foo, and also all its elements. That's because it is a list. [] returns object of the same class.

    sample.list[1]

This returns a vector that is sitting on the first index. As this is a vector, it doesn't have a name. Name was associated with a list.

    sample.list[[1]]

We can also extract value associated with the name. Few alternatives:

    sample.list$foo
    sample.list["foo"]
    sample.list[["foo"]]

## 2. Partial matching

It is possible to provide just part of the name and R will figure out which name it relates to and provide the data.

    sample.list$f
    sample.list[["f"]]  # NULL
    sample.list[["f", exact = FALSE]]  # returns data