# Lecture 7: Exploratory analysis

## 1. Introduction

R allows us to plot data on a single, or multiple charts at the same time. There are few techniques how we can create our plots. Basically, R works in a way that first you initialize your plot, and next you append additional information to it. 

If you are reading this file in RStudio, then when we execute a line drawing a plot, it will create it in "Plots" section. So, at a time, you will have only 1 canvas in which plot is drawn. Yes, you can draw few plots in one canvas, and yes, you can later export them to JPG/PNG, PDF or even SVG.

## 2. Read data

Let's try some real example. We will try to find some insighst about people who boarded on Titanic. At first let's read the data, apologies for the recap.

Set working directory and read data to data frame.

    setwd("C:/git/Articles/data/data_sample")
    data.titanic <- read.csv("train.csv")

## 3. Statistical summaries

We can quickly inspect the data frame by calling

    View(data.titanic)

Sometimes we will be working with big data sets, and trying to show the entire collection in grid view is pointless, so here's a handy way to limit your set to the first N rows, and show only them on the grid. Function head takes 2 parameters, data frame and N. To show bottom N rows use "tail" function.

    View(head(data.titanic, 10))

Usually when we start working with some data set we want to understand its structure and uncover some patterns and anomalies. 

There are 2 ways to do it:

* by statistically summarizing data
* by presenting filtered data on charts

There's no right or wrong answer here, it's a matter of personal preferences. 

In order to get a better feel of our data set we can call "summary" function.

    summary(data.titanic)

It shows summarized information about data stored in each column, if they are

* numbers (e.g. Age) we can see median, mean
* categorical (e.g. Sex) we can see how many females/males were on the ship

Yes, some results are pointless, like mean of IDs -- R doesn't know what's ID.

To get another glimpse at our data we can call "str" function. It shows structure of the data frame.

    str(data.titanic)

In order to investigate particular column we can use table function which groups elements by factors. For instance
    
    table(data.titanic$Sex)

We can also investigate Age of passengers. Quick glimpse on the Age values:

    data.titanic$Age

As we can see, some passangers don't have age specified (=NA). "table" function will ignore them. In case of future analyses, sometimes we may want to clear this data, by either injecting some values (average, default 0, create prediction algorithm which will guess missing data - it does happen), which then can be used for further predictions, for example.

    table(data.titanic$Age)

We might also be interested to see proportions, what fraction of passengers were female. To do that we would use "prop.table" function.

    prop.table(table(data.titanic$Sex))

Notice that "prop.table" function requires data in grouped format (for example summarized by "table" function).

## 4. Charting 

As mentioned before, the basic mechanics of charts in R are:

* initialize chart
* append additional information (points, fit lines, legend, etc.)

R allows for multiple charts (I don't know all, so I'm only going to cover ones I used):

* scatterplot
* histogram
* barplot
* boxplot

### 4.1. Barplot

Let's first do 1D charts. Say we want to see distribution of sexes on a bar plot. Bar plot requires us to provide data in grouped format (for instance through "table" function).

    barplot(table(data.titanic$Sex))

### 4.2. Histogram

Let's create a very basic histogram first. NA(s) are ignored.

    hist(data.titanic$Age)

We can see that the most populus groups were people in are group 20-30 and 30-40.

If we would like to have a more  granular view. In this example, we set that x axis will contain 80 units.

    hist(data.titanic$Age, breaks = 80)

In order to make charts more presentable they can be fully customized, with regards to labels, legends, coloring, margins... everything. I have never cared about that, so you have to read about that on the Internet. It's pretty straight-forward.

### 4.3. Boxplot

Quick re-cap what boxplot is, in case you forgot.

