# Git internals 

This section will explain how you can get started with Git. Then we will do few basic operations and dive right into the internals of Git - what is actually going on under the hood. 

##Installing Git

In order to install Git go to 

    https://git-scm.com/

and download the latest Git release. If you are installing it on Windows OS, at some point installation wizard will prompt you with the option to "Adjusting your PATH environment".  Select "Run Git and include Unix tools from the Windows Command Prompt" - there is a red warning next to it. It replaces Windows' find command (no-one uses it anyway) with its Unix version which is much more powerful, and adds few more commands, eg ls.

##First steps 

Now we have Git installed on our machine, so let's create a new repository and make some changes to it. Next, we'll see what Git actually stores this information. First, let's navigate to the folder where we want to create a new repository (feel free to locate it elsewhere, it doesn't matter). In this tutorial we will be interacting with Git via command line.

    $ cd c:\
    $ mkdir git
    $ cd git
    $ mkdir test_repo
    $ cd test_repo 

Let's create a new Git repository

    $ git init 

Initially our folder was empty. This command created a hidden folder (.git) which tells Git that this folder (test_repo) is now a repository. .git folder is going to store all information relating to your repository - history of files, all versions, their descriptions and many more.

Let's now create a file and commit it to our repository. For brevity we'll do everything in console.

    $ echo aaa >> a.txt

This command prints "aaa" into a a.txt file. Now we want to commit this file to our repository (create the first snapshot). We can do it by running this one command (later we will show you a different way which gives you more control over what happens)

    $ git add .
    $ git commit -m "Initial commit - added a.txt"

Let's make some more changes: let's update a.txt file and create a new one.

    $ echo bbb >> a.txt
    $ echo 123 >> b.txt

Let's now add them to the repository

    $ git add .
    $ git commit -m "Updated a.txt and created b.txt"


OK. Now it's a good time to look retrospectively what has just happened. In the previous section we said that Git (like any other version control system) records changes that happens to our software. In this repo we recorded 2 changes

* First, when we created a.txt file with "aaa"
* Second, when we updated a.txt by adding second line with "bbb" and created a new file, b.txt with "123"

Git's job is to record them (on our request) and allow us to view this history. We can easily view this history by running the following command.

    $ git log 

The output can look like this

    commit d5a235c0af73676dabbf28d957090043492c98b1
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:14:30 2016 +0100

        Updated a.txt and created b.txt

    commit 814facd944926d714eae63a4f53fd9d430101b0e
    Author: Karol Gornicki <karol.gornicki@gmail.com>
    Date:   Sun Oct 23 20:13:49 2016 +0100

        Initial commit - added a.txt

This shows journal of all changes that we made in our repository. As you can see the most recent change is listed at the top. 

As we can see, each commit has a unique ID (the 40-character long string next to the word commit). This is how Git identifies commits. In fact, when we ran 

    $ git commit [...]
    
Git creates a commit object which encapsulates all relevant information for that snapshot and stores it in .git folder. More about that in the next section.

##Object-based database

All files related to the management process of our repository are stored in .git folder that was created when we ran $ git init command. Let's navigate to that folder and see what's in it 

    $ cd .git
    $ ls -l
    
As we can see that are few folders in it. For the time being we will focus on objects folder and as we advance further in this tutorial we will come back here and talk about the remaining ones.

    $ cd objects 
    $ ls -l
    
There are quite a few folders with names only 2-characters long. When Git creates objects it gives each object an ID (40 character long) and splits it into prefix and suffix 2+38 respectively. The prefix is used to create a folder and inside that folder it creates a file named using suffix. So if we look at our most recent commit its ID is d5a235c0af73676dabbf28d957090043492c98b1 and indeed we can see "d5" folder. If we enter that folder we can see a file which name's correspond to the last 38 characters of the ID. 

    $ cd d5
    $ ls -l
    
If we try to open this file in any text editor we will find out that it's content is not readable. Git in order to preserve space on your drive compresses these files. Fortunately, we can use one of many git commands to read the content of that file, referning it by the ID.

    $ git cat-file -p d5a235c0af73676dabbf28d957090043492c98b1
    
The output

    tree 40605725a8fef247117f96546f88e139365849a8
    parent 814facd944926d714eae63a4f53fd9d430101b0e
    author Karol Gornicki <karol.gornicki@gmail.com> 1477250070 +0100
    committer Karol Gornicki <karol.gornicki@gmail.com> 1477250070 +0100

    Updated a.txt and created b.txt
    
At the very bottom we can see the description that we gave to that commit when we created it. It also has an information about committer and author. Lastly we see 2 entries, parent and tree. However, they are  different than all other entries. Instead of having any infromation they reference other files (by their IDs). If we look carefully we notice that the 40-character long ID next to parent is exactly the same as ID of the first commit. This tells Git what was the previous state (the previous commit) of our files. Every commit (except from the first commit) has at least one parent. We'll talk later when a commit can have multiple parents. To visually represent this notion you can think that each commit has a pointer to their parent commit - the one that happen before. This creates a directed graph.

