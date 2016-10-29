# Lecture 9 - Introduction to Machine Learning

## 1. Introduction 

In the previous lectures we learned about basic mechanics of R. Knowledge in itself is almost never a goal, but the application of knowledge. In this lecture we will provide overview of machine learning concept and in subsequent lectures we will show how R can be used to build a model predicting who will survive Titanic disaster, and maybe some other stuff too.

## 2. What's a Prediction?

To kick things off, first, we have to ask a very basic question - what's a prediction? A prediction is a guess which is basing its rationale on patterns discovered in historical data. In layman terms - we have historical data, and we identify some patterns. Then we get some new data that we have never seen before and we expect that the same pattern will exist there too. We apply our previously identified pattern to new data, and derive predictions on that basis. 

For instance we discovered in our historical records that people over 65 are 75% likely to get heart attack in the next 10 years. When we get some new data (new person - John who's 71) - we can make a prediction that he will get a heart attack in the next 10 years too.

Interesting prediction, scientist are predicting, that 50% of people alive today will suffer from cancer in their lifetime. It's purely because we live longer, and our immune system weakens as we get older.

Although we didn't get far from the start we already managed to hit one of the biggest challenges in this domain. Given historical data we assume that the same pattern will exist in the future. "Assume" is not good enough in this case. There is a mathematical model - called VC theory (from initials of its authors, Vapnik and Chervonenkis.) This theory explains what, from  mathematical standpoint, it means "to learn" and builds a foundation for the entire statistical machine learning domain. In here we are only going to scratch the surface. For more details check Caltech course (see references.)

In order to be sure that discovered pattern exists in future data, we are going to validate our model each time. How we validate is largely dictated by the VC theory. You can think of it in this way - adhering to the theory is like a warranty. If you follow basic steps/rules of thumb you can be sure that what you learned from your data (=discovered patterns) is valid.

## 3. Example of a Model

To get a better intuition let's investigate a very simple example. Back in the days, banks used to manually process credit requests. People used to file application in which they were stating their age, obtained education, current salary, years in residence, outstanding debt and many more. Next, bank clerk was reading it, evaluation, and giving his decision - whether to extend a credit to that customer or not.

Our job is to write a program which can do clerk's job automatically - it reads the application and makes the decision - whether to extend the credit or deny. Moreover, it should do this job as good as human clerk. 

We know, that bank has already processed number of applications over the years, and we could use them in order to learn (identify historical patterns.)

As you can imagine applications which bank processed in the past are just a fraction of all possible applications that people can write. The question is - if we identify some pattern in our historical applications, what are the chances that this pattern holds in future applications (the ones we haven't seen yet.) This question is answered by Hoeffding's inequality (which leads to VC theory.) But, we are not going to dig any deeper here. 

Instead, think of it in this way. We have our 10,000 applications. For each application we have the decision that was made (to extend or deny a credit line.) We are going to split our data (10,000 applications) into 2 groups:

* training data (80%)
* testing data  (20%)

The proportions (80-20) are derived from mathematical formulas, but in most of the cases the 80-20 split can be applied as a rule of thumb and it gives a good generalization. Generalization means that pattern(s) identified in seen data will be present in the unseen (new) data too.

Now, we split data for a purpose. In order to learn patterns we are going to use the training data only. We don't snoop onto the testing data at any point. The purpose is to simulate realistic learning environment as much as possible. After identifying the pattern (=learning) we want to find out how in realistic scenario this pattern will work. This is the reason why we left out the testing data (20% of applications) outside learning. For each application in this set we know the final outcome - extended or denied credit line. We can use our model (learned on 80% of remaining applications) and try to guess whether the credit, according to out model, should be extended or denied. Next we compare with actual decisions that were made to see how well our model approximates decisions made by human-clerks. This is called model evaluation (some also call it validation.)

We jumped straight to the models - just to be sure we're on the same page - a simple model could be something like if the applicant is in their 20s, doesn't have outstanding debt and worked for at least 5 years, extend credit, otherwise deny. As you can imagine, there could be more complicated rules - this is purely for illustration purpose. 

### 3.1. When We Should Apply Machine Learning?

Paragraph above describes very simplistic model. In some cases this kind of rules are well defined - there's no point of using machine learning techniques to uncover this patterns as they are already known. We only want to use this techniques to discover patterns which we are not able to pin down mathematically. 

### 3.2. Prerequisites For Machine Learning?

Next question we have to ask ourselves is - when machine learning is really applicable? Can every pattern be discovered by those algorithms - NO. Machine learning relies on learning from data, so if we don't have historical data we are out of business at the very beginning (also when we have small volume.)

Next we have to ask ourselves - whether the pattern exists in the data. Say we have data of someone flipping the coin 10,000 times and recording whether they flipped head or tail each time. If we'll try to predict what are they going to flip next - we (our algorithm) might discover some patterns but are they realistic? Hopefully this will be caught during our model evaluation.

And at the end, we are not able to pin the pattern down mathematically. Otherwise why bother, right?

To recap:

* we have data
* the pattern exists
* we cannot pin it down mathematically

## 4. Significance of the 80-20 Rule

One could ask - surely you've got some good algorithms, why evaluate them in this case. Someone checked them before publishing, right?

The algorithm might be good, but its results (identified patterns) rely on the data, as pattern is inherently in the data - the method is only used to discover it. If you have a rubbish data - you will get a rubbish (=useless) pattern. And this is the point when you want to know - whether what you just learned has a value.

Another angle to look at this problem is a bit more extreme. Let's say that our model sort of memorized all learning data - it's like you would memorize all the answers from the textbook without knowing how to calculate them. When you're checking your results with answers at the back of the book you're getting everything perfectly - well, you memorized it perfectly. However, when you arrive in the exam - it's not so great any more. Pretty much the same can happen in machine learning.

## 5. Overfitting

Above example (the extreme case) is closely related to overfitting (you will here it a lot in machine learning.) To give you a better intuition what it is let's say we have group of people which are characterized by their age and the average volume of milk they consume. (20yo, 1galon), (34yo, .2g) and so on. We can plot this data on the scatter plot, and next we can provide best fit line. If we are going to use a linear model there will be some errors (usually we would apply mean squared error in this case.) However, if we would use higher order polynomial to fit the data we would get better error rate (=lower). 

On a side note - this is the main purpose of machine learning. You create an error function (mean of squared errors from every point, for example) and then the job of the algorithm is to minimize its value by tweaking parameters of the model. When you hit error level that is acceptable to you, you get your model and report its all parameters as your trained model - ready to be evaluated on a testing data.

You can think of it as calculating gradient of error function over each parameter and equating it to zero, and eventually finding minimum of hyperplane. If it doesn't make any sense to you, just ignore this paragraph.

Coming back to best fit function - linear model might not be as good as 55-order polynomial - judging by the error it generates. However, there's a catch. Data is rarely clean - it's almost always noisy. If you use high order polynomial you most likely learning the noise rather than the general pattern that occurs in your data. 

If you think you don't have noisy data... you're the luckiest person on Earth, or you better think some more.

To summarize it, overfitting model generalize poorly.

The obvious questions one can ask - how can I know if I'm overfitting, and is there a way to prevent this from happening? The answer is regularization.

## 6. Regularization

Overfitting, by and large, happens because we use model which is too complex for our data. Basically, the job of regularization is to reduce complexity of the model - rather than using 8-order polynomial, it encourages to use lower order, say 3.

We can achieve this in 2 ways.

* by reducing number of independent variables in our training data set,
* by applying various smoothers in our models.

### 6.1. Reducing variables

The more variables we have (columns in our training data set when we look at it as a grid) the more challenging it becomes to fit them well. Sometimes it might be the case, that with fewer variables we are able to create a model which has lower complexity and fits data at acceptable rate.

This is also called removing VC dimensions, which is derived from VC theory mentioned earlier. One reason you would like to do it, is the more variables you use, the more training data you need to have in order to be sure that whatever model you created is generalizing - creating any model from 10,000 variables is easy. The problem is finding enough data which will guarantee you that whatever you learned has any value. To make it more concrete example - from 10,000 variables you derived model which predicts likelihood of getting heart attack. However, your predicted probability has error margin - in this case it's for example 44%. So when you predicting that someone will get heart attack with 56%, what you actually say, they will get heart attack with 52% +/- 44% which gives you a range of 8% - 96% where your actual likelihood lies. Yup, you are right - this prediction is utterly useless, as it pretty much says that everything can happen.

### 6.2. Applying Smoothing Techniques

In layman terms there are number of techniques, depending on your learning algorithm which can "discourage" your model from getting too complex. If you think of hyperplane created by high order polynomial - you could observe loads of local minimums/maximums. Smoothing aims to reduce those spikes - smooth them out so they are closer to the general surface of your hyperplane.

There are many regularizors available, just to name few:

* Tikhonov regularization
* LASSO (which stands for least absolute shrinkage and selection operator)

They are used in different situations, and their aim is to increase generalization by parameters shrinkage, and some also variable selection. In other words it makes some variables contribute less to final prediction, and in some techniques can also remove non-influential variables.

## 7. Cross Validation

Suppose you have data of some patients, and you would like to predict what's the likelihood they will die in the next 5 years. You've got plenty of variables - their age, sex, medical history, etc. Your job is to find a pattern and report how accurate it is. 

As you know there are plenty of algorithms which can learn from data (e.g. decision trees, neural networks, even perceptron.) 

Each algorithm can identify different pattern, and you would only like to use the pattern which gives you the best predictions. So at best, you would like to run all the algorithms, and evaluate all learned models, compare them with each other and report only the one which has the highest accuracy (we'll talk about measurements in the next lecture.) 

According to what we have learned to far, we would split our data into 2 sets: training set and testing set. Then we would use training set to generate a model using first algorithm, evaluate using testing set. Next we would do the same with 2nd, 3rd and all other algorithms. Do you see any problem with that?

The problem is that our testing set became part of our learning model, because it was used to evaluate all models. In other words - we trained many models and in the end selected one which works best for out testing data. So testing data not only provides the information how good it is, but also guides our final decision (and that's wrong!) Well, not categorically wrong, but you provide an optimistic accuracy of your model - which is not really something  you should do.

To overcome this, we use cross validation. How does it work? At first we split our data (at random) into:

* testing set   (20%)
* training set  (80%)

From now on, we are only going to play with testing set. We take our training set and split it, at random, into 2 subsets: 

* validation    (20% of all the data)
* (sub)training (60% of all the data)

We use our first algorithm to learn (train the model) from those 60%. Next, we evaluate the model on validation set (20%). We write down how well it did.

Next we take again our testing set (80%) and again split, at random, into validation set (20%) and subtesting (60%). On the new subtesting set we run our 2nd algorithm, which trains another model, then we evaluate this model against validation set (20%), and save its performance result.

We do that for all other algorithms we want to run. At the end, we pick the one which performed best, and run this model against testing data (20%) which hasn't been used so far in our experiment. It gives us some accuracy, and this is something we report. We provide this model, but we say it's accurate and here we provide what we measured against testing data. 

In this way testing data is only used once, and doesn't guide selection of the model. To look at it from the other angle - you're trying to simulate realistic learning process, and you want to know how well you are learning, not just report a high accuracy.

The above scenario is a bit simplistic view, sometimes, you can run the same model multiple times on different validation sets, and average errors. You could write so much more about on this topic.

### 7.1. Other Techniques

Cross validation is not the only approach. As we alluded in the previous section this subject was heavily researched over the last years, and people came up with number of methods. Usually each method comes with its own trade-offs and they should guide your final decision, which one you should use.

For instance another widely used method is K-fold validation. However it comes at a price. You as a user have to decide which K you would like to use. This decision has following impact on your model:

* larger K leads to smaller bias and larger variance 
* smaller K leads to larger bias and smaller variance 

## 8. Closing Remarks

I feel like I'm committing a crime here by not introducing any maths. For more comprehensive introduction to machine learning please check Caltech course mentioned in references. 

If you want to be serious about Data Science, I think it's pretty important to have a very good understanding of the basics of machine learning, as it will give you more confidence in your work. 

Also, very often we are not only interested in getting a prediction as "yes" or "no". Sometimes business will demand that our analyses are easily interpretable and maybe even scalable. 

Interpretable analyses - when we provide a prediction that this patient will die, or not - our client who requested this analyses would like to know why this patient is expected to die because maybe there's something we can do extra to change it. And this is something where knowledge about different algorithms (also how they work) comes at play - some will be easy to interpret like decision tree, some a bit harder like neural networks or SVMs (although I believe you can still do it, it's just harder.)

Scalability is about addressing problem of large volume of data. If you have inefficient algorithms it will become very slow when you drastically increase the volume of training data. This is very often the case of regularization algorithms, which rely on quadratic programming.

In the next lecture we'll do some practical machine learning using caret package.