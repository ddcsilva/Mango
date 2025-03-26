# 🛍️ MicroStore

**MicroStore** is a fully modular e-commerce platform built with a microservices architecture using **.NET 8**, **Docker**, and modern development best practices.  
This project was developed as part of my studies in distributed systems and clean architecture during the course [".NET Core Microservices - The Complete Guide (.NET 8 MVC)"](https://www.udemy.com/course/dotnet-microservices/) by Bhrugen Patel, with custom enhancements and refinements added throughout the process.

---

## 🔧 Tech Stack

- ASP.NET Core 8 (Web APIs)
- Entity Framework Core
- RabbitMQ for asynchronous communication
- Docker & Docker Compose
- Ocelot API Gateway
- MongoDB & Redis
- IdentityServer for authentication
- AutoMapper
- Serilog for structured logging
- Swagger / OpenAPI

---

## 🧱 Microservices Structure

| Project                          | Description                            |
| -------------------------------- | -------------------------------------- |
| `MicroStore.Services.AuthAPI`    | User authentication and token issuance |
| `MicroStore.Services.ProductAPI` | Product catalog and management         |
| `MicroStore.Services.CouponAPI`  | Discount coupons and validation        |
| `MicroStore.Services.OrderAPI`   | Order processing and status tracking   |
| `MicroStore.Services.PaymentAPI` | Payment handling and status updates    |
| `MicroStore.Services.EmailAPI`   | Email notifications                    |
| `MicroStore.Web`                 | Frontend (MVC)                         |
| `MicroStore.Web.Gateway`         | API Gateway (Ocelot)                   |
| `MicroStore.MessageBus`          | Shared messaging library (RabbitMQ)    |

---

## 🚀 Running the Project

> Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/), [Docker Desktop](https://www.docker.com/products/docker-desktop)

1. Clone the repository:

   ```bash
   git clone https://github.com/ddcsilva/MicroStore.git
   cd MicroStore
   ```

2. Build and run the services with Docker:

   ```bash
   docker compose up --build -d
   ```

3. Access the services using your browser:

| Service          | URL                             |
| ---------------- | ------------------------------- |
| Product API      | `http://localhost:8001/swagger` |
| Coupon API       | `http://localhost:8002/swagger` |
| Auth API         | `http://localhost:8003/swagger` |
| Web (MVC)        | `http://localhost:5000`         |
| Gateway (Ocelot) | `http://localhost:8000`         |

> Ports may vary depending on your `docker-compose.yml` configuration.

---

## 📸 Preview

Add screenshots of the application here, such as Swagger UI, frontend pages, or service interactions.

---

## 👨‍💻 About Me

This project was developed by **Danilo**, software developer and .NET enthusiast.  
Feel free to connect with me on [LinkedIn](https://www.linkedin.com/in/ddcsilva/) or explore more of my work here on GitHub.

---

## 📄 License

This project is open-source and intended for educational and portfolio purposes only.
