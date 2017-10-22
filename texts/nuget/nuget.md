# Introduction to NuGet

## Introduction

The design of modern operating systems and programming languages promotes modularized architecture and code reuse. This approach, when applied to our own projects, can significantly improve speed of their delivery. Whenever we have to write a code that someone else has already written, we don't have to reimplement it ourselves. All we have to do is to reference their library in our project. This is what we call creating a dependency -- our project from now on depends on someone else's work. 

In this article we describe how we can manage dependencies when developing in .NET ecosystem using NuGet. 

We will start with step by step tutorial on how to use other people's libraries when working on your own project and then gradually move to more advanced topics. We will look into the package and analyze how it's internally constructed and later demonstrate few techniques which you can use to create your own packages that later other folks can use. 

## NuGet Ecosystem  

System which allows you to easily manage packages in .NET projects is called NuGet. It's fully integrated with Visual Studio, so whenever you install it on your PC you already have everything you need to start working with it. 

Visual Studio NuGet addin allows you to:

* Add new dependencies to your project.
* Remove already existing dependencies from your project.
* Update already existing dependencies in your project.

Dependencies are represented in form of packages. This is just a way to standardize the whole process. People work on projects which vary in their nature. In order to create a tool which can allow you to easily apply them to your projects, a common interface was introduced -- which is called a package. 

