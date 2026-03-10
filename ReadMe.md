# Server Side - Pandora Jewelry Store Project

Welcome to the backend repository for the **Pandora Jewelry Store**. This project provides a robust API that powers the online store, handling jewelry collections, managing personal accounts, and providing customer interactions with products.

## Overview

This application bridges between physical jewelry products and digital commerce. Instead of standard storefronts, the system offers personalized shopping, user authentication, and advanced analytics tracking designed to deliver a seamless e-commerce experience.

### Key Business Features:
- **User Authentication**: User registration and login for a personalized shopping experience.
- **Catalog Management**: Seamless sync of luxury jewelry products, including real-time inventory and price updates.
- **Order Processing**: Automated order workflow, including checkout, payments, and order tracking.

## Technical Architecture
The server is built using ASP.NET Core 9 (Web API) following modern software engineering principles to ensure scalability and maintainability.



### Core Technologies:
- **Language**: C#
- **Framework**: .NET (ASP.NET)
- **Data Management**: SQL Database (Optimized for transactional operations and direct SQL control)
- **Mapping**: AutoMapper
- **Logging**: NLog (Used for logging and tracking application activities)

### 3-Layer Architecture:
- **Application Layer**: Handles API controllers and request routing.
- **Service Layer**: Connects to the core business logic and validation segments.
- **Repository Layer**: Manages user interactions and direct database communication.
 
## API Endpoints

### Products
| Method | Endpoint                               | Description                          |
|--------|----------------------------------------|--------------------------------------|
| GET    | `/api/products`                        | Get all products                     |
| POST   | `/api/products`                        | Add a new product                    |
| GET    | `/api/products/{id}`                   | Get product details by ID            |
| PUT    | `/api/products/{id}`                   | Update product by ID                 |
| DELETE | `/api/products/{id}`                   | Delete product by ID                 |
| POST   | `/api/products/upload-image`           | Upload product image                 |

### Categories
| Method | Endpoint                               | Description                          |
|--------|----------------------------------------|--------------------------------------|
| GET    | `/api/categories`                      | Get all product categories           |
| POST   | `/api/categories`                      | Add a new product category           |

### Orders
| Method | Endpoint                               | Description                          |
|--------|----------------------------------------|--------------------------------------|
| GET    | `/api/orders`                          | Get all orders                       |
| POST   | `/api/orders`                          | Place a new order                    |
| GET    | `/api/orders/{id}`                     | Get order details by ID              |
| PUT    | `/api/orders/{id}`                     | Update order by ID                   |

### Passwords
| Method | Endpoint                               | Description                          |
|--------|----------------------------------------|--------------------------------------|
| POST   | `/api/passwords`                       | Reset password                       |

### Users
| Method | Endpoint                               | Description                          |
|--------|----------------------------------------|--------------------------------------|
| POST   | `/api/users/register`                  | Register a new user                  |
| POST   | `/api/users/login`                     | User login                           |
| GET    | `/api/users/{id}`                      | Get user details by ID               |
| PUT    | `/api/users/{id}`                      | Update user information              |
| DELETE | `/api/users/{id}`                      | Delete user by ID                    |

## Reliability & Monitoring

### Quality Assurance (Testing)
The project maintains high code quality through structured testing:
- **Unit Testing**: Testing individual service and logic in isolation.
- **Integration Testing**: Validating the full flow from the API layer down to the database.

### Monitoring:
- **Error Handling Middleware**: A centralized middleware catches all exceptions, providing consistent responses.
- **Logging**: Integrated with logging for comprehensive monitoring and debugging.
- **Traffic Analytics**: All interactions are tracked in a dedicated testing table for performance evaluation.

## Getting Started

### Prerequisites
- **.NET 9 SDK**
- **SQL Server**

## Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/zehavahgolda/WebApiProject
2. Configuration:
 Update the connection string in appsettings.json to point to your SQL Server instance.
3.Restore Dependencies:
	dotnet restore
4.Run the Project:
	dotnet run