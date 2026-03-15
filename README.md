# SQL + C# Engineering Assessment

This project implements a simple order management system using SQL Server and C#.

## Requirements

- SQL Server 2019 or later
- .NET 8 SDK

## Setup Database

Run the SQL script:

sql/answers.sql

This will create the database `AssessmentDB` and insert sample data.

## Run Application

Navigate to the console project:

cd src/CustomerConsoleApp

Run the application:

dotnet run

The program will prompt for a minimum spend value and return customer order summaries.

## Project Structure

src/ – Console application source code  
sql/ – SQL database schema and queries  
ssrs/ – SSRS reporting answers

## Features

- Customer order summary
- SQL aggregation queries
- Input validation
- Parameterized SQL queries
- Basic logging and error handling
