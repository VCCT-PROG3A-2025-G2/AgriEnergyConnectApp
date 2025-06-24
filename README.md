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

- .NET 8 SDK or later
- Visual Studio 2022 or later](https://visualstudio.microsoft.com/)
- SQL Server Management or SQLLite DB Browser
- Entity Framework Core
- Git (for cloning)

-----

## Setting Up the Development Environment

### 1. Clone the Repository
git clone https: https://github.com/VCCT-PROG3A-2025-G2/AgriEnergyConnectApp.git
cd AgriEnergyConnect

### 2. Restore .Net Dependencies
dotnet restore

### 3. Configure the Database
- Open appsettings.json and modify the connection string
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AgriEnergyDB;Trusted_Connection=True;"
}

### 4. Run Migrations
- Install EF Core
 dotnet tool install --global dotnet-ef
- Apply migrations yo create data schema
  dotnet ef database update

## Setting Up the Development Environment
- Build thr App:
  dotnet build
- Run the App:
  dotnet run

  -----
  
## System Functionalities
Main Features

Farmer Dashboard:
- Farmers can view and manage their products, including adding new products, updating existing products, and deleting products.
- They can filter products by category and view product details.
- Admin Dashboard:
- Admins (employees) can view all farmer profiles and manage them.
- Admins can also filter and search products across all farmers.

Product Management:
- Farmers can add products, including details such as name, category, price, and date.
- Admins can view, approve/reject, and manage products from all farmers.
- User Registration and Login:
- Farmers can create accounts and log in to their dashboards.
- Admins can manage farmer accounts and roles.
-----
## User Role and Permissions
The system supports two main user roles: Farmer and Employee (Admin). Each role has different permissions and access levels.

Farmer
- Dashboard: Farmers have access to a personalized dashboard where they can manage their - products, view product details, and perform actions related to their products.
- Product Management: Farmers can add new products, update existing ones, and delete products.
- Profile Management: Farmers can view and update their profile information.
Employee
- Dashboard: Admins have access to a broader dashboard to manage multiple farmers, view their products, and manage farmer profiles.
- Farmer Management: Admins can create, view, edit, or delete farmer profiles.
- Product Management: Admins can view, approve, or reject products listed by farmers.
- Role Management: Admins have the ability to assign and manage roles for different users,   ensuring that the right permissions are granted to each user.
----- 
## Screenshots 
![Product Display Elegant Facebook Cover](https://github.com/user-attachments/assets/377d5bf6-ceeb-4356-be6b-b004b50beeb1)

----- 
## References
https://www.youtube.com/watch?v=NDlJ-ROmNTo
https://www.youtube.com/watch?v=BWOcba-XfcM
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-9.0
https://www.c-sharpcorner.com/blogs/manage-session-in-mvc
https://stackoverflow.com/questions/19181085/session-manage
