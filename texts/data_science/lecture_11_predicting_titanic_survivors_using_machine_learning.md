# Lecture 11: Predicting Titanic Survivors Using Machine Learning

## 1. Task

The task is, as previously, to predict who is going to survive Titanic crash and who's not. However, this time we want to use machine learning techniques which were introduced in the previous lectures.

## 2. Solution

### 2.1. Introduction 

Alright. This time we are going to do it properly - by which I mean we are going to split our data into testing and training sets, use cross validation, and ROC (receiver operating characteristics). We will also demonstrate some simple techniques for preparing your data to get the most out of your prediction algorithm. At the end we'll find out which variables (columns) were most influential.

There will be couple of new stuff that we didn't cover previously, so fast your seatbelts!

### 2.2. Set up

Reference relevant packages.

    library(caret)
    library(kernlab)
    library(Amelia) # This is something new. Nice little package written by folks 

from Harvard for analysing missing data.

    library(pROC) # Used for ROC, which is a very handy tool to evaluate a model.

Read the data.

    setwd("C:/git/Articles/data/data_sample")
    titanic.data <- read.csv("train.csv")

### 2.3. Preparing the Data

Look at the data.

    summary(titanic.data)

Some of our data is categorical (e.g. Sex) some continuous (e.g. Age), some simply useless (e.g. ID). 

Typically we will have some data missing. This is the case of Age in our case. We can nicely visualize it using Amelia library. 

    missmap(titanic.data)

As we can see, indeed only in Age category we have some missing data, in all other columns we have provided data.

Why this is problematic? We want to use our historical data to learn from data. If we miss some data... it's kind of difficult to learn from nothing. Usually we can apply following strategies:

