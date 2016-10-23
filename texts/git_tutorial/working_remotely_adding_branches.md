#Adding and deleting branches remotely

Let's create a branch in our local repository. Next, let's export this branch to remote repository. 

First, let's see what branches we have 

    $ git branch 

Let's create a new branch 

    $ git branch new-name 
    
This branch doesn't exist in the remote repository yet. We can export it by running push command - it basically promotes whatever we want to remote repoitory. Syntax is following 

    $ git push -u origin new-branch 
    
-u parameter is a shortcut for upstream which will essentially make our local branch to track (link) the newly created branch on remote repository. This way other people can also update this branch and we will be able to fetch their updates too. The last two parameters set where and what we want to push - origin is how our remote repository is referred to in Git; new-branch is the branch we want to push. 

In order to delete a branch on remote run 

    $ git push origin :new-branch 
    
Essentially prefix the name of your branch with a collon. This syntax says to Git push no data into that head on the remote repository. 