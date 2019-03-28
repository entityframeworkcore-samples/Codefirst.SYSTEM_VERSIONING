# Code first with System Versioning Entity Framework Dotnet Core 2x simple guide

## 1. Startup 
This guide it's re-using the sample done in this [other CodeFirst Sample](https://github.com/entityframeworkcore-samples/CodeFirst)

First step is execute the powershell script "1.UpdateDatabase.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef database update --startup-project ..\ConsoleApp

Using Package Manager Console select the DAL.JecaestevezApp.csproj and execute

> PM> update-database –verbose
