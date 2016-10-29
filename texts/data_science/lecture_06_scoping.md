# Lecture 6: Scoping (theory)

When you first learning about R this topic isn't crucial to understand, however, in a long run it will definitely save you some time while debugging.

## 1. How does R store variables?

Previously we saw this line

    x <- 5

What we do here, we define a variable "x" and assign value "5" to it. If you are reading this in RStudio, in the Environment section you can see that in Values section "x" and "5" showed up.

R takes a very simplistic approach to variables, and store them as pairs:

* name (e.g. x)
* value (e.g. 5)

If you are familiar with other programming languages you can think that R is maintaining a symbol table (e.g. hashtable) tied with the environment.

When we type in console something like this

    x + 3

It will print the result of this operation, which is 8. But what exactly R is doing here? R inspects variables in the Environment, looking for the name "x," and when it finds it, R uses its value and adds to 3.

## 2. How does R find values for variables?

OK, so what's going to when we run this line?

    x <- c(1, 2)

As we remember from previous lectures, it creates a vector containing 2 elements: 1 and 2. But from R perspective, R reads this line from right to left. When it gets to "c" it looks in the Environment for name "c". R doesn't know it's a function - as far as R is concerned it's some variable (notice, variables can store values like numbers, but also functions - we'll talk about that later.) We didn't declare "c", and in the Environment tab in RStudio you can see variables stored in the Environment - there's no "c" there.

Let's spend a few moments to talk about environments in general. Environment like a namespace, is a space in which you can define various names (variables) and attach values to them. But it would be unthinkable to ask user to define everything. R comes forward, and allows user to import packages - it's like appending additional environments to our own environment. 

Moreover, every environment has a parent environment - the environment where it was created (sort of).

Going back to our example. R inspects "c" - doesn't know what it is, so it looks for "c" in the environment. If it fails to find a match, it loops through all packages that are added to our environment (additional environments). In this case it finds "c" in base package ("base" is the name of the package), and applies corresponding to it value. In this instance, it's a function. 

Hypothetically, if our name "c" wouldn't be find in any packages, R would navigate to parent environment, which in this case would be "Empty Environment" (which doesn't have parent). Empty environment doesn't have anything, so it would fail to find a match and print an error message in console.

In order to find out which environments and packages are being searched call.

    search()

R will look for the first match, so in theory, you could alter the order in this list. I don't know how to do it, but I'm sure Google could help you with that. 

## 3. What does lexical scoping mean?

If you are familiar with languages like C++, or C#, this might be a bit counter-intuitive for you. R uses lexical scoping (lexical scoping is used in Scheme (dialect of LISP), but also in Clojure for "let" command, binding uses dynamic scoping). I think Python also allows lexical clojures, but I would have to check that out.

So, what the lexical scoping is all about? Let's say we have a function which takes a value (maybe number of steps) as an argument and calculates the overall distance. Inside this function we set the value of variable unit, and call a function which does the actual calculation. The code looks like that.

    unit <- 10

    get.distance <- function(steps) {
      unit <- 5
      calculate.distance(steps)
    }

    calculate.distance <- function(s) {
      s * unit
    }

Execute the following line - what's the distance of 10 steps? 

    get.distance(10)

The result is 100. When R get to the point where it executes "calculate.distance" it encounters "unit" variable. At first it looks in its local environment, which is within this function. "unit" is not declared there to it goes it its parent environment. But parent environment is not the function in which the function was called, but in which it was declared. In this instance, it is Global Environment. This is the key feature of lexical  scope - it binds things where they were defined, not where they are called (this is called dynamic scoping.) That's why unit gets value 10.

If your plan was to override the default value of "unit" with 5 and use it to calculate the final distance you can apply 2 strategies.

**Strategy 1** - pass unit as an argument to "calculate.distance" function.

    unit <- 10

    get.distance <- function(steps) {
      unit <- 5
      calculate.distance(steps, unit)
    }

    calculate.distance <- function(s, u) {
      s * u
    }

    get.distance(10)

**Strategy 2** - declare function "calculate.distance" within "get.distance"

    unit <- 10

    get.distance <- function(steps) {
      unit <- 5
      calculate.distance <- function(s) {
        s * unit
      }
      calculate.distance(steps)
    }

    get.distance(10)

On a side note - each function should be responsible for doing one, and only one thing, and in respect to that the above desing is not good. It was created purely to make the point, and demonstrate how scoping works.

## 4. Practical application of lexical scoping

Examples provided in the above section may seem to be detached from the reality to some, so I though I can give you a better example which showcase this feature.

Let's say that in our code we're doing a lot of raising to the power x. In some cases we raise to the power 2, in the next iteration to the power 3, etc. We could create the following function which will save the power value and return another function which accepts the number we want to apply power operation to.

    raise.to.power <- function (p) {
      function (n) {
        n ^ p
      }
    }

At this point you can see, that inner function (the one which is returned) "remembers" the value p which was used when it was created, and uses it whenever new function is called. 

By calling this function we get back another function which already know, that whatever argument we will supply, will be raised to the power of 3.

    to.the.power.3 <- raise.to.power(3)

Now we can simply call this function (stored in variable) with argument 4, and it will raise 4 to 3, 4^3 = 64.

    to.the.power.3(4) 

Alternatively, you could do it in one line, but most of the time, you would  write those function with re-usability in mind.

    raise.to.power(3)(4)
