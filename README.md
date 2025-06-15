# Order & Inventory Microservices Application (C# .NET + RabbitMQ)

This project demonstrates an event-driven microservices architecture using **.NET Core**, **RabbitMQ**, and **Docker**. It simulates a real-world backend system. It follows a 3-tier architecture with decoupled services for **Order Management** and **Inventory Tracking**, communicating asynchronously through message queues.

---

## Architecture Overview

- **OrderService**: Handles order creation and emits events
- **InventoryService**: Listens for order events and updates stock levels accordingly
- **CustomerService**: Manages customer data and emits customer-related events
- **RabbitMQ**: Message broker to decouple services and enable asynchronous event communication
- **Shared**: Contains common message/event contracts and RabbitMQ utility services
- **CustomerPortal**: Frontend MVC web app for customers to place and view orders
- **InventoryPortal**: Frontend MVC web app for inventory managers to monitor and update stock

Each microservice and portal is independently deployable. Backend services communicate via events, and frontends consume backend APIs. The system follows **event-driven architecture** and **domain-driven design** principles.

---

## Tech Stack

- **.NET 7 / ASP.NET Core Web API**
- **Entity Framework Core**
- **RabbitMQ** (message broker)
- **Docker + Docker Compose**
- **PostgreSQL** (for service persistence, optional)
- **Postman / Swagger** (for testing APIs)

---
