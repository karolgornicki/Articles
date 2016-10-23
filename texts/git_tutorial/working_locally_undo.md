# Undo

One more thing before we conclude this section. Say we did something wrong and we want to undo our changes. 

In order to take a file out of staging area run 

    $ git reset a.txt 
    
In order to remove all changes we made to the file
    
    $ git checkout -- b.txt
    
In order to undo the entire commit let's first add a new one.

    $ git add a.txt 
    $ git commit -m "Updated a.txt with 2 extra lines" 

Quick look at logs

    $ git log --oneline 
    
In order to remove this commit run 

    $ git reset HEAD~
    $ git log --oneline 
    
Our commit it gone, if we look at the status 

    $ git status 

We can see that there are some uncommitted changes to a.txt. If we drill down

    $ git diff a.txt 

We can see that Git removed the previous commit (in fact the object is kept in .git folder - go to .git folder and try to find this object) but kept all the changes that we previously made in case we wanted to make some quick fix. If you want to discard there changes simply run

    $ git checkout -- a.txt 