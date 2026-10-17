# RestaurantOS 🍽️

A REST API for managing restaurant operations — tables, menu, orders, billing, inventory, employees, and staff accounts — built with **.NET 10** following **Clean Architecture** and **CQRS**.

## ✨ Features

- **Authentication & Authorization** — Register, login, refresh & revoke tokens (JWT + ASP.NET Core Identity), with role-based access for `Admin`, `Waiter`, `Chef`, and `Cashier`.
- **Tables** — Manage restaurant tables and their availability status.
- **Menu** — Categories and menu items (create, update, delete, browse with paging).
- **Orders** — Create orders per table, add items, and track them through a status flow: `Pending → Confirmed → Preparing → Ready → Served → Completed` (or `Cancelled`), enforced by a domain state machine.
- **Billing** — Each completed order gets a bill with payment status tracking.
- **Inventory** — Track stock items and quantities.
- **Employees** — Manage employee records.

## 🛠️ Tech Stack

| Area | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 10) Web API |
| Architecture | Clean Architecture + CQRS |
| Mediation | MediatR |
| Validation | FluentValidation (pipeline behavior) |
| Database | SQL Server via Entity Framework Core 10 (Code-First + Migrations) |
| Identity | ASP.NET Core Identity + JWT Bearer + Refresh Tokens |
| API Docs | OpenAPI (`/openapi/v1.json` in Development) |

## 🏗️ Architecture

```
RestaurantOS.API            → Controllers, middleware, DI & configuration (presentation)
RestaurantOS.Application    → CQRS commands/queries, DTOs, validators, interfaces
RestaurantOS.Domain         → Entities, enums, and domain logic (no dependencies)
RestaurantOS.Infrastructure → EF Core DbContext, repositories, Identity, migrations
```

Dependencies flow inward: API → Application/Infrastructure → Domain. The domain layer holds the business rules (e.g., order status transitions) and knows nothing about EF Core or HTTP.

## 📁 Project Structure

```
RestaurantOS.API/
├── Controllers/        # Auth, Tables, MenuItems, Categories, Orders, Inventory, Employees
├── Middleware/         # Global exception handling
└── Program.cs          # JWT setup, role seeding, DI wiring

RestaurantOS.Application/
├── Features/           # CQRS: Commands, Queries, Handlers per feature
├── DTOs/               # Request/response models
├── Validation/         # FluentValidation validators + ValidationBehavior
├── Interfaces/         # IAuthService, IUnitOfWork, repositories
└── Mappings/           # Domain ↔ DTO mapping extensions

RestaurantOS.Domain/
├── Entities/           # Order, OrderItem, Bill, MenuItem, Category, RestaurantTable, InventoryItem, Employee
└── Enums/              # OrderStatus, PaymentStatus, TableStatus

RestaurantOS.Infrastructure/
├── Identity/           # ApplicationUser, AuthService, RefreshToken
├── Persistence/        # RestaurantDbContext, entity configurations, EF migrations
└── Repositories/       # GenericRepository, MenuRepository, OrderRepository, UnitOfWork
```

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or LocalDB, which is the default in `appsettings.json`)

### Configuration

The JWT signing key is read from configuration and must be at least 32 bytes. Store it with User Secrets (never commit it):

```bash
cd RestaurantOS.API
dotnet user-secrets set "Jwt:Key" "your-super-secret-key-at-least-32-characters-long"
```

Optionally adjust the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RestaurantOSDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Run

```bash
# apply database migrations
dotnet run --project RestaurantOS.API --ef-migrate    # or: dotnet ef database update --project RestaurantOS.Infrastructure --startup-project RestaurantOS.API

# start the API
dotnet run --project RestaurantOS.API
```

Then open the OpenAPI docs at the printed URL (e.g. `http://localhost:5xxx`) in Development mode.

> The app seeds the roles **Admin / Waiter / Chef / Cashier** on startup.

## 🔌 API Overview

| Endpoint | Description |
|---|---|
| `POST /api/auth/register` `/login` `/refresh` `/revoke` | Account & token management |
| `GET/POST/PUT/DELETE /api/tables` | Manage tables & status |
| `GET/POST/PUT/DELETE /api/categories` | Manage menu categories |
| `GET/POST/PUT/DELETE /api/menuitems` | Manage menu items |
| `POST/GET /api/orders` | Create orders & track status |
| `GET /api/inventory` `POST /api/inventory` | View & add stock items |
| `GET/POST/PUT/DELETE /api/employees` | Manage employees |

## 📄 License

For educational/portfolio use.
