#Copying a remote repository. 

In this tutorial thus far we were exploring Git using our local reposotory. We can get the path to our repository by running pwd - print working directory command. 

    $ pwd 

Let's create a copy of this repository, but first go the the folder where we want to create our copy. We create a copy (clone) of the repository by referencing its .git folder. The last parameter in clone command is how the new folder is going to be named. 

    $ cd c:\git 
    $ git clone c:\git\test_repo\.git test_repo_clone
    $ ls -l 
    
We can see that a new folder was created, and if we inspect its content we will notice that it looks pretty much the same as our original repository. 

Git created a clone of our original repository - that means we not just have all the files but we also have their entire history and such. This is now a fully functioning repository. In fact, Git did a little more than just that. Besides copying all the files Git also created links (called references) to the original repository from which it cloned all the files. 

    $ cd test_repo_clone
    $ git status 
    
It prints 

    On branch master
    Your branch is up-to-date with 'origin/master'.
    nothing to commit, working tree clean

It reports that out current master branch is up-tp-date with 'origin/master'. This is how git references branches on a remote repository - remote repository is referred to as origin. As origin of this repository. In other words we have a master branch in our local repository which is linked to master branch in a remote repository (the one we copied from) - and they both point to a commit object with the same ID. We could also say that they are in sync. 

Git allows you to see your local branches by running 

    $ git branch 
    
which prints 

    * master
    
And all remote branches 

    $ git branch -r 
    
which prints 

      origin/HEAD -> origin/master
      origin/experiment
      origin/feature/4
      origin/master
    
In the next section we will explain how you can receive updated from remote repository.