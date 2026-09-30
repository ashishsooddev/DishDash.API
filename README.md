# DishDash

DishDash is a food ordering REST API built using ASP.NET Core Web API and Entity Framework Core.

The application allows customers to order food from restaurants and provides CRUD operations for customers, restaurants, food items, and orders.

## Technologies Used

- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- Swagger / OpenAPI
- REST API
- Git and GitHub

## Project Architecture

The project follows a simple N-Tier architecture.

### DishDash.API
Contains:
- API controllers
- API configuration
- Swagger/OpenAPI configuration

### DishDash.BLL
Contains:
- Business logic
- Service interfaces
- Service implementations

### DishDash.DAL
Contains:
- Entity Framework Core DbContext
- Database configuration
- Entity relationships
- Seed data
- EF Core migrations

### DishDash.Models
Contains:
- Entity classes
- DTO classes

## Main Entities

The application contains five main entities:

1. Customer
2. Restaurant
3. FoodItem
4. Order
5. OrderItem

### Relationships

- One Customer can have many Orders.
- One Restaurant can have many FoodItems.
- One Restaurant can have many Orders.
- One Order can contain many OrderItems.
- One FoodItem can appear in many OrderItems.
- Orders and FoodItems have a many-to-many relationship through OrderItem.

## Features

- Customer CRUD operations
- Restaurant CRUD operations
- Food item CRUD operations
- Order creation and management
- DTOs for API requests and responses
- Data validation
- Async database operations
- Pagination for customers
- Order searching by status and minimum amount
- Entity Framework Core Code First
- Database migrations
- Seed data
- Swagger/OpenAPI documentation
- ProblemDetails error handling
- RESTful API endpoints

## API Endpoints

### Customers

```text
GET    /api/Customers
GET    /api/Customers/{id}
POST   /api/Customers
PUT    /api/Customers/{id}
DELETE /api/Customers/{id}
