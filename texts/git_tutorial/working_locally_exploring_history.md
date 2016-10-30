# Exploring history 

Git is a version control system and its primary job is to keep history of our files. It wouldn't be much useful if we wouldn't be able to browse this history and query. 

## Browsing history 

We have already seen log command. 

    $ git log 
    
It prints all commits starting from the most recent one. Git is paging it's output, so to get the next page (batch) of commits press Pg Dn (Page down button), if you don't want to see any more commits press q (for quit). Very often we don't want to see all commits but the last few. We can apply 

    -2
    
parameter. It will limit the output to the last 2 commits.

    $ git log -2 
    
Default printout of log command is quite verbose. When we look at large number of commits it's usually preferable to see them in their abbreviated form. We can achieve that by applying 

    --oneline 
    
For example

    $ git log --oneline 
    
We can always combine multiple parameters 

    $ git log --oneline -5
    
Which prints 

    b2be659 Fix for b.txt (added xyz)
    729be61 Added *** to a.txt
    dcee90e Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    
Another useful option is to print branches pointing to each commits
    
    $ git log --oneline --decorate 
    
It prints 

    b2be659 (HEAD -> master) Fix for b.txt (added xyz)
    729be61 Added *** to a.txt
    dcee90e (feature/4) Added feature 4
    33254a6 Added feature 5
    c41a863 Merge branch 'feature/2'
    40dc13c Added feature 3
    bd24479 Added feature 2
    140c07a Added feature 1
    da9e20e Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt
    
We can also see sequence of commits represented as a graph where links show parents to commits
    
    $ git log --oneline --decorate --graph 
    
It prints 

    * b2be659 (HEAD -> master) Fix for b.txt (added xyz)
    * 729be61 Added *** to a.txt
    * dcee90e (feature/4) Added feature 4
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

There are plenty of options you can apply to log command. You can see the help website by using 

    $ git log --help 
    
In fact, you can apply --help option to any git command and it will open a website with command documentation. 

## Querying history 

We just discussed few techniques you can use to browse the history. It's not always obvious where some change occur. For example, we know there is a bug in file b.txt and we would like to see all related commits. 

Let's quickly look at a log output

    $ git log -1 
    
It prints 

    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 30 09:25:24 2016 +0000

        Fix for b.txt (added xyz)

It has the message we gave this commit its author and date stamp. Our first strategy could be to query against those information. Say we want to find all commits which reference "b.txt" in their messages (it's not really a good way of finding commits, but in order to demonstrate the basic concept it will do). 

    $ git log --grep b.txt 
    
We use --grep option after that we append text to search for. It prints 

    commit b2be659fb69d5ed42c252ef77bb15309b09387de
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 30 09:25:24 2016 +0000

        Fix for b.txt (added xyz)

    commit d5a235c0af73676dabbf28d957090043492c98b1
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:14:30 2016 +0100

        Updated a.txt and created b.txt

As we said, it's not really a bullet proof technique to find commits relating to a particular file. Instead, we can specifically look for all commits which updated a particular file. For that we use --follow option 

    $ git log --follow b.txt 
    
It prints 

    commit b2be659fb69d5ed42c252ef77bb15309b09387de
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 30 09:25:24 2016 +0000

        Fix for b.txt (added xyz)

    commit 729be61337ffaf6ba7e6981c8be3b3916d444e28
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:51:00 2016 +0100

        Added *** to a.txt

    commit 40dc13c79df698f8eaf437387e5c38ed72bb7cc4
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:31:01 2016 +0100

        Added feature 3

    commit d5a235c0af73676dabbf28d957090043492c98b1
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:14:30 2016 +0100

        Updated a.txt and created b.txt

Let's say we would like now to see what files were changes in "Added feature 3" change. The name doesn't give away much clues (and here's another lesson - we must always use meaningful names for commits that will help developers understand how their code evolve). 

In order to see files that were changes in a particular commit we can use show command, and reference a commit by its ID (by the way, we can only apply the first few characters and Git will be able to figure out which commit object we refer to). 

    $ git show --name-only 40dc13c7
    
It prints 

    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:31:01 2016 +0100

        Added feature 3

    b.txt

Although it starts by printing all the information we already saw in log output it then appends list of files. In this particular case only one file was changed. If we remove --name-only option it will also prints out the actual change that was introduced. 

    $ git show 40dc13c7
    
It prints 

    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:31:01 2016 +0100

        Added feature 3

    diff --git a/b.txt b/b.txt
    index fb0a03d..ab3968a 100644
    --- a/b.txt
    +++ b/b.txt
    @@ -1 +1,2 @@
     123
    ++++

If you would like to see how much a file has changed between given 2 commits you can apply diff command. For example, we know that b.txt was updated in 4 commits, so let's see the change between 1st and 3rd commit. 

    $ git diff d5a235c 729be61
    
It prints 

    diff --git a/a.txt b/a.txt
    index d2260f8..7e348ac 100644
    --- a/a.txt
    +++ b/a.txt
    @@ -1,2 +1,8 @@
     aaa
     bbb
    +ccc
    +xxx
    +yyy
    +@zz
    +new line ...
    +***
    diff --git a/b.txt b/b.txt
    index fb0a03d..b85c7b6 100644
    --- a/b.txt
    +++ b/b.txt
    @@ -1 +1,3 @@
     123
    ++++
    +another new line ...

As we can see it prints changes to all files that have been updated between these 2 commits. If we want to limit the output to only one file we can append its name 

    $ git diff d5a235c 729be61 b.txt 
    
Which prints 

    diff --git a/b.txt b/b.txt
    index fb0a03d..b85c7b6 100644
    --- a/b.txt
    +++ b/b.txt
    @@ -1 +1,3 @@
     123
    ++++
    +another new line ...

Another very often use case is when we look for changes to a particular line. Let's look at file a.txt 

    $ type a.txt 
    
It prints 

    aaa
    bbb
    ccc
    xxx
    yyy
    @zz
    new line ...
    ***

Let's say we are suspicious about the line 6: @zz and we would like to find all commits which made changes to that line. We would use blame command. 

    $ git blame -L 6,6 -- a.txt
    
It prints 

    dcee90e8 (Karol Gornicki 2016-10-23 20:34:08 +0100 6) @zz

Unfortunately, in our repository we were only appending new lines to files so we only have 1 commit listed. In the blame command we had to specify the range of lines we want to inspect - in our case from 6 to 6 (6,6) and specify in which file. 