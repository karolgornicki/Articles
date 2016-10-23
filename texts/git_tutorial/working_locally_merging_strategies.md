# Branches and merging

In the previous section we explained how branches work. Branch is just a pointer to the commit object - nothing more. Because of that creating branches is cheap and quick. In fact, while working with Git you are going to use them - a lot! 

Most repositories are organized in a way that master branch represents code that is stable and was released. If you want to create new features you create a branch. On that branch you develop your new feature. Once you are happy with your new code and everything is tested you integrate your change to the master branch. And this is our first use-case - we are going to create a new branch, add something to it and finally integrate our new stuff with main development stream (master branch).

## Use Case #1 - Fast forward

Let's start by switching to master and creating a new branch 

    $ git checkout master 
    $ git branch feature/1
    
Let's checkout feature/1 branch 

    $ git checkout feature/1 
    
In order to simulate development of a new feature let's add a new line to a.txt.

    $ echo xxx >> a.txt
    
And let's commit this change 
    
    $ git add .
    $ git commit -m "Added feature 1"
    
Quick look on log of our repository 

    $ git log --oneline --decorate 
    
Our repository can be depicted as 

[PICTURE-REPOSITORY]

Let's have a log at the output printout (later we will compare with after the merge).

    $ git log --oneline --decorate 
    
It prints 

    140c07a (HEAD -> feature/1) Added feature 1
    da9e20e (master) Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt

Now, we would like to integrate our feature/1 changes into the master branch. This process is called merging in Git. We are going to merge feature/1 into master. In order to od that, we first have to switch to our master branch (because we want to intergrate changes from feature/1 into master).

    $ git checkout master 
    
And now we want to request to merge feature/1 into master 

    $ git merge feature/1
    
The output of this command is:

    Updating da9e20e..140c07a
    Fast-forward
     a.txt | 1 +
     1 file changed, 1 insertion(+)
    
First we have yo understand how Git is merging. This is how Git appraches a merge scenario. Git looks at the 2 branches - each pointing to a commit. In our case master points to da9e20e and feature/1 points to 140c07a. Since our commit history is essentially a graph (as we have seen earlier) Git finds the first common ancestor for both commits. In this case the first common ancestor for the 2 commits is da9e20e. This clearly says to Git that there was no development on master since we created a new branch and in order to incorporate changes from feature/1 branch all it has to do is update master branch pointer to point to 140c07a. This operation is called fast-forward, and it's exactly what Git reports in the printout after we run merge command. After running this command our repository is depicted below. 

[PICTURE-REPO-AFTER-MERGE]

Now we can delete feature/1 branch (which means that we are going to delete a pointer to the last commit) because it master and feature/1 are now pointing to the same object.

Before we finish this use case let's look at the log printout
    
    $ git log --oneline --decorate 
    
which prints 

    140c07a (HEAD -> master, feature/1) Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt
    
We can compare with the previous printout (before the merge) and we can see that all commit objects have the same IDs (which means they were not changed) and only the master branch (pointer) was updated. 

Finally, we can clean up unnecessary branches
    
    $ git branch -d feature/1

## Use Case #2 - 3-way merge 

In the first use case we saw a very simple scenario - we created a branch, developed a new feature on it and then brought this change into master while nothing new was added to the master in the meantime. 

Let's now simulate a more realistic scenario - we are going to develop a new feature but before we bring our changes into the master we update master branch we a new commit (in order to simulate that as we were developing our new feature other member of a team committed their updated while we were working on ours).

Let's start with creating a new branch of our dev work.

    $ git checkout master 
    $ git branch feature/2 
    $ git checkout feature/2 
    
The same as before let's simulate our work by adding a new line to our a.txt file

    $ echo yyy >> a.txt 
    $ git add .
    $ git commit -m "Added feature 2"
    
Now let's quickly checkout back to master, and create a commit which will simulate work done by someone else. 

    $ git checkout master 
    $ echo +++ >> b.txt 
    $ git add . 
    $ git commit -m "Added feature 3"
    
