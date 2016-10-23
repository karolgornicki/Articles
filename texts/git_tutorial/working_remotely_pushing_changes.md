#Sending changes to remote repository

Sending change to the remote repository is as simple as running push command. So let's first make a new branch, promote it to the remote repository and commit something locally.

    $ git branch new-branch 
    $ git push -u origin new-branch
    $ git checkout new-branch 
    $ echo uuu >> a.txt 
    $ git commit -am "Added uuu to a.txt" 
    
We just made a change locally. It's not visible on remote (feel free to check). In order to publish this commit to remote all we have to do is to run push command.    

    $ git push 

Note, that if we would have previously rebased this branch we would have to apply -f (for force) parameter in order to sucessfully execute push command. 

    $ git push -f 

It is usuallt the case that you are pushing something to the remote branch and create a pull request on the server. Next, person responsible for managing remote repository reviews your change and merges into main development stream (for example master branch) so everyone else can see this change and update their own repositories. 