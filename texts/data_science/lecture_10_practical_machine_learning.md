# Lecture 10 - Practical Machine Learning

## 1. Introduction

In this lecture we will go through various packages and demonstrate how we can create some prediction models. We will be creating our example based on iris data set which is freely available in R. It's a collection of 4 measurements of some flowers.

## 2. Data

Get the data.

    data("iris")

Briefly inspect the data.

    str(iris)
    View(iris)

As you can see it has 4 measurements and a species. Our job is to identify a species based on those 4 measurements.

## 3. Cater package

### 3.1. Installation

Installation of this package takes a while, and it's a little bit weird. First run this command. It will install the package, and all required dependencies (or at least this is what it should do.)

    install.packages("caret", dependencies = c("Depends", "Suggests"))

It most likely didn't install all dependencies. To test, run this command.

    library(caret)

If you get an error it says which library you are still missing. Add all missing libraries by installing them using the following line.

    install.packages("[name-of-missing-library]")

### 3.2. Creating a Prediction Model

We assume that at this point you have all required libraries installed. Next you need to add references to those libraries to your script.

    library(caret)
    library(kernlab)

Usually we set a seed for random sampling, to make our results replicable.

    set.seed(23234)

First we have to partition our data into 2 sets.

    in.training.data <- createDataPartition(y = iris$Species, p = .8, list = F)
    training <- iris[in.training.data, ]
    testing <- iris[-in.training.data, ]

Then we specify our training function, where as arguments we specify:

* what we want to learn: Species
* what's our data set
* which algorithm we want to use to build our model, here we selected GBM - 

Stochastic Gradient Boosting (purely because it works fast in this case)

    model.gbm <- train(Species ~., data = training, method = "gbm")

When it finishes we can see a summary of trained various models

    model.gbm

Also we can see which parameters are truly influential by calling summary function on our model.

    summary(model.gbm)

We can also inspect the best model.

    model.gbm$finalModel

Now we would like to know how well we did - first we have to generate predictions for our test data.

    predictions <- predict(model.gbm, newdata = testing)

And next we would like to see how good they are. To do that we can use confusion matrix.

    confusionMatrix(predictions, testing$Species)

Few words of explanations. Confusion matrix is showing how many of members of a population were correctly identified, and how many were mislabelled. So it's a notion of "true positives", "true negatives", "false positives" and "false negatives". So, if columns are actual classes (+, -) and columns are predictions (+, -) this matrix would look like this:

       |  +  |  -  
    ---------------
     + | TP  |  FP
     - | FN  |  TN

Some of commonly used matrices are:

* Sensitivity: TN/(TP+FN)
* Specifity: TN / (FP + TN)
* Positive Predictive Value: TP / (TP + FP)
* Negative Predictive Value: TN / (FN + tn)
* Accuracy: (TP + TN) / (TP + TN + FP + FN)

### 3.3. Specifying Cross Validation

Train method has argument "trControl" which Controls the computational nuances of the train function. It's a good practice to set it as individual variable and then pass to the train function as argument.

    ctrl <- trainControl(method = "repeatedcv",
                         repeats = 3)

It's also a common practice to break arguments into multiple lines, it makes the code more readable.

    model.gbm.cv <- train(Species ~ .,
                         data = training,
                         method = "gbm",
                         trControl = ctrl)

Check trained model. Here you can read that it actually used Cross Validation as re-sampling method (10 fold, repeated 3 times.)

    model.gbm.cv
    model.gbm.cv$finalModel

Plot confusion matrix.

    predictions.2 <- predict(model.gbm.cv, newdata = testing)
    confusionMatrix(predictions.2, testing$Species)

## 4. Using Neural Networks 

Load the package.

    library(nnet)

Split data into training and testing sets.

    set.seed(23234)
    in.training.data <- createDataPartition(y = iris$Species, p = .8, list = F)
    training <- iris[in.training.data, ]
    testing <- iris[-in.training.data, ]

Train network.

    model.nn <- nnet(Species~., data=training, size=4, decay=0.0001, maxit=500)

Inspect the model.

    summary(model.nn)
    model.nn

Get predictions for our testing data.

    predictions <- predict(model.nn, testing[,1:4], type="class")

And in the format of confusion matrix that we saw earlier.

    confusionMatrix(predictions, testing$Species)

We did a bit better, almost 97% this time.

## 5. Using SVM

Load the library.

    library(kernlab)

Split the data.

    set.seed(23234)
    in.training.data <- createDataPartition(y = iris$Species, p = .8, list = F)
    training <- iris[in.training.data, ]
    testing <- iris[-in.training.data, ]

Train the model.

    model.svm <- ksvm(Species~., data = training)

    summary(model.svm)

Get predictions for our testing data.

    predictions <- predict(model.svm, testing[,1:4], type="response")

And in the format of confusion matrix that we saw earlier.

    confusionMatrix(predictions, testing$Species)

As we can see, we did as well as using GBM method. Just 93% accuracy.