Right. Now let's bring out changes from feature/2 branch into master. We are already on master branch so let's run a merge command and see what happens. 

    $ git merge feature/2
    
Git prints
    
    Merge made by the 'recursive' strategy.
     a.txt | 1 +
     1 file changed, 1 insertion(+)
    
In this instance Git applied recursive strategy. It starts similarly to the previous case - Git first looks for the first common ancestor. In this case none of our two branches is poinitng to this commit so Git cannot just update master pointer - this way it would have lost feature/3 commit. What Git does in this case it inspects changes made to both sequence of commits (streams of work) since the first common ancestor and looks for conflicts. BY conflict we understand that the same line of code was updated in 2 streams (branches). If there are no conflicting changes Git can automatically merge changes. We will look more closely at conflict resolution in the next use case. 

Since here we were making changes to 2 different files there was no conflicts and Git handled the entire situation for us. 

Let's look at the log printout

    $ git log --oneline --decorate 
    
Which prints 

    c41a863 (HEAD -> master) Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 (feature/2) Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt

We can immediatelly spot that Git created a new commit and called it 

    Merge branch 'feature/2'
    
Actually, we can get a better intuiton what exactly happened by inspecting a graphical representation of the log. We can do it in console by running log command with --graph paramter.

    $ git log --oneline --decorate --graph 
    
Which prints 

    *   c41a863 (HEAD -> master) Merge branch 'feature/2'
    |\
    | * bd24479 (feature/2) Added feature 2
    * | 40dc13c Added feature 3
    |/
    * 140c07a Added feature 1
    * da9e20e Added ccc to a.txt
    * d5a235c Updated a.txt and created b.txt
    * 814facd Initial commit - added a.txt
    
Now we can clearly see that our new commit has 2 parents. The same information can be found in object itself.

    $ git cat-file -p c41a863
    
It prints 

    tree 3dedb7b88d4c227403ee66e9f513b9aa12a07736
    parent 40dc13c79df698f8eaf437387e5c38ed72bb7cc4
    parent bd24479a7b82df28825babcc30effe08bc2dd41f
    author Karol Gornicki <karol.gornicki@gmail.com> 1477251081 +0100
    committer Karol Gornicki <karol.gornicki@gmail.com> 1477251081 +0100

    Merge branch 'feature/2'
    
Before we close this use case let's clean up unnecessary branches:

    $ git branch -d feature/2 
    
## Use Case #3 - 3-way merge with conflicts 

In this use case let's recreate previous scenario - we create a new branch, make some changes and commit them. At the same time we create a new commit on master. However, this time we are going to create a conflicting change - in both commits we are going to update the same line. We will see how Git is handling it, and how we can improve the process (which will lead us to Use Case #4).

So, let's get started.

    $ git checkout master 
    $ git branch feature/4 
    $ git checkout feature/4
    $ echo zzz >> a.txt
    $ git add . 
    $ git commit -m "Added feature 4"
    
Now let's checkout to master and add different last line to a.txt 

    $ git checkout master
    $ echo @@@ >> a.txt 
    $ git add . 
    $ git commit -m "Added feature 5" 
    
Now let's merge 2 branches together. 

    $ git merge feature/4
    
Git prompts us with the following message 

    Auto-merging a.txt
    CONFLICT (content): Merge conflict in a.txt
    Automatic merge failed; fix conflicts and then commit the result.

Git attempted to do the merge, it discovered conflicts which means it couldn't figure out which changes should be applied to the final version and requested your assistance to fix them before making the final commit. You can see what are the conflicts by running a status command. 

    $ git status
    
It printed out 

    On branch master
    You have unmerged paths.
      (fix conflicts and run "git commit")
      (use "git merge --abort" to abort the merge)

    Unmerged paths:
      (use "git add <file>..." to mark resolution)

            both modified:   a.txt

    no changes added to commit (use "git add" and/or "git commit -a")

In this instance Git marked in the file areas of conflict and we have to resolve them manually. To see how a.txt looks after running this command.

    $ type a.txt 
    
Which prints 

    aaa
    bbb
    ccc
    ddd
    xxx
    yyy
    <<<<<<< HEAD
    @@@
    =======
    zzz
    >>>>>>> feature/4
    
To see in colors what the changes are we can run diff command (we'll talk about those commands in more detail in upcoming sections) 

    $ git diff a.txt
    
Which prints 

    diff --cc a.txt
    index 01cc485,2c97fe7..0000000
    --- a/a.txt
    +++ b/a.txt
    @@@ -3,4 -3,4 +3,8 @@@ bb
      ccc
      xxx
      yyy
    ++<<<<<<< HEAD
     +@@@
    ++=======
    + zzz
    ++>>>>>>> feature/4

Notice the indention of + signs - this actually indicated from which branch change was brought. 
    
We can see that HEAD (which is a commit Added feature/5 from master has @@@ whereas feature/4 added zzz. It's now our job to remove Git annotations: <<<< ===== >>>>. Let's say we want to make the last line @zz. We have to open a.txt in a text edior of our choice and make necessary changes. We can use Vim or notepad.

    $ vim a.txt
    $ notepad a.txt 
    
Now we can inspect our change one more time using diff command 

    $ git diff a.txt 
    
This time it prints 

    diff --cc a.txt
    index c2a946f,3811863..0000000
    --- a/a.txt
    +++ b/a.txt
    @@@ -4,4 -4,4 +4,4 @@@ cc
      ddd
      xxx
      yyy
    - @@@
     -zzz
    ++@zz

Now all we have to do it to commit this change. We will do it in the exact same way as before, and later we'll talk in more detail about add and commit commands.

    $ git add .
    $ git commit -m "Merged feature 4 into master"
    
Quick look at the log

    $ git log --oneline --decorate --graph 
    
It prints 

    *   f6acb1c (HEAD -> master) Merged feature 4 into master
    |\
    | * b7a0577 (feature/4) Added feature 4
    * | 33254a6 Added feature 5
    |/
    *   c41a863 Merge branch 'feature/2'
    |\
    | * bd24479 Added feature 2
    * | 40dc13c Added feature 3
    |/
    * 140c07a Added feature 1
    * da9e20e Added ccc to a.txt
    * d5a235c Updated a.txt and created b.txt
    * 814facd Initial commit - added a.txt

This time intentionally we won't delete feature/4 branch as we're going to reuse it in the next use case. 
  
## Use Case #3 Analysis - Merging with conflicts analysis 

In the last two use cases we just saw how Git can handle marging branches. When everything is simple there is no challenge and git does everything automatically. However, when there are some conflicting changes (which is quite common in typical dev work) Git requires our assistance to reconcile differences. 

This poses another problem. We are making commits which resolve conflicts directly on to the master branch. If we are working only localy there is no harm in doing so. However, if we are merging on a server (we will talk about it later in the course) this can be problematic. Git has a solution for it, called rebasing. 

If we look once again on the graph representing our repository 

    $ git log --oneline --decorate --graph 
    
We can see 

    *   f6acb1c (HEAD -> master) Merged feature 4 into master
    |\
    | * b7a0577 (feature/4) Added feature 4
    * | 33254a6 Added feature 5
    |/
    *   c41a863 Merge branch 'feature/2'
    |\
    | * bd24479 Added feature 2
    * | 40dc13c Added feature 3
    |/
    * 140c07a Added feature 1
    * da9e20e Added ccc to a.txt
    * d5a235c Updated a.txt and created b.txt
    * 814facd Initial commit - added a.txt

that both changes: feature 4 and feature 5 are happening in parallel. At least when we look at branches. However the truth is that one of them happened before the other. To be more precise, feature/5 was added to the master branch before we integrated feature/4. And Git has a solution to this exact situation - it's called rebasing.

As you can see feature 4 commit (b7a0577) and feature 5 (33254a6) share the same parent commit. This formation looks kind of like a fork - 2 commits have the sme parent. Git can sort of move this fork to happen after feature 5. In other words is can take our feature 4 branch and apply it after feature 5 branch. Let's see how it looks in practice. 

For brevity, let's undo the last commit 

    $ git reset HEAD~
    $ git checkout -- a.txt 
    
Later we explain how to undo commits in more detail. 

Now we want to re-apply our branch after the last commit on master branch. To do that we have to checkout feature/4 branch and call rebase command. 

    $ git checkout feature/4
    $ git rebase master 
    
This prints 

    First, rewinding head to replay your work on top of it...
    Applying: Added feature/4
    Using index info to reconstruct a base tree...
    M       a.txt
    .git/rebase-apply/patch:9: trailing whitespace.
    zzz
    warning: 1 line adds whitespace errors.
    Falling back to patching base and 3-way merge...
    Auto-merging a.txt
    CONFLICT (content): Merge conflict in a.txt
    error: Failed to merge in the changes.
    Patch failed at 0001 Added feature/4
    The copy of the patch that failed is found in: .git/rebase-apply/patch

    When you have resolved this problem, run "git rebase --continue".
    If you prefer to skip this patch, run "git rebase --skip" instead.
    To check out the original branch and stop rebasing, run "git rebase --abort".

Git essentially says that it tried to re-apply this change and encounter a conflict. It cannot resolve this conflict automatically, so we have to do it manually. This is pretty much exactly the same situation as before. To see which files require resolving

    $ git status 
    
We can look at the file 

    $ type a.txt 
    
It looks exactly the same before, so we resolve the conflict in a text editor. 

After we resolved the conflict we have to add this file to the staging area (we will explain that in the next segment) and enter a command telling Git to continue rebasing. 

    $ git add .
    $ git rebase --continue 
    
It prints 

    Applying: Added feature/4
    
Because there are no more comments we know that Git finished rebasing. We can look at the graphical representation of our repository 

    $ git log --oneline --decorate --graph
    
It prints 

    * dcee90e (HEAD -> feature/4) Added feature 4
    * 33254a6 (master) Added feature 5
    *   c41a863 Merge branch 'feature/2'
    |\
    | * bd24479 Added feature 2
    * | 40dc13c Added feature 3
    |/
    * 140c07a Added feature 1
    * da9e20e Added ccc to a.txt
    * d5a235c Updated a.txt and created b.txt
    * 814facd Initial commit - added a.txt

As we can see this applies our feature 4 change after feature 5. In decorations of each commit we can see that feature 4 is applied of the last commit from master branch. In this situation if we would like to integrate these changes into master we simply run merge command which would perform fast forward. Fast forward simply updated master branch ponter.

We can also notice that Git didn't move the old commit ahead of all other commits, it re-created that commit (+ any changes that resulted with rebasing). We can inspect this change by
    
    $ git show dcee90e

Why does it matter? Using this apprach is preferred when we work with a remote repository. The same way as we here integrate our changes with master branch we can push our changes to some remote repository and integrate with its master branch. We would like to resolve all possible conflicts in our local repository and only when we are sure that everything works we would push our changes and on the server, in order to bring those changes into master we Git would only do fast-forward merge. 

Resolving conflicts is error prone. It is simply better to do it privately and only when we are sure we promote it to the master branch. Also, our history is clearer. let's finish the job by merging them
    
    $ git checkout master
    $ git merge feature/4 
    
And look at the log 

    $ git log --oneline --decorate --graph 
    
It prints 

    * dcee90e (HEAD -> master, feature/4) Added feature 4
    * 33254a6 Added feature 5
    *   c41a863 Merge branch 'feature/2'
    |\
    | * bd24479 Added feature 2
    * | 40dc13c Added feature 3
    |/
    * 140c07a Added feature 1
    * da9e20e Added ccc to a.txt
    * d5a235c Updated a.txt and created b.txt
    * 814facd Initial commit - added a.txt

We don't have a merge commit, and instead resolving any conflicts happened in feature 4 commit (dcee90e). This will be beneficial in the future when you will have to inspect the history of changes in order to understand what has changed and when. 