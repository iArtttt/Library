# Library Web API

A scalable, asynchronous RESTful Web API for a library management system. This project demonstrates backend development best practices using C# and .NET, structured around Clean Architecture principles.

---

## 🚀 Architectural Design & Structure

The solution is strictly decoupled into functional layers to achieve clean separation of concerns (SoC):

* 🌐 **`Library.API`** — The entry point of the application. Contains REST controllers, routing configurations, middleware, and dependency injection setups.
* 🛠️ **`Library.Infrastructure`** — Implements core infrastructure logic, external integrations, services, and cross-cutting concerns.
* 📁 **`Library.DAL`** *(Data Access Layer)* — Handles data persistence. Includes the Entity Framework Core database context, entity configurations, and automated migrations.
* 📁 **`Library.Common`** — The shared domain layer containing core data structures, models, transfer objects (DTOs), and global constants.

---

## 🛠️ Core Tech Stack
* **Language & Runtime:** C# | .NET 8.0 / 9.0
* **API Style:** RESTful Web API (JSON payloads)
* **ORM:** Entity Framework Core
* **Design Patterns:** Clean Architecture, Repository Pattern, Dependency Injection

---

## 💻 Getting Started Locally

### Prerequisites
* .NET SDK (matching your project version)
* An IDE supporting the `.slnx` format (Visual Studio 2022 v17.10+ / JetBrains Rider)

### Installation & Run
1. Clone this repository:
   ```bash
   git clone https://github.com
   ```
2. Open the solution file: `Library.API.slnx`
3. Apply database migrations via Terminal:
   ```bash
   dotnet ef database update --project Library.DAL --startup-project Library.API
   ```
4. Run the API project (`Library.API`). Open the Swagger UI page in your browser (usually at `/swagger/index.html`) to test the endpoints.
