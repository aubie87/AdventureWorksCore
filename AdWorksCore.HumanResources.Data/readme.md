## AdWorksCore.HumanResources.Data

### Summary
This project holds the Entity Framework Core 2 Context and entity classes representing
the Human Resources domain of the Adventure Works database.

#### Prerequisites

* Visual Studio 2017 15.7 or higher
* SqlServer 2014 or higher
* AdventureWorks 2014 sample database (bak file)

#### Initial Project Setup

1. Create a .Net Standard Library project.
1. Delete the created class.
1. Either in Nuget or in Package Manager Console, install the following packages:
   1. Microsoft.EntityFrameworkCore.SqlServer
   2. Microsoft.EntityFrameworkCore.Design
   3. Microsoft.EntityFrameworkCore.Tools
4. Set an executable app as the startup app: web, console
   * The EF Tools use the executable project to run the tools with.

*Note:* the *.Design and *.Tools packages above could be installed in a dedicated console
app to avoid 'carrying' these packages into production servers. The console app would need
to be set as the startup project when the EF Tooling is run. Having a console app
has the added benefit of allowing experimentation with EF Core entity usage.

#### Database setup

Restore the AdventureWorks 2014 database into either a localdb or a running version of
SqlServer. Ideally this should be run on the local computer for ease of development.

Once setup, determine the connection string to the database as configured above.

#### Running the EF Core 2 Tools - Database First

Helpful docs from Microsoft:
[EF Core Package Manager Console Tools](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell)

Open the Package Manager Console and run the following command to verify the tooling
is properly setup.
```
PM> Get-Help About_EntityFrameworkCore
```
Parameter information should be displayed about the EF Tooling commands. If not then visit
the Ninja of Entity Framework, Julie Lerman's website:
[thedatafarm.com](http://thedatafarm.com/data-access/no-executable-found-matching-command-dotnet-ef/)
We are attempting to use the Package Manager Console (PMC) version of the tools.

With the tooling working we can now reverse engineer any supported database we like. For
this tutorial we will focus on the `Person` and `HumanResource` schemas in the
AdventureWorks 2014 database for a Domain Driven Design approach. Run the following 
command from PNC with the Default Project set to the new .Net Standard project created 
above. In our case it is called `AdWorksCore.HumanResources.Data`.

```
PM> Scaffold-DbContext -Connection "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2014" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Entities" -Context HrContext -Schemas HumanResources, Person
```

If you see a startup project error then be sure to set a web or console app as the solution
startup project. A successful command will product a long commentary on the artifacts found
in the database including relations, indexes and keys.
