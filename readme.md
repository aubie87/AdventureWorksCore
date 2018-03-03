## ASP.NET Core 2 Tutorial Capturing Various Tools and Techniques

### Summary
The initial goal is to build an ASP.NET Core project that accesses the AdventureWorks database. Within the solution there will be multiple sub-projects separating the front-end technologies from back-end support requirements including data access and business logic. Domain Driven Design (DDD) will also drive separation of business needs and usage.

Ideally we will be able to demonstrate multiple techniques for performing the same task so we can compare and contrast differing solutions. For example we would like to see how Razor and Angular would implement the same UI using the same backend data and classes. Additionally, we would also like to explore basic web API design compared to an OData 4 API.

Unit testing and TDD will be implemented intentionally with older versions of some libraries and frameworks so that upgrades can be easily verified and repaired as needed. With unit testing in place, any refactoring should quickly catch breaking changes. Properly implemented and tested classes and modules should also help enforce the 'S' and 'O' in SOLID design.

### Prerequisites
We expect the initial completed solution to require the following tools, technologies and libraries. This list will expand and functionality is added to the solution.

* Backend Tools and Libraries
    * [Visual Studio 2017](https://www.visualstudio.com/vs/)
    * [Visual Studio Code](https://code.visualstudio.com/)
    * nuget package manager - for backend libraries and tooling only.
    * [.NET Libraries](https://www.microsoft.com/net/download/visual-studio-sdks)
        * .NET Standard 2.0
        * .NET Framework 4.7.1
        * .NET Core 2.1.4
        * Entity Framework Core 2.0
    * [Automapper](http://automapper.org/) - mapping database entities to web API classes
* Frontend Tools and Libraries
    * MVC/Razor - .NET style web experience and host for Angular
    * [Node and NPM](https://nodejs.org/en/) - cmd line dev tools
    * [Angular CLI 1.4.10](https://cli.angular.io/) - Angular cli addon to npm (version generates 4.4.6 code)
    * [Angular 4.4.6](https://v4.angular.io/docs) - LTS version
    * [Bootstrap 3.3.4](https://getbootstrap.com/docs/3.3/) - latest 3.3 version

#### Notes
Angular 5.0+ requires Bootstrap v4. 

### Initial Solution Creation
Create the initial solution as a "Blank Solution" so that additional projects can be easily added with correct namespaces. Choose "Other Project Types/Visual Studio Solutions" on the left and then the "Blank Solution" template. Give the solution a good "BaseSolutionName" that new dotted project names can then be added to.

### Git Tips for Initial commit/push existing repo

1. Create project and commit at interesting steps.
2. Create a new repository (github for example) with no starter files.
3. From command line in folder where .git is (or a subdirectory) enter the following commands:
   * `> git remote add origin git@github.com:user/repo.git`
   * `> git push -u origin master`

Where the `user` is your github userid and the `repo` is the name of your new repo.

### Add New Web Project - ASP.NET Core 2

1. Right click the solution node and select **Add/New Project...** inside the solution
   1. **Visual C#/.NET Core** on the left filter menu.
   2. **.NET Framework 4.7.1** from the SDK version drop down menu at the top.
   3. Select **ASP.NET Core Web Application** template.
   4. Enter a name and location of the new project at the bottom of the dialog. For example "BaseName.Web".
2. Select the project template for this type of application.
   1. At the top choose **.NET Core** and **ASP.NET Core 2.0**
   2. Select the **Empty** template. This will create a blank project that we will add to.
   3. **Enable Docker Support** should be unchecked.

Run Build/Build Solution to verify the project builds cleanly.

Running the initial project has some options to note. It can be run via IIS Express or via various browsers. If you choose to debug via a browser then it can be beneficial to use a browser that you do not use day to day. This eliminates cookies, bookmarks and other extraneous data your normal browser would bring with it.

### Add Data Project - .NET Standard 2.0 (under review)

1. Right click the solution node and select **Add/New Project...** inside the solution
1. **Visual C#/.NET Core** on the left filter menu.
2. **.NET Framework 4.7.1** from the SDK version drop down menu at the top.
3. Select **Class Library (.NET Standard)** application template.
4. Enter a name and location of the new project at the bottom of the dialog. For example "BaseName.Data".

