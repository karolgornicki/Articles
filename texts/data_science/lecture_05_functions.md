# Lecture 5: Functions

## 1. Flow Control

In R we have following flow control structures:

* if, else 
* for
* while 
* repeat (goes forever, hence use break)
* break (exiting loop)
* next (skips to the next iteration in the loop)
* return (returns value)

Examples

    for (i in 1:10)
      print(i)

    count <- 1
    while (count < 10) {
      print(count)
      count <- count + 3
    }
  
I heard that some folks find repeat as pointless function as it might be non-deterministic. Well, yes, but it has its merits. For example we have a learning algorithm, and we say that is has to stop when the error level goes below some threshold, or after 10,000 epochs. We could set repeat, maintain a counter and if erorr.rate goes below our level, or counter > 10,000 then exit our repeat block.

## 2. Sample functions

Simple function adding 2 numbers. Notice that the last line is a returned value.

    add.two.numbers <- function(x, y) {
      x + y
    }

    add.two.numbers(2, 3)

Simple function filtering odd numbers. %% is modulo division. This is a commonly used pattern. First create a vector which specifies which elements (denoted by their indices) pass your filter. Next filter your original data using this vector.

    filter.odd.numbers <- function(data) {
      use <- data %% 2 == 0
      data[use]
    }

    filter.odd.numbers(1:20)