![PICTURE-2-COMMITS](https://github.com/karolgornicki/Articles/blob/master/img/git_tutorial/first_two_commits.jpg)

The last thing that must discuss here is tree entry. This entry points to the state of our files. So let's see what's in it. Git allows us to interrogate all of its files, so we can use exactly the same command to inspects its content.

    $ git cat-file -p 40605725a8fef247117f96546f88e139365849a8
    
The output is 

    100644 blob d2260f8cf9979e1d457aa040b73f61244a902d82    a.txt
    100644 blob fb0a03d992212388b6835ab96fe4affa483a100b    b.txt
    
It depicts how our repository looked like when we made a commit. At that time we had 2 files, and they both are represented by Git objects. If we drill down to any of them we'll see their content at that time. 

    $ git cat-file -p d2260f8cf9979e1d457aa040b73f61244a902d82

Git stores the entire content of that file (at the time of the snapshot), not just changes that we've made. Git is very space efficient in doing so and there is no need to be worried. To the left of the ID we have "blob". This is type of the file. If we would have a folder in our directory it would be represented by a file of a type "tree". Drilling down into this file would reveal its content, just like we've done it here. 

The picture below visualizes how our repository could be represented.

![PICTURE-GIT-COMMITS-AND-FILES](https://github.com/karolgornicki/Articles/blob/master/img/git_tutorial/commit_with_details.jpg)

During the evolution of your repository Git simply grows its files representing various objects. This is essentially Git's database storing history of everything that happened in your repo. 

Let's come back to the root folder of our repository.

    $ cd ..\..\..
    
##References and branches 

Previous section explained how Git stores all the information about commits. During the inspection we learned that commits point to their parent commits (ones that happened immediately before). Therefore Git must knows which commit is the last one. Indeed, when we run log command it shows the most recent commit at the top. 

Git stores the reference to the most recent commit and it is called HEAD. Not surprisingly, HEAD is a file in .git folder. We can inspect its content.

    $ type .git\HEAD
    
It prints 

    ref: refs/heads/master

It simply represents a reference to another file. If we navigate to that file and open it up we'll find that this file contains only 40-character long ID (of the last commit).

    $ type .git\refs\heads\master 
    
It prints 

    d5a235c0af73676dabbf28d957090043492c98b1
    
which is ID of the last commit. 

Let's create another commit to see what happens.

    $ echo ccc >> a.txt
    $ git add .
    $ git commit -m "Added ccc to a.txt"
    
master file was updated and now it points to the most recent commit. We can also see it using --decorate option for log command.

    $ git log --oneline --decorate 
    
It shows

    da9e20e (HEAD -> master) Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt

By default log command is quite verbose and for brevity we used here additional parameters. --oneline presents log in abbreviated format, and --decorate includes information about references. In this case it shows that the last commit is marked as HEAD, and HEAD is pointing to master. 

Master is not some random file. It's a branch. Every Git repository is created, by default, with a single branch called master. As you can see from the above, branch is just a pointer to the commit. We can see all branches in our repository by running 

    $ git branch 
    
It shows 

    * master
    
We only have one branch, * next to the name of the branch indicates our currently checked out branch. Let's create a new branch then. We can do it by running 

    $ git branch experiment 
    
If we re-run branch command we get 

      experiment
    * master
    
Now we've got 2 branches. Since branches are just stored in files, we can inspect them.

    $ cd .git/refs/heads/
    $ ls -l
    $ type experiment 
    $ type master 
    
Both of them point to the same commit. We can also see it reflect in log.

    $ cd ..\..\..
    $ git log --oneline --decorate 
    
Prints:

    da9e20e (HEAD -> master, experiment) Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt

Git encourages developers to work on branches, where each branch represents a stream of work - for example a development of a new feature, or a bug fix. First let's see it in practice. 

Let's first check out branch - it tells Git on which branch we want to work now.

    $ git checkout experiement 
    
We can also inspect quickly how HEAD look now. 

    $ type .git\HEAD
    
HEAD is now poitning to experiment, whereas it was previously pointing to master. Let's make some changes to our files and commit them 

    $ echo ddd >> a.txt
    $ git add . 
    $ git commit -m "Added ddd to a.txt"
    
Let's see how it is reflected in log.

    $ git log --oneline --decorate 
    
It prints 

    5539dc6 (HEAD -> experiment) Added ddd to a.txt
    da9e20e (master) Added ccc to a.txt
    d5a235c Updated a.txt and created b.txt
    814facd Initial commit - added a.txt

What's interesting is that we created a new commit (listed at the top), and experiment branch is pointing to it. However, master branch "didn't move". It points to the third commit as it was before. That's because we checked out to experiment branch, which essentially told Git that now we are working on this branch and any commits should be commited to this branch. If we inspect the content of the last commit we will learn that it references 3rd commit as its parent. 

    $ git cat-file -p 5539dc6
    
It prints 

    tree 966f4a6af5210044f777610113d9aaf454eed377
    parent da9e20ee64755bb5b705c277599db88466903d0e
    author Karol Gornicki <karol.gornicki@gmail.com> 1477250610 +0100
    committer Karol Gornicki <karol.gornicki@gmail.com> 1477250610 +0100

    Added ddd to a.txt

Our repository can be depicted as a graph 

![PICTURE-REPO-GRAPH](https://github.com/karolgornicki/Articles/blob/master/img/git_tutorial/repository_with_experiment.jpg)

In the next section we will show how we can join branches (Git calls it merging), but before that we have to revisit the checkout command. We said that it tells Git on which branch we want to work (it updates HEAD file to point to the branch selected by us). Obviously we can have more than one branch, so what exactly happens when we switch branches. Let's to the experiement. At first, let's print the content of a.txt (tail prints the last 10 lines of a file).
    
    $ type a.txt
    
We can see all 4 lines. Let's now switch to master and inspect the same file.

    $ git checkout master 
    $ type a.txt
    
It doesn't have the last line - "ddd". This goes back to the point when we talked about commit objects - they store full content of all files in our repo. When we checkout a branch (and remember, branch is just a pointer to a commit object) Git updates all files to the state as of that commit. If we now run log command 

    $ git log --oneline --decorate 
    
we will see only 3 commits - branch master doesn't know about branch experiment, and it shouldn't. da9e20e commit only has a pointer to its parent (previous) commit, not the future ones.

In the next section we'll explain how you can work productively with Git and cover more complicated scenarios.
