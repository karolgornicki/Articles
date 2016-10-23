#Receive updates 

OK, now we'ge got our copy of remote repository. The idea of connecting to remote repository is that we take updates from it, or send updates. Let's first see how we can get any updates. To do that, let's switch to the original repository and make some change and see what happens. 

    $ cd ..
    $ cd test_repo
    $ echo *** >> a.txt
    $ git commit -am "Added *** to a.txt"
    
Commit command with -am parameter is a shortcut for add all and commit with message. Quick look at the log
    
    $ git log --oneline
    
which prints 

    729be61 Added *** to a.txt
    dcee90e Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt
    
Let's now switch back to cloned repo and see how things looks there. 

    $ cd ..
    $ cd test_repo_clone
    $ git status 
    
No change here - it says we are up to date. So let's inspect the log 

    $ git log --oneline 
    
It prints 

    dcee90e Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.tx
    814facd Initial commit - added a.txt
    
Now this doesn't show the change that happened in the remote - why is that? The reason is - Git doesn't automatically take updates. And it's actually quite a good thing - imagine you are working on a new feature and all of a sudden someone made a change that conflicts with yours and you are asked all of a suddent to resolve a conflict. 

Instead you have to specifically ask Git to check if there was anything new added to the remote repository and bring those changes in. It happens in 2 steps 

* fetch updates 
* merge these updates into your files 

So, in order to get the latest updates we have to run fetch command 

    $ git fetch 
    
It prints 

    remote: Counting objects: 4, done.
    remote: Compressing objects: 100% (2/2), done.
    remote: Total 4 (delta 0), reused 0 (delta 0)
    Unpacking objects: 100% (4/4), done.
    From c:\git\test_repo\
       dcee90e..729be61  master     -> origin/master

If we now run the status command 

    $ git status 
    
We get 

    On branch master
    Your branch is behind 'origin/master' by 1 commit, and can be fast-forwarded.
      (use "git pull" to update your local branch)
    nothing to commit, working tree clean
    
What does that exactly mean? When we cloned original repository Git liked our master branch in this (cloned) repository to master branch on remote repository. This is called tracking - we would say that our master branch tracks master branch on remote repository. When we run branch -r command 

    $ git branch -r 
    
We see that Git lists origin/master. So let's checkout this branch. 

    $ git checkout origin/master 
    
It prints 

    Note: checking out 'origin/master'.

    You are in 'detached HEAD' state. You can look around, make experimental
    changes and commit them, and you can discard any commits you make in this
    state without impacting any branches by performing another checkout.

    If you want to create a new branch to retain commits you create, you may
    do so (now or later) by using -b with the checkout command again. Example:

      git checkout -b <new-branch-name>

    HEAD is now at 729be61... Added *** to a.txt
    
It says that we are in detached HEAD state. We will explain what that means in a second (it's actually very important concept to understand) but first let's check log.

    $ git log --oneline --decorate 
    
This prints 

    729be61 (HEAD, origin/master, origin/HEAD) Added *** to a.txt
    dcee90e (origin/feature/4, master) Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt
    
As you can see latest updates are there. So, when we run fetch command Git took updates from remote repository, created objects for them (they are stored in .git folder) and updated heads of remote branches to point on them. The only step left is to merge them with your local branches (updating your local branches to point to those new commit objects) - which is done with merging (or pull command which does these 2 things for you). 

Let's now exaplain what does detached HEAD mean. It happens when we run checkout command with either commit ID or remote branch. After executing this command HEAD pointer is being updated with ID of current commit object (and files in working directory are updated accordingly). However, this commit object is not being referenced by any branch in your repository (remote branch is like a point of reference to know at what state is our remote repository). Therefore if we would make any changes and commit them, this commit wouldn't be tracked by any branch. We could lose that commit the moment we would checkout a new branch - because none of other branches are pointing to that comit object. As message reads, we can create a branch on the fly and then we can commit our changes and be sure there is a reference to them. 

Coming back to our original thread - on order to get these changes integrated into our files (and updated local branches) we can run pull command - and this command is most commonly used. Pull command essentially combines 2 actions, fetching updated from original repository and merging them into your working directory. 

    $ git checkout master 
    $ git pull 
    $ git log --oneline --decorate
    
This prints 

    729be61 (HEAD -> master, origin/master, origin/HEAD) Added *** to a.txt
    dcee90e (origin/feature/4) Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt
    
Now all looks good. 

If there would be any conflicting changes between your repository and updated pulled from the original you would have to resolve them in the same way as we have done earlier. 

Original repository can also add or delete branches - in order to update your remote references when you use pull command apply -p parameter (it will prune unnecessary references).

    $ git pull -p 