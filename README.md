#Criminal Case Investigation Management System (CCIMS)

A role-based ASP .NET Core MVC Application designed to manage criminal cases, investigation logs and audit trails.

## Features
- Role based access (Admin, Investigator)
- Case lifecycle management (Open -> Under Investigation -> Closed)
- Immutable investigation logs for audit integrity
- Automatic logging of case status changes
- Investigator identity attached to all logs
- ASP .NET Identity authentication
- Entity Framework Core with migrations

## Tech Stack
- ASP .NET Core MVC (.net 8)
- Entity Framework Core
- SQL Server LocalDB
- ASP .NET Identity

## Business Rules
- Only investigators can add investigation logs
- Closed cases are read-only
- Investigation logs cannot be edited or deleted
- All status changes are automatically logged

## Why this project
This project was built to understand real-world backend patterns such as authorization, auditing and domain-driven design in ASP.NET Core.

## Future Improvements
-Investigator dashboard
Evidence attachment system
Authorization policies
