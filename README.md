# Order & Inventory Microservices System

## Overview
This project is an Order & Inventory Microservices System built using .NET and RabbitMQ. It follows a 3-tier architecture, separating concerns into different services for better scalability and maintainability.

## Architecture
The system consists of two main microservices:
- **Order Service**: Manages order-related operations such as creating, updating, and retrieving orders.
- **Inventory Service**: Manages inventory-related operations such as adding, updating, and retrieving inventory items.

Both services communicate with each other through RabbitMQ for event-driven messaging.

## Project Structure
```
order-inventory-microservices
├── OrderService
│   ├── src
│   ├── OrderService.csproj
│   ├── appsettings.json
│   └── appsettings.Development.json
├── InventoryService
│   ├── src
│   ├── InventoryService.csproj
│   ├── appsettings.json
│   └── appsettings.Development.json
├── Shared
│   ├── Models
│   └── Messaging
├── docker-compose.yml
└── README.md
```

## Setup Instructions
1. **Clone the Repository**
   ```
   git clone <repository-url>
   cd order-inventory-microservices
   ```

2. **Docker Setup**
   Ensure you have Docker installed. Use the following command to start the services:
   ```
   docker-compose up
   ```

3. **Run the Services**
   Each service can be run independently. Navigate to the respective service directory and run:
   ```
   dotnet run
   ```

## Usage
- **Order Service**: Access the Order Service API at `http://localhost:<port>/api/orders`.
- **Inventory Service**: Access the Inventory Service API at `http://localhost:<port>/api/inventory`.

## Contributing
Feel free to submit issues or pull requests for improvements or bug fixes.

## License
This project is licensed under the MIT License.