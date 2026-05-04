# AbySalto Junior — Restaurant Order Management API

A .NET 9 Web API for managing restaurant orders, built as part of the AbySalto junior developer task.

## Tech Stack

- **.NET 9** — Web API
- **Entity Framework Core** — Code-first ORM
- **SQL Server Express** — Database
- **Swagger / OpenAPI** — API documentation and testing

## Prerequisites

Before running the project, make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (free edition)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install EF Core tools globally if you haven't already:

```bash
dotnet tool install --global dotnet-ef
```

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/dmisadin/junior.net.git
cd junior.net
```

### 2. Configure the connection string

Open `appsettings.Development.json` and update the connection string to match your SQL Server Express instance name:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_MACHINE_NAME\\SQLEXPRESS;Database=AbySaltoJunior;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_MACHINE_NAME` with your actual computer name. You can find it by running in PowerShell:

```powershell
$env:COMPUTERNAME
```

Example connection string:

```json
"DefaultConnection": "Server=DESKTOP-GKDVOKL\\SQLEXPRESS;Database=AbySaltoJunior;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 3. Run the application

```bash
dotnet run --launch-profile https
```

On startup the application will automatically:
- Create the database if it does not exist
- Apply all pending migrations
- Seed 10 customers and 10 products into the database

No manual migration steps required.

### 4. Open Swagger UI

Once running, open your browser and navigate to:

```
https://localhost:7056 
```

The port is displayed in the terminal output after startup. Swagger UI loads at the root URL and lists all available endpoints.

> **Note:** Always use the `https` URL shown in the terminal. Using `http` will result in a network error due to HTTPS redirection.

---

## Running Migrations Manually (Optional)

If you prefer to apply migrations manually instead of relying on auto-migration on startup, comment out the following block in `Program.cs`:

```csharp
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
```

Then run migrations manually from the project root:

```bash
# Apply all migrations and create the database
dotnet ef database update

# To roll back to a specific migration
dotnet ef database update MigrationName

# To list all migrations and their status
dotnet ef migrations list
```

---

## API Overview

### Orders

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Order` | Get all orders. Use `?sort=2` to sort by total amount descending (options: 0,1,2)|
| `GET` | `/api/Order/GetAllForCustomer/{customerId}` | Get all orders for customer. Use `?sort=2` to sort by total amount descending (options: 0,1,2)|
| `GET` | `/api/Order/{id}` | Get a single order by ID |
| `POST` | `/api/Order` | Create a new order |
| `PATCH` | `/api/Order/{id}/status` | Update order status |

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Product` | Get all available products |
| `POST` | `/api/Product` | Create a new product |

---

## Example Requests

### Create an order

```json
POST /api/Order
{
  "customerId": 1,
  "paymentMethod": 0,
  "currency": 0,
  "note": "Extra napkins please",
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 5, "quantity": 1 }
  ]
}
```

### Update order status

```json
PATCH /api/Order/1/status
{
  "status": 1
}
```

### Enum reference

**OrderStatus**
| Value | Meaning |
|-------|---------|
| `0` | Pending |
| `1` | InPreparation |
| `2` | Completed |

<br>

**PaymentMethod**
| Value | Meaning |
|-------|---------|
| `0` | Cash |
| `1` | Card |
| `2` | Online |

<br>

**Currency**
| Value | Meaning |
|-------|---------|
| `0` | EUR |
| `1` | USD |
| `2` | GBP |
| `3` | HRK |

---

## Project Structure

```
AbySalto.Junior/
│
├── Controllers/          # API layer — thin controllers, HTTP in/out only
├── Application/
│   ├── DTOs/             # Request and response data transfer objects
│   ├── Services/         # Business logic
├── Domain/
│   ├── Entities/         # EF Core entity models
│   ├── Enums/            # OrderStatus, PaymentMethod, Currency
│   └/── Interfaces/       # IApplicationDbContext, IEntity
├── Infrastructure/
│   └── Database/
│       ├── DbMaps/   # IEntityTypeConfiguration per entity (DbMaps)
│       ├── Migrations/       # EF Core migrations
│       ├── ApplicationDbContext.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```
