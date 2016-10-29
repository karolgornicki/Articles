# Lecture 8: Creative Naïve Predictions

## 1. Task

The task is to create some naïve prediction model guessing whether passengers of Titanic survived or perished. You should, without problems, achieve 70% with your model (it's really easy.)

At this point we don't have to be concerned about validation techniques. We will do it properly when we get to machine learning.

Below I present 2 candidate solutions.

## 2. Solution #1 (78.68%)

Load the data.

    setwd("C:/git/Articles/data/data_sample")
    train <- read.csv("train.csv")

We know that women and children took precedence during the  rescue procedure. Let's inspect women first. Our data contains sex.

This shows count of each sex.

    table(train$Sex)

Now let's see how many individuals survived in each group.

    table(train$Sex, train$Survived)

And as proportions.

    prop.table(table(train$Sex, train$Survived))

To see relative proportions (per row), add 1 as second argument.

    prop.table(table(train$Sex, train$Survived), 1)

We can see that 74% females survived, whereas only 19% males. We can create a dummy model, predicting that all women survived, and all men perished.

To do that we create a new column in our data frame, prediction. By default everyone died.

    train$Prediction <- 0

Update Prediction column, if female then set as survived (=1)

    train$Prediction[train$Sex == 'female'] <- 1

Let's check how accurate we are.

    train$AssessPrediction <- 0
    train$AssessPrediction[train$Prediction == 1 & train$Survived == 1] <- 1
    train$AssessPrediction[train$Prediction == 0 & train$Survived == 0] <- 1
    prop.table(table(train$AssessPrediction))

## 3. Solutions #2 (80.8%)

Load data.

    setwd("C:/git/Artciles/data/data_sample")
    train <- read.csv("train.csv")

We developed our first model. We used one variable, sex, and assumed that all women will survive whereas all men were going to perish.

In this example let's try to raise the level of complexity by incorporating move variables.

Let's categorize price of tickets.

    train$fare.categroy <- '30+'
    train$fare.categroy[train$Fare < 30 & train$Fare >= 20] <- '20-30'
    train$fare.categroy[train$Fare < 20 & train$Fare >= 10] <- '10-20'
    train$fare.categroy[train$Fare < 10] <- '<10'

Let's have a look whether there's a correlation between the amount people paid for their tickets, class of a cabin and their sex.

    aggregate(Survived ~ fare.categroy + Pclass + Sex, 
              data = train, 
              FUN = function(x) {sum(x)/length(x)})

We could observe that female in 3rd class who paid 20 or more were unlikely to survive. 

    train$Prediction2 <- 0
    train$Prediction2[train$Sex == 'female'] <- 1
    train$Prediction2[train$Sex == 'female' & train$Pclass == 3 & train$Fare >= 20] <- 0

    train$AssessPrediction2 <- 0
    train$AssessPrediction2[train$Prediction2 == 1 & train$Survived == 1] <- 1
    train$AssessPrediction2[train$Prediction2 == 0 & train$Survived == 0] <- 1
    prop.table(table(train$AssessPrediction2))