* [Example #1](http://www.datavizcatalogue.com/methods/images/anatomy/box_plot.png)
* [Example #2](https://en.wikipedia.org/wiki/Box_plot#/media/File:Boxplot_vs_PDF.svg)

In order to draw one, run this line.

    boxplot(data.titanic$Age)

### 4.4. Scatterplot

Say we want to see whether age of passengers had any correlation to how much they paid for the cruise. To do that, we could create a chart where on X axis we would plot age, and on Y axis price of their tickets.

    plot(data.titanic$Age, data.titanic$Fare)

### 4.5. Appending additional information

In order to demonstrate appending additional information let's say we want to add a line that fits best all those points (to see trend).

To do that we'll use "lm" function (stands for Linear Model). It creates an objective which contains all coefficiencies and other stuff (have a look).

    model <- lm(data.titanic$Age ~ data.titanic$Fare)

Next we add line to the chart by calling "abline", which takes as parameters our linear model. Optionally we set line width (=lwd) to see it better.

    abline(model, lwd = 2)

### 4.6. Alternative way of plotting

As we saw in previous example when drawing a scatterplot, there was some repetition. It's quite annoying, however, there's a way around this. We will use "with" function, which applies a function to a data set.

    with(data.titanic, plot(Age, Fare, pch = 20))
    
We use "pch" argument to change how single point is draw.

Now let's say we want to see, which of those points are "males" and which are "females". To do that we re-draw points on the chart. Remember, function "with" applies function (2nd argument) to data set (1st argument). So, we have to restrict out data set to only passengers of one gender -- to do that we use subset function. To re-draw points we use "points" function, col denotes color for which group.

    with(subset(data.titanic, Sex == "female"), points(Age, Fare, col = "blue"))
    with(subset(data.titanic, Sex == "male"), points(Age, Fare, col = "red"))

At this point we can say that women were more willingly to pay more.

Maybe there's a correlation between high price and chances of survival, or age and ticket price, etc.

### 4.7. Multiple charts

Let's say we want to see 2 chars at the same time. Let's print to barplots -- one for sex distribution, the other for port on which they boarded.

    par(mfrow = c(1, 2))
    with(data.titanic, {
      barplot(table(Sex), main = "Sex Distribution")
      barplot(table(Embarked), main = "Embarkation Distribution")
    })

"par" specifies how canvas is organized. Then we plot what we want.

## 5. Exporting charts.

In RStudio go to the upper ribbon of the application, click on "Plots" and in  the drop down you'll see options to save it as PDF or Image.

Or you can use commands (it will save in working directory):

Create PDF file and keep open stream to it.

    pdf("my_chart.pdf")

Draw plot, which will be injected into PDF.

    par(mfrow = c(1, 2))
    with(data.titanic, {
      barplot(table(Sex), main = "Sex Distribution")
      barplot(table(Embarked), main = "Embarkment Distribution")
    })

Close stream. Otherwise Windows will "think" that RSudio is still using the file.

    dev.off()

Apply the same pattern with JPGs and other formats.

    jpeg("my_chart.jpg")

## 6. Scatterplot Matrices

Usually we visualize the data to see whether some variables are correlated with one another. We can do it manually by creating each chart individually, or we can take a shortcut and use "pairs" function.

Say we want to learn more about iris dataset- it's a collection of 150 observations about some flowers, each observations contains 4 measurements.

    data(iris)

Say we would like to find out how each measure relate to all other measures and whether we can find some patterns. To do that we use the following.

    pairs(iris[1:4], pch = 20)

As you can see, nothing really useful comes out of it. You can create some clusters, but that's about it. 

Luckily, we can color each species with different color. To do that we want to map each observation (species from it) to a color, and store all colors in a vector. So, this vector must have 150 entries.

Initialize vector of characters of length equal to number of rows in out data frame.

    cols <- character(nrow(iris))

At the beginning under each index we have an empty string. We have to override them. We know how to do that from out previous lectures, right?

    cols[iris$Species == "setosa"] <- "blue"
    cols[iris$Species == "versicolor"] <- "green"
    cols[iris$Species == "virginica"] <- "red"

    pairs(iris[1:4], col = cols, pch = 20)

Now we are able to see some patterns. 

When we provide our first argument, iris[1:4], we essentially specify which columns we are interested in.