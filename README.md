# Book Store Management System

A modern ASP.NET Core Web API project built using **Clean Architecture**, **Entity Framework Core**, and **ASP.NET Identity**.

## Features

### User Management

* User Registration
* User Login
* ASP.NET Identity Integration
* Role Management
* Authentication & Authorization

### Book Management

* Create, Update, Delete, and Retrieve Books
* Book-Author Relationship
* Book-Category Relationship
* Book-Publishing House Relationship

### Author Management

* Create, Update, Delete, and Retrieve Authors

### Category Management

* Create, Update, Delete, and Retrieve Categories

### Publishing House Management

* Create, Update, Delete, and Retrieve Publishing Houses

## Architecture

This project follows the Clean Architecture pattern and is divided into the following layers:

### Domain Layer

Contains:

* Entities
* Core Business Models
* Domain Logic

### Application Layer

Contains:

* Interfaces
* Repository Contracts
* Application Services

### Infrastructure Layer

Contains:

* Entity Framework Core
* SQL Server Integration
* Repository Implementations
* Identity Configuration

### API Layer

Contains:

* Controllers
* Authentication Endpoints
* Swagger Documentation
* Dependency Injection Configuration

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* Swagger / OpenAPI
* Repository Pattern
* Dependency Injection
* Clean Architecture

## Database Entities

* ApplicationUser
* Role
* Book
* Author
* Category
* PublishingHouse

## Project Structure

BookStore.API

BookStore.Application

BookStore.Domain

BookStore.Infrastructure

## Future Enhancements

* JWT Authentication
* Refresh Tokens
* CQRS Pattern
* MediatR
* Unit of Work Pattern
* Generic Repository
* Docker Support
* Logging & Monitoring

## Getting Started

1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Run migrations:

   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```
4. Run the application.
5. Open Swagger UI and start testing the APIs.

## Author

Developed as a Clean Architecture learning project for building scalable and maintainable ASP.NET Core applications.
