# The big picture 

In this section we will briefly explain what a version control system is, and what problems it is designed to solve. Next we will see how these objectives can be met by examining few most common workflows. Every team and project is different and how teams incorporate version control system into their workflow is their individual decision. We will cap this section with a quick overview of Git and what distinguishes Git from other version control systems.

##What's version control system? 

Version control system can be understood quite literally. It's a database system that records versions of your files. It's a system that allows you to take snapshots of your code and save it to the database. Later you can explore this history and do all sorts of useful things with it. 

##Why teams use VCS?

**Collaboration** For a second let's consider how team of developers could work on the same codebase. They would probably have to use some shared folder for their code and constantly talk to one another who's working on which files in order to avoid overwriting someone else's changes. This approach is very time consuming and error-prone. VCS puts a workflow in place which allows multiple people working at the same time on shared codebase and reconciles any conflicts in a very scrutinized way.

**Audit** Because software nowadays is team effort it is very important that the evolution of the code can be easily explained - who did this change, when and why. Auditing is at the core of VCS - it literally records versions of your files. Each new version of a file is labeled with a description, its author and a timestamp. VCS provides means to query the history of file which proved to be very efficient way of learning how and why software evolved in a particular way. Another aspect is regulatory constraints - almost every commercial software requires transparent history of the code. 

**Backup** Unfortunately, we make mistakes. If we think for a second about our fictitious setup when developers were using shared folder - what would happen when someone made a mistake which must have been rolled back (to undo the last change). In a shared folder scenario it would have been close to impossible. VCS on the other hand works as a database, documenting evolution of your code and enables you to "go back in time" and revert your code to the past state. 

##How teams use VCS

The idea of VCS doesn't enforce one particular way of working. Each projects is different and it's team's responsibility to adopt a workflow which suits them best. VCSs have been around for quite some time and few workflows emerged as the most popular.

**Centralized** In centralized workflow we have an official repository. It's goal is to provide one focal point from where all developers in the team take the latest code. Every developer subscribes to the official repository - creates a copy on their PC, makes changes locally and then pushes them to the official repository. Once these changes are in the official repository they become visible to all other developers.

[PICTURE-CENTRALIZED-WORKFLOW] 

**Distributed** In centralized workflow we had only one repository to which developers subscribe. In distributed workflow we have many. It usually suits large projects with large number of contributors. Two workflows worth examining are the ones used by GitHub and Linux communities. 

In GitHub we usually have one official repository. Developers can fork it - create a copy of this repository (which is publically available) which belongs to this developer. They as well as other people can contribute to this forked repository (from their local copies which subscribe to them). Once their project is completed (eg development of a new feature) they can request the official repository to include their changes into the official code (it's usually called creating a pull request and it's often done via some integration repository in which others can validate that new changes are well integrated with the larger system). Picture below illustrates it. 

[PICTURE-GITHUB-WORKFLOW]

Linux kernel is one of the largest projects and over time it developers its own workflow with lieutenants. Each lieutenant is responsible for integrating changes from a particular area of larger system, and later promoting them to dictator who has a final say what goes into the official repository. Below picture depicts this workflow. 

[PICTURE-LINUX-WORKFLOW]

##Git approach 

Git in its nature is a distributed system, however, flexible enough so it can easily accommodate a centralized workflow too. In contrast to many other version control systems, like SVN or Perforce, when you subscribe to the remote repository Git creates a full-featured repository on your local PC. It's not just a copy of the current code, you actually get the entire history of all files. 

Another big different compare to other popular systems is that Git, because of how it's organized, encourages certain habits - frequent check-ins (committing your changes), and branching. We'll talk about branching more in the next section - this is a very useful feature, however, it is quite cumbersome to use in other systems. 

Git is also well suited for scripting - many teams wrote their own scripts sitting on top of Git. In fact it created a platform for few commercial applications. 