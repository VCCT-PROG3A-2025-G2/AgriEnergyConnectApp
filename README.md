# AgriEnergy Connect

AgriEnergy Connect is a web-based platform designed to bridge the gap between farmers and buyers by enabling farmers to showcase their agricultural products, manage their offerings, and connect with potential buyers. Employees (admins) manage user access and system operations.

Bridging the gap between sustainable agriculture and renewable energy solutions
![logo](https://github.com/user-attachments/assets/b0a197b0-6b9c-4227-a23d-ef86a767201b)
------
## Project Overview

AgriEnergy Connect supports the South African Department of Agriculture’s digital transformation goals. It empowers local farmers to manage their products online and connect with buyers easily. The system allows for user role-based access with two roles:
- **Farmer**: Upload and manage products.
- **Employee (Admin)**: Oversee farmer registrations, view all products, and manage users.

-----

## System Requirements

To run this project, install the following software/tools:

- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [Visual Studio 2022 or later](https://visualstudio.microsoft.com/)
- [SQL Server or SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Entity Framework Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- Git (optional for cloning)

---

## Setting Up the Development Environment

### 1. Clone the Repository
git clone https://github.com/your-username/AgriEnergyConnect.git
cd AgriEnergyConnect

### 2. Restore .Net Dependencies
dotnet restore

### 3. Configure the Database
- Open appsettings.json and modify the connection string
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AgriEnergyDB;Trusted_Connection=True;"
}

### 3. Configure the Database
