# Ignoring files 

During our development work we very often create files which are not part of the build. They are either some auxiliary files or our final binaries. They usually change each time we introduce any change to our code. There would be no point to commit them to the repository. 

However, making sure manually that we are not going to commit them is error prone - for once, you have to know which files should not be committed and essentially you are restricting yourself not to use dot shortcut (for instance for add command). 

Git has a solution for that as well - it can ignore specific files. Let's see it in action. First we will add log folder with some file, and next we will show how it can be ignored by Git. 

    $ mkdir logs 
    $ cd logs 
    $ echo abc >> log.txt 
    $ cd ..
    
If we now run status command 

    $ git status 
    
It shows that we added a new folder, logs. 

    On branch master
    Untracked files:
      (use "git add <file>..." to include in what will be committed)

            logs/

    nothing added to commit but untracked files present (use "git add" to track)
    
We can tell Git which files should be ignored in .gitignore file - it's a plain text file. So let's create one 

    $ echo /logs >> .gitignore 
    
If we now run status command 

    $ git status 
    
It prints 

    On branch master
    Untracked files:
      (use "git add <file>..." to include in what will be committed)

            .gitignore

    nothing added to commit but untracked files present (use "git add" to track)
    
It shows that logs are no longer detected, however, .gitignore file is. We have to commit it to our repository so other team members can benefit from it. 

Now, what did we actually add to .gitignore file. This file specifies what Git should ignore. It can be summarized with few rule 

* /logs - ignore logs folder at the root of the repository 
* logs - ignore any logs folder
* a.txt - ignore any a.txt file 

Let's commit .gitignore file.

    $ git add .
    $ git commit -m "Added .gitignore file"