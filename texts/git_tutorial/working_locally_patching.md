# Patching

As we saw previously, in Git you can add only some of your changes made to the file to staging area. Let's now update first and last line of a.txt - for readability we will use numbers. 

Updated file looks like that 

    $ type a.txt 
   
And Git diff
    
    $ git diff a.txt 
    
prints 

    diff --git a/a.txt b/a.txt
    index b741796..3fc402d 100644
    --- a/a.txt
    +++ b/a.txt
    @@ -1,7 +1,7 @@
    -aaa
    +123
     bbb
     ccc
     ddd
     xxx
     yyy
    -@zz
    +987
    
Say we only want to commit the first line change, but not the last one. Git allows us to do it by patching. We will tell Git to break changes to this file into chunks (Git calls them hunks - don't know why). This is a bit complicated, but turns out to be very useful. 

We are going to use interactive add command. 

    $ git add -i 
    
This will prompt you with a menu, select p or 5. Next Git will list all files you can select a.

Next hit Ctrl+Z and enter - this shows the first hunk. Git decided to treat our entire change as one hunk, however, we can break it into pieces. It prints 

    diff --git a/a.txt b/a.txt
    index b741796..3fc402d 100644
    --- a/a.txt
    +++ b/a.txt
    @@ -1,7 +1,7 @@
    -aaa
    +123
     bbb
     ccc
     ddd
     xxx
     yyy
    -@zz
    +987
    Stage this hunk [y,n,q,a,d,/,s,e,?]?
    
it gives us quite a few options, type ? to see their explainations. 

We want to select e (for edit). It opens Vim with 

    # Manual hunk edit mode -- see bottom for a quick guide
    @@ -1,7 +1,7 @@
    -aaa
    +123
     bbb
     ccc
     ddd
     xxx
     yyy
    -@zz
    +987
    # ---
    # To remove '-' lines, make them ' ' lines (context).
    # To remove '+' lines, delete them.
    # Lines starting with # will be removed.
    #
    # If the patch applies cleanly, the edited hunk will immediately be
    # marked for staging. If it does not apply cleanly, you will be given
    # an opportunity to edit again. If all lines of the hunk are removed,
    # then the edit is aborted and the hunk is left unchanged.
    
In order to keep the first line update @@ -1,7 +1,7 @@ to be @@ -1 +1 @@ and remove the last two lines. The file should look like that. In Vim in order to edit you have to enter interactive mode (hit i) - now you can edit as you would do in notepad or any other text editor (you cannot use mouse though).

    # Manual hunk edit mode -- see bottom for a quick guide
    @@ -1 +1 @@
    -aaa
    +123
     bbb
     ccc
     ddd
     xxx
     yyy
    # ---
    # To remove '-' lines, make them ' ' lines (context).
    # To remove '+' lines, delete them.
    # Lines starting with # will be removed.
    #
    # If the patch applies cleanly, the edited hunk will immediately be
    # marked for staging. If it does not apply cleanly, you will be given
    # an opportunity to edit again. If all lines of the hunk are removed,
    # then the edit is aborted and the hunk is left unchanged.

Save your changes and quit Vim (ESC + :wq + ENTER). Quit interactive add command with q + ENTER. 

Now let's see how our status look like 

    $ git status 
    
All good. We can inspect changes
    
    $ git diff --cached a.txt 
    $ git diff a.txt 

This technique is particulary useful when we mock about with config or solution files.