* use mean value (as overall it won't distort stats)
* create a model which will try to infer age based on other variables
* remove them from our analyses, although, I don't think it's a good approach 

as some testing or future data may have some missing data, and in that case we won't be able to provide out predictions. 

In the case of our Titanic data we are going to enter mean value instead of NAs.

    titanic.data$Age[is.na(titanic.data$Age)] <- mean(titanic.data$Age[!is.na(titanic.data$Age)])

Or a little shorter. It does exactly the same.

    titanic.data$Age[is.na(titanic.data$Age)] <- mean(titanic.data$Age, na.rm = T)

Another problem that we can foresee with our data are names of passengers. They are quite unique. Well, everyone has different name. However, almost all of them contain titles embedded in their names (e.g. Mr., Mrs.) We could extract those, and have nicely categorized information. Whether it will turn out to be useful - you have to try.

    titanic.data$Title <-   ifelse(grepl("Mrs", titanic.data$Name),"Mrs",
                            ifelse(grepl("Mr", titanic.data$Name),"Mr",
                            ifelse(grepl("Miss", titanic.data$Name),"Miss",
                            ifelse(grepl("Master", titanic.data$Name), "Master",
                            "Others"))))

Quick look at stats. Distribution between sexes, and among survivors.

    prop.table(table(titanic.data$Title, titanic.data$Sex), 1)
    prop.table(table(titanic.data$Title, titanic.data$Survived), 1)

We can notice that among men, Masters were significantly more likely to survive than people with Mr. title.

Let's investigate people categorized as Others.

    titanic.data$Name[titanic.data$Title == "Others"]

They are pretty unique, so let's keep them under Others category.

Most of prediction algorithms only works with numeric data, so we have to convert titles into factors. If you look at summary.

    summary(titanic.data)
    summary(titanic.data$Title)

Title is stored as class character. We have to convert it into factors.

    titanic.data$Title <- as.factor(titanic.data$Title)

The same with Pclass

    titanic.data$Pclass <- as.factor(titanic.data$Pclass)

However, we leave "Survived" unchanged at this point.

We don't need all data, so let's remove columns we don't need any more.

    names(titanic.data)
    titanic.data <- titanic.data[c("Pclass", "Age", "Sex", "Title", "Survived")]

In some columns we store multiple factors like Sex column, contains 2 factors: male and female. For 

    titanic.split.vars <- dummyVars("~.", 
                                    data = titanic.data, 
                                    fullRank = F)

Let's overwrite our previous variable with titanic data, and from now on we are only going to use our processed data.

    titanic.data <- as.data.frame(predict(titanic.split.vars, titanic.data))

Quick glimpse into processed data.

    summary(titanic.data)

It's a very good practice to save names of columns for class which we try to predict as well as predictors, as we're going t use them in few places. It also makes your code loosely coupled with your data, which encourages easy re-usability (or recycling if you wish.)

    outcome.name <- "Survived"
    predictors.names <- names(titanic.data)[names(titanic.data) != outcome.name]

At this point we see that our data are nicely split (columns which were containing categorical data), Age column contains values from range 0-80, and Survived 0s and 1s. Some algorithms are good for both, classification and regression. In order to force our algorithm to do binary classification we must change our Survivors into factors/strings, so it will "know" to performs classification.

    titanic.data$Survived <- ifelse(titanic.data$Survived == 1, 'yes','no')

### 2.4. Training the Model

Now we are done with preparing data for our algorithm. As a next step, we split our data into training and testing data sets.

    set.seed(232332)
    training.idnex <- createDataPartition(y = titanic.data$Survived, 
                                          p = .8,
                                          list = F)

    training  <- titanic.data[ training.idnex, ]
    testing   <- titanic.data[-training.idnex, ]

Set parameters of training - use Cross Validation.

    ctrl <- trainControl(method = 'cv',
                         number = 3, 
                         returnResamp='none', 
                         summaryFunction = twoClassSummary,
                         classProbs = T)


Train our model - use gbm algorithm, set metric to ROC and pre-process data (it's usually a good practice).

    gbm.model <- train(training[,predictors.names],  
                       as.factor(training[,outcome.name]), 
                       method = 'gbm', 
                       trControl = ctrl,  
                       metric = "ROC",
                       preProc = c("center", "scale"))

Let's have a look at our model.

    summary(gbm.model)
    gbm.model

Moreover, we can see which variables were the most influential while our model was created. This shows which variables (columns) we important for making a decision whether a person will survive, or die. 

    plot(varImp(gbm.model, scale = F))

Although this algorithm calculates classes it provides probabilities - what's a probability for this passenger to survive and also to die. Their sum equates to 1. BY default the algorithm applies threshold of .5, and everything above is assumed to be of category survived, and below as dead.

Predict function allows us to get predictions converted into classes. To get them we specify argument type as "raw". 

    predictions <- predict(object=gbm.model, testing[,predictors.names], type='raw')
    confusionMatrix(predictions, testing[,outcome.name])

As we can see our prediction is sort of OK - 79.1%. However, we can improve it. At first let's have a look at raw probabilities.

Get prediction as a probability. To do that set type as "prob".

    predictions <- predict(object=gbm.model, testing[,predictors.names], type='prob')

Quick glimpse. 

    head(predictions)

As we mentioned earlier, default classifier applied threshold at .5. It's not always the best choice - and this clearly isn't an exception. To find out better threshold we can use ROC curve. 

    result.roc.model <-  roc(testing[,outcome.name], predictions$yes)
    plot(result.roc.model, print.thres="best", print.thres.best.method="closest.topleft")
    result.coords.model <- coords(result.roc.model, 
                                  "best", 
                                  best.method="closest.topleft",
                                  ret=c("threshold", "accuracy"))

    result.coords.model

We didn't gain much, but with threshold set at 0.4185114 we get slightly better accuracy 80.22% (increase by .9%)

Also if the area under the ROC is above .8 that's generally a good prediction model - although it depends of the field. In some disciplines it's better to predict false positives than true negatives, and in some businesses emphasis on high sensitivity while low specificity might be of most importance. Think of it like that - your system is detecting intruders in some bank. It's better to deny access to your employee by accident, than to accidentally grant access some random guy. Or similarly with delivering bad news about contagious disease. 

### 2.5. Training glmnet model 

Revert back to 1/0 values for Survived column. 

    titanic.data$Survived <- ifelse(titanic.data$Survived == 'yes', 1, 0)

Split data.

    set.seed(232332)
    training.idnex <- createDataPartition(y = titanic.data$Survived, 
                                          p = .8, 
                                          list = F)

    training  <- titanic.data[ training.idnex, ]
    testing   <- titanic.data[-training.idnex, ]

Train

    ctrl <- trainControl(method='cv', number=3, returnResamp='none')
    glmnet.model <- train(training[,predictors.names], 
                      training[,outcome.name], 
                      method = 'glmnet', 
                      trControl = ctrl)

See which variables are the most influential for each class.

    plot(varImp(glmnet.model, scale = F))

    predictions <- predict(object=gbm.model, testing[,predictors.names])
    confusionMatrix(predictions, ifelse(testing[,outcome.name] == 1, 'yes', 'no'))

Using this method we are a bit more accurate, 82.02%.