Blog API .net core 2.2
========

Used technologies and structures
-------------------------------
- versioning
- Jwt bearer authentication
- swagger documentation,
- serilog logger
- EF Core Repository pattern 
- Db Migration
- dependency injection
- project layers
- Mssql

Connection
----------
set ```"DefaultConnection" ="";``` in ```appsettings.json ``` 


migration
----------

edit ```var connectionString ="";``` in ```DbContextFactory.cs ``` and go ```.....\Blog\src\Libraries\Blog.Data ``` path open on CLI and run commands

```sh
#restore Blog.Data project
...Blog.Data_> dotnet restore

#build Blog.Data project
...Blog.Data_> dotnet build

#add Initial name migrations 
...Blog.Data_> dotnet ef migrations add Initial

#Update migrations on database
...Blog.Data_> dotnet ef database update
#dotnet ef database update Initial

```