In a nutshell package is made up of your files (be that executables, DLLs, or even code or images) and metadata which describes your package (who's the author, when it was released, its version, etc.).

In addition to Visual Studio addin, we've also got NuGet command line interface (CLI) which allows you to create packages and NuGet gallery. Gallery is a place where people and organizations can publish their packages. It's publicly available, however, you can also create a private gallery for the benefit of your organization. 

In summary, the evolution of a package can be described as follows. You can create a package, that package can then be uploaded to the gallery and finally other people can reuse your work in their projects by referencing it from NuGet gallery. 

## NuGet: First Steps 

Let's do a quick demo -- let's create a simple Calculator project and add some unit tests. In order to implement these tests let's use XUnit framework. If you are not familiar with XUnit this page can be a good starting point, https://xunit.github.io/ 

In my examples I'll be using Visual Studio 2017, however, the same process can be mimicked in almost exactly the same way on older versions. 

We create 2 projects, Calculator and Calculator.Test and add a reference as shown below. Calculator itself has one module with a function we want to test. 

![PICTURE-01](https://github.com/karolgornicki/Articles/blob/master/img/nuget/01.png)

Next we need to add XUnit dependency to our test project. To do that we right-click on our project and select Manage Nuget Packages… from the drop-down. 

![PICTURE-02](https://github.com/karolgornicki/Articles/blob/master/img/nuget/02.png)

This will open a window in a central pane which allows us to browse available packages that we can add to our project. Right now we want to find XUnit package. To do that we select Browse option, make sure we use nuget.org as a source and type in search bar "xunit". 

![PICTURE-03](https://github.com/karolgornicki/Articles/blob/master/img/nuget/03.png)

After hitting Install button NuGet will add this package to your project. Confirm that you are OK with the proposed changes.

![PICTURE-04](https://github.com/karolgornicki/Articles/blob/master/img/nuget/04.png)

Upon completion you can check the output tab to make sure that the installation completed successfully. If you expand your project in Solution Explorer you can see that new dependencies were added. Also, when browsing in NuGet Package Manager you can see which packages are already installed by looking at green check mark. 

![PICTURE-05](https://github.com/karolgornicki/Articles/blob/master/img/nuget/05.png)

In order to be able to run XUnit in Visual Studio we also have to add one more dependency, "xunit.runner.visualstudio".

Once this is done we can add our test function and rebuild the whole solution (necessary step after adding XUnit for tests to be discovered). 

```fsharp
namespace Calculator.Tests
open Xunit
type CalculatorTests() =
    [<Fact>]
    let add_1and2_returns3 () =
       // Act
       let result : int = CalculatorFunctions.add 1 2
       // Assert
       Assert.Equal( 3, result )
```

Let's quickly have a look what changes NuGet does to our project structure. Dependencies are referenced in packages.config file. 

```
<?xml version="1.0" encoding="utf-8"?>
<packages>
 <package id="System.ValueTuple" version="4.3.0" targetFramework="net452" />
 <package id="xunit" version="2.3.0" targetFramework="net452" />
 <package id="xunit.abstractions" version="2.0.1" targetFramework="net452" />
 <package id="xunit.analyzers" version="0.7.0" targetFramework="net452" />
 <package id="xunit.assert" version="2.3.0" targetFramework="net452" />
 <package id="xunit.core" version="2.3.0" targetFramework="net452" />
 <package id="xunit.extensibility.core" version="2.3.0" targetFramework="net452" />
 <package id="xunit.extensibility.execution" version="2.3.0" targetFramework="net452" />
 <package id="xunit.runner.console" version="2.3.0" targetFramework="net452" developmentDependency="true" />
 <package id="xunit.runner.visualstudio" version="2.3.0" targetFramework="net452" developmentDependency="true" />
</packages>
```

When NuGet addin is looking for updates it simply compares versions for referenced dependencies against the latest available in the gallery. As you can see, NuGet manages dependencies on a project by project basis.

All dependencies are stored in separate folder. So, if two or more projects rely on the same dependency (and the same version). Only one copy will be created and simultaneously used in all projects that require it. 

![PICTURE-06](https://github.com/karolgornicki/Articles/blob/master/img/nuget/06.png)

## Using Package Manager Console 

As an alternative to using UI you can use Package Manager Console. You can access it by going to View in a top-window ribbon. Next select Other Windows and next Package Manager Console. 

Everything you can do in the UI, you can also accomplish using commands. In fact, command interface is slightly more powerful than the UI. If you are curious, it's powered by PowerShell. 

Since packages are managed on a project basis, when you installing packages you have to make sure that you selected the right project in the drop-down list. 

![PICTURE-07](https://github.com/karolgornicki/Articles/blob/master/img/nuget/07.png)

If you'd like to learn more what commands are available head to https://docs.microsoft.com/en-us/nuget/tools/package-manager-console 

## Where Are Packages Coming From?

When we were adding XUnit to our project as a dependency, we set source to "nuget.org". This is a public gallery to which everyone can upload their own packages and others can start using it. The service is completely free of charge. You can browse these projects either using Visual Studio tools (UI or console) or on their webpage, https://www.nuget.org/ 

To browse packages go to Packages tab in a top-window menu.

Later we will show you how you can push your own packages and also how you can create such host by yourself and even use them as source for packages in Visual Studio.

## Package Internals 

In order to understand what packages are made up of let's look at one of the packages we used in our project. Let's pick a relatively simple one, for example xunit.assert.2.3.0. 

Navigate to the right folder (packages/xunit.assert.2.3.0). It contains

![PICTURE-08](https://github.com/karolgornicki/Articles/blob/master/img/nuget/08.png)

nupkg if a file that interests us. It contains all the information about this package. So let's double click on it. It will open it in NuGet Package Explorer. 

![PICTURE-09](https://github.com/karolgornicki/Articles/blob/master/img/nuget/09.png)

As you can see there are 2 parts of it:

* Content 
* Metadata (description of the package) 

In order to see the exact content of that file expand lib. Section of the left-hand side doesn't really show all available options so let's go to Edit and Edit Metadata. Now we can clearly see what author of that package can add. If you scroll to the bottom, we are even able to add dependencies in form of other packages. 

OK, so this is the content of nupkg file, but how is it encoded? Let's close NuGet Package Explorer and go back to the folder. Rename this file's extension to zip. Nupkg is just a fancy name for a zip file at the end of the day. 

If we open a zip file we see the following content. 

![PICTURE-10](https://github.com/karolgornicki/Articles/blob/master/img/nuget/10.png)

If you open xunit.assert.nuspec in a text editor you'll see that it's just an XML file with the exact same metadata that we saw in NuGet Package Explorer. The content of that package is stored in lib folder. That's just the convention. 

## How to Create NuGet Package?

Let's first try to manually create a NuGet package. For the purpose of the demo, suppose we would like to export our module CalculatorFunctions with add function as a package. 

Step 1, let's open NuGet Package Explorer. Welcome screen gives us an option to create a package, let's select that. 

![PICTURE-11](https://github.com/karolgornicki/Articles/blob/master/img/nuget/11.png)

Let's update metadata with the appropriate title and others. Now, in order to add content to our package we select Content, Add and choose what we want to add. For the purpose of this demo, let's choose Lib Folder. 

![PICTURE-12](https://github.com/karolgornicki/Articles/blob/master/img/nuget/12.png)

It created lib folder for us. When we right-click on that we're presented with quite a few options. In short, when you create a package you can provide content which target various frameworks. Let's choose 4.5.2 and in that folder let's add our Calculator.dll file. 

![PICTURE-13](https://github.com/karolgornicki/Articles/blob/master/img/nuget/13.png)

As you know, we wrote this library in F#. So, if someone would like to use it in C# project they should be able to do so. Therefore, we also have to include a dependency for FSharp.Core (as it's not part of standard C# library project). 

To do that we have to go back to editing Metadata section, and scroll down to the very end. Click edit dependencies button. If you click the button indicated in the image below we can browse NuGet feeds for relevant packages. 

Next we search for FSharp.Core, click on the relevant package and select "open". If you want you can browse through different versions if you want to reference not the most recent release. 

![PICTURE-14](https://github.com/karolgornicki/Articles/blob/master/img/nuget/14.png)

![PICTURE-15](https://github.com/karolgornicki/Articles/blob/master/img/nuget/15.png)

Pay extra attention whether you are including pre-release packages. It may not be the best idea when releasing production-ready code! 

Click OK on the next window and save changes to Metadata section. Now you can see that FSharp.Core is listed as one of dependencies. 

Let's now create a package. It's super easy, all you have to do is to go to File and click Save As. 

Rather than saving at default location let's create a folder which will store only this package. Next we will use that folder to simulate an alternative NuGet package feed. 

Let's create a new solution with a C# project, and let's add our newly created NuGet package. Don't worry that we didn't publish it to NuGet gallery yet. We will show you how to use a local folder as a source. 

First, let's open NuGet Package Manager. Next to Package Source drop-down list there is Settings icon. Let's add another source pointing to the folder in which we saved our new package. 

![PICTURE-16](https://github.com/karolgornicki/Articles/blob/master/img/nuget/16.png)

![PICTURE-17](https://github.com/karolgornicki/Articles/blob/master/img/nuget/17.png)

Now if we go back to the previous view, select "Local NuGet Feed" and switch to browse we can see our package! Let's install it and see if it works.

If NuGet addin can't see the package it's probably because it's still open in NuGet Package Explorer which locks the file. Simply close that application and all should be good.

```csharp
using System;
namespace Demo
{
    class Program
    {
       static void Main(string[] args)
       {
           Console.WriteLine(CalculatorFunctions.add(1, 2));
           Console.ReadKey();
       }
    }
}
```

## Creating Packages Programmatically 

Previously introduced approach to create package was mainly to demonstrate how packages work. Doing it by hand every time you have to make a release is not practical.

For production purpose we would use a command line interface. NuGet provides an EXE which can do all of these features -- create a package and upload it to a gallery completely outside of Visual Studio context. 

In order to get the EXE go to Visual Studio NuGet addin and install "nuget.commandline". Now, go to packages folder and in tools folder we've got EXE. 

In Command Prompt we can run 

```
NuGet.exe help 
```

to get info how we can use CLI.

## Advanced Topics 

If you got that far I presume you've done one or two demos on your own and have a working knowledge of how it works. In this last section we will give a brief instructions how more advanced tasks can be accomplished. 

### How to Publish to nuget.org? 

It's fairly easy. Go to https://www.nuget.org/ and create an account which will be validated via email. Next create API key (top-right corner, click on your name and there should be a link in a drop-down list).

Now you can upload package either via NuGet Package Explorer. Simple go to File, Publish and fill in the form. Then you can go to nuget.org and under your username select manage packages where your new package will be listed. You can also search for your package to make sure others can find it too. 

Alternative is to use CLI. Help command can guide you how to do it. It's easy.

### How to Delete Old Package from nuget.org? 

You can't. NuGet doesn't allow you to delete. All you can do is hide a package so it won't show up in search results. That's why, when you upload a package it must have a unique version.

If you have a private feed (for example restricted to your organization only) different rules might be put in place and you may be allowed to delete.

### Creating a Package with Project as a Source

That's also fairly easy. Let's say you have a project (Calculator). Open command prompt and navigate to that folder. Run NuGet.exe spec 

```
c:\git\Articles\src\NuGetExample\Calculator>..\packages\NuGet.CommandLine.4.3.0\tools\NuGet.exe spec
Created 'Calculator.nuspec' successfully.
```

Next with pack command. When running for the first time you'll likely run into errors (see below). 

```
c:\git\Articles\src\NuGetExample\Calculator>..\packages\NuGet.CommandLine.4.3.0\tools\NuGet.exe pack
Attempting to build package from 'Calculator.fsproj'.
MSBuild auto-detection: using msbuild version '14.0' from 'C:\Program Files (x86)\MSBuild\14.0\bin\amd64'.
Packing files from 'c:\git\Articles\src\NuGetExample\Calculator\bin\Debug'.
Using 'Calculator.nuspec' for metadata.
Authors is required.
Description is required.
```

That's because nuspec file is referencing project variables, and some of them are not set but are mandatory for package creation process. To fix that go to Assembly.fs file and update Description and Company. Then rebuild the project and try again pack command. Now all should be good. 

```
c:\git\Articles\src\NuGetExample\Calculator>..\packages\NuGet.CommandLine.4.3.0\tools\NuGet.exe pack
Attempting to build package from 'Calculator.fsproj'.
MSBuild auto-detection: using msbuild version '14.0' from 'C:\Program Files (x86)\MSBuild\14.0\bin\amd64'.
Packing files from 'c:\git\Articles\src\NuGetExample\Calculator\bin\Debug'.
Using 'Calculator.nuspec' for metadata.
Found packages.config. Using packages listed as dependencies
Successfully created package 'c:\git\Articles\src\NuGetExample\Calculator\Calculator.1.0.0.nupkg'.
```

Most likely you'll get some warnings but they are easy to fix. 

### How to Create a Symbol Package? (So the User Can Debug It)

```
NuGet.exe pack -symbols 
```

### How to Create Package that Modifies Existing Files in the Project? 

See here, https://docs.microsoft.com/en-us/nuget/create-packages/source-and-config-file-transformations 

### How to Create a Private NuGet Feed? 

* Create MVC project 
* Install "nuget.server" package. 
* Build. 
* Since email is not set, you have to find a message with a link in local files to authenticate account (for testing purpose).

For more detailed description see, https://docs.microsoft.com/en-us/nuget/hosting-packages/nuget-server 
