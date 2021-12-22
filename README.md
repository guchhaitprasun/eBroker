# eBroker-application
### eBroker web API application using c#, dotnet core (3.1 LTS) and SQL Server to explore unit testing using xUnit.

## Getting Started
Make sure you have [installed](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) .Net Core **version 3.1** and Visual Studio 2019 on your machine and [configured](https://www.microsoft.com/en-in/sql-server/sql-server-downloads) SQL Server 2019.

Before going for the application first let's set up the databse. go to **Database** folder and follow any of the below mentioned option to configure database. 

- Use `TableCreation.sql` file and execute each sql query line by line on your machine in SQL Server Management studio (SSMS). 
- Use `eBroker.bak` file and restore database used by me using SSMS on your local machine.
- Use `eBroker.bacpac` file to restore the database in Azure SQL 

After that, you have to open the application `(eBroker.sln)` using Visual Studio 2019, Update the database connection string in `appsettings.json` with your datbase connection string and build the application. 

After build executes successfully run the application on **IIS Express** using Visual Studio

## Unit Testing & Code Coverage 
Unit testing for this project is done using [xUnit](https://xunit.net/) & Code covergae is done using [coverlet](https://www.nuget.org/packages/coverlet.msbuild/). To generate the fresh report run **Run** `ReportGenertaor.ps1` script using powershell and it will generate the report and open in your defualt browser. 

Please make sure you have report generator tool already installed. if not install it using pcakge manager console of Visual studio using this command. 
```Nuget package manager 
dotnet tool install -g dotnet-reportgenerator-globaltool
```
## Credentials
Since the application mainly focused on unit testing, Security aspect is somewhat ignored in this project. Following are the minimal credentials that will be handy while testing the api manually.

- EMAIL ADDRESS: [prasun.guchhait@nagp.com]()
- USER ID: [20210]()
- DMAT NUMBER: [1234-5678-9012-3456]()

## Business Requirement  
Trading application with users(Trader). trader should be able to do the following tasks

- Buy equity units
    - Should be able to buy from 9AM to 3PM for Monday to Friday
    - Should have sufficient funds to buy the equity
- Sell equity units
    - Should be able to sell from 9AM to 3PM for Monday to Friday
    - Can sell only those equities which the trader hold
    - Money should be added back to funds after deducing brokerage (0.05%, min 20rs)
- Add Fund
    - Can add funds any time (Above 1L charges 0.05%)

## Application Architecture
The application comprise of 3 layers. 

- **Presentation Layer** comprise of all the API contollers to cater web requests and provide with http responses. 
- **Business Layer** Responsible for all the businesss logic and validation checks
- **Data Layer** for database interaction

in addition to that there is a **Shared Layer** to store DTOs & helper classes. 




<!-- DB Scafolding

Scaffold-DbContext "Data Source=PRASUNGUCHHAIT;Database=eBroker;Trusted_Connection=True; MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Database -Context EBrokerDbContext -DataAnnotations -->
