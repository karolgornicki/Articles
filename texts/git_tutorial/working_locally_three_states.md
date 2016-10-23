# Three states

Let's talk now how Git organizes a working directory. When we create a new repository with 

    $ git init 
    
Git treats that folder as a Git repository. It allows you to commit changes made to files in that folder and whatnot. In earlier parts of this tutorial we didn't pay much attention to this as we were focusing on internal mechanics of Git. In this section however we are going to explain in detail what is going on in your workspace - often refered to as working directory. 

Let's get right into the demo and make a change to one of our files 

    echo new line ... >> a.txt 
    echo another new line ... >> b.txt
    
Our file has changed, we appended a new line. Git allows us to discover any files that were changed or, were added. 

    $ git status 
    
The output of this command

    On branch master
    Changes not staged for commit:
      (use "git add <file>..." to update what will be committed)
      (use "git checkout -- <file>..." to discard changes in working directory)

            modified:   a.txt
            modified:   b.txt

    no changes added to commit (use "git add" and/or "git commit -a")

tells us on what branch we are currently working, and lists all files that were either edited, added or removed since the last commit. Previously in order to commit our changes we were running the following commands (don't run the below commands now).

    $ git add .
    $ git commmit -m "Here we were adding a message" 
    
If you did run them you can undo it by 

    $ git reser HEAD~
    
Let's break it down, line by line. The first line calls add command. This command adds selected files to staging area - dot (.) means add everything. Staging area is the area where we prepare our commit - in our working directory we can change bunch of files but we only want to commit some of them. Let's say we only want to commit file a.txt. 

    $ git add a.txt 
    
If we now check the status of out repository

    $ git status
    
it prints 

    On branch master
    Changes to be committed:
      (use "git reset HEAD <file>..." to unstage)

            modified:   a.txt

    Changes not staged for commit:
      (use "git add <file>..." to update what will be committed)
      (use "git checkout -- <file>..." to discard changes in working directory)

            modified:   b.txt

It shows  that file a is marked as "to be committed" - that's the staging area (also called index) where we are preparing the commit, whereas b.txt is listed under not staged. Now we could commit only a.txt but before we do that let's talk about diff command. 

Just because we made a change to a.txt and added it to staging it doesn't prevent us from adding another change. 

    $ echo another line ... >> a.txt 
    
If we run status command 

    $ git status 
    
we get 

    On branch master
    Changes to be committed:
      (use "git reset HEAD <file>..." to unstage)

            modified:   a.txt

    Changes not staged for commit:
      (use "git add <file>..." to update what will be committed)
      (use "git checkout -- <file>..." to discard changes in working directory)

            modified:   a.txt
            modified:   b.txt

On the first instance that may look weird that a.txt appears in both categories, but it actually makes sense. First we added a line and added this to staging area. We didn't actually added a file - we added our change. So if we make another change Git simply interprets it as another change that we have to add.

Working directory can be depicted like that 

[GIT-WORKING-DIRECTORY]

These are the three states: working directory, staging, and repository.

Just knowing which files has change is only parlty useful - we would like to know what exactly did we change and we are about to commit. To discover this information we are going to use diff command (we used it briefly earlier on). This command allows us to see differences between files in different states.

To see what we changed in b.txt simply run 

    $ git diff b.txt
    
In order to see all changes that we introduced since the last commit to file a.txt run 

    $ git diff HEAD a.txt 
    
This lists 2 new lines. 

In order to see what we added to staging (for a.txt) run
    
    $ git diff --staged a.txt 
    
In order to see unstaged changes to a.txt run 

    $ git diff a.txt 
    
This can be represented on a chart as 

[CHART-DIFF-STAGES]

Now you also understand when we were rebasing and had to resolve conflicts we were adding resolved files to staging area and entering a command for Git to continue rebasing. Git was essentially picking up files from staging area in order to make a new commit. Remember, during rebasing Git doesn't move commits, but it actually creates a completely new ones (only changes made to files are the same + your conflict resolutions). 