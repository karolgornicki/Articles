# Stashing

Stashing is a very convenient way of temporarily saving your changed files to Git's clipboard without officially committing them. Let's look at the example - say you are working on a new feature.

    $ git checkout -b feature/6
    $ echo ddd >> a.txt 
    
You made some changes, but let's say they are not quite finished yet. All of a sudden your boss comes along and asks you you drop whatever you were doing and work on a quick fix for something else. What would you do in this situation? You don't want to commit work that is half-done (it's a very bad practice to create commits that can introduce bugs - every commit have to leave the system in a stable state, eg everything builds). The easiest way would be to copy all the files which show up in 

    $ git status 
    
into some folder outside your Git repository, work on fix for your boss, and once you done copy them back to the repo and continue working. Obviously this is very inefficient and error prone. 

Luckily Git can help you with it. You can stash your changes., What it does it takes all your changes (tracked files), adds them to stash and reverts them to their original state. 

Let's see it in practice. We changed one file, we're not finished, so we want to stash it. 

    $ git stash 
    
It prints 

    Saved working directory and index state WIP on feature/6: 729be61 Added *** to a.txt
    HEAD is now at 729be61 Added *** to a.txt
    
Git automatically created the name under which we can recognize out change (WIP stands for work in progress), and reverted any changes made to our files to their most recent commit. If we now check the status 

    $ git status 
    
no change is detected, our repository is clean. So where did our changes go? They are stashed - we can inspect the stash

    $ git stash list 

which prints 

    stash@{0}: WIP on feature/6: 729be61 Added *** to a.txt
    
The part before colon (:) is sort of like ID of stashed elements. You can stash more than one change by the way.

Let's do the quick fix then 

    $ git checkout master 
    $ git checkout -b quickfix/1
    $ echo xyz >> b.txt 
    $ git commit -am "Fix for b.txt (added xyz)"
    $ git checkout master 
    $ git merge quickfix/1
    $ git branch -d quickfix/1
    
Merge does fast forward, our fix is not incorporated into master. Now we can switch to our feature/6 branch, bring our stashed changes back (obviously before committing we will also rebase). 

    $ git checkout feature/6

Let's inspect stash 

    $ git stash list 
    
It prints 

    stash@{0}: WIP on feature/6: 729be61 Added *** to a.txt
    
Now we can bring those changes from stash into our working directory, and we have 2 options 

* we can remove them from stash (in our case stash will be empty)
* we can keep them in stash (maybe we want to experiment or something)

In order to copy (apply) them from stash we run 

    git stash apply stash@{0}
    
As you can see we refer to the stash entry by its sort-of ID. It print 

    On branch feature/6
    Changes not staged for commit:
      (use "git add <file>..." to update what will be committed)
      (use "git checkout -- <file>..." to discard changes in working directory)

            modified:   a.txt

    no changes added to commit (use "git add" and/or "git commit -a")

    
We can see the change in a.txt 

    $ git diff a.txt 
    
Now, our stash also contain it. 

    $ git stash list 
    
If we would like to remove this entry we simply run 

    $ git stash drop stash@{0}
    
This prints 

    Dropped stash@{0} (a60ed9e6d88fbdeff9dad64f7a41127146f31e4d)
    
If we now check stash
    
    $ git stash list 
    
Nothing is printed - our stash is empty. 

Since these 2 commands often go hand in hand you can use pop command which essentially combines them. Let's have a quick experiment - we will stage our changes again and this time use pop command. 

    $ git stash 
    
Things got stashed, it prints 

    Saved working directory and index state WIP on feature/6: 729be61 Added *** to a.txt
    HEAD is now at 729be61 Added *** to a.txt
    
Quick status check 

    $ git status 
    
Quick look at out stash

    $ git stash list 
    
It has one change  

    stash@{0}: WIP on feature/6: 729be61 Added *** to a.txt
    
Let's pop this 

    $ git stash pop stash@{0} 
    
It prints 

    On branch feature/6
    Changes not staged for commit:
      (use "git add <file>..." to update what will be committed)
      (use "git checkout -- <file>..." to discard changes in working directory)

            modified:   a.txt

    no changes added to commit (use "git add" and/or "git commit -a")
    Dropped stash@{0} (7ef2ae5b22dfd21a68cdc4b72a7b9d3224d03cc5)

Our working directory is updated 

    $ git status 
        
And our stash is empty

    $ git stash list 
    
Let's just quickly clean up by discarding all our changes. 

    $ git checkout a.txt 
    $ git checkout master 
    $ git branch -d feature/6