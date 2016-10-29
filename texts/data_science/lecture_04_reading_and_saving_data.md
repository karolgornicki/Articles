# Lecture 4: Reading and saving data

## 1. Functions Overview

In majority of the cases we will be reading data from some external sources, and use R to analyze it. We can use the following functions for reading and saving (indented):

* read.table; read.csv - for tabular data
 * write.table
* readLines - for reading lines from text file 
 * writeLines 
* source - for reading R-code 
 * dump 
* dget
 * dput 
* load - for reading in saved workspace 
 * save 
* unserialize - for single R object in binary form
 * serialize 

## 2. Memory Estimation

R stores all their data in the memory. So when we read data from the file, it not only allocates memory and add data, but also categorizes it. To give us a better intuition let's assume we're reading a CSV which contains 1,500,000 rows, each containing 120 columns. For simplicity, let's just say all data is numeric. The question is, how much memory is required to allocate this volume of data?

Numeric data is stored on 8 bytes, we have 120 columns, 1.5E6 rows. This gives us 1.44E9 bytes. 1 MB = 2^20 bytes, so it equates to 1,373 MB, which is about 1.34 GB.

What's disappointing, in order to read that volume of data, we would usually require twice as much space, as R is also recognizing the type of data.

## 3. Reading text file

It is a good practice to set a working directory first. When you're copying path from Windows, change "\" to "/".

    setwd("C:/git/Articles/data/data_sample")

Establish a connection with the text file.

    con <- file("sample_text.txt")

Read lines from the sample_text.txt

    x <- readLines(con)     # read the entire file 
    x <- readLines(con, 2)  # read only the first 2 lines 

Remember to close connection after you finish reading the file. You can read the file first, save content in a variable(s), close the connection. Afterwards, you can analyze information stored in the variable. It's a good practice to close open connections as quickly as possible.

    close(con)

## 4. Reading CSV file

    train.data <- read.csv("train.csv")

## 5. Saving data

Save data in R format. It creates a structure code.

    dput(train.data, "data.R")

Import previously saved data.

    imported.data <- dget("data.R")

dump and source functions are used to save/read multiple objects.