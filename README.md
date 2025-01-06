# Mini Inventory Management System
This repository contains a Mini Inventory Management System, built using C#, ASP.NET Core, and an in-memory database (or SQLite). The system allows users to manage products, place orders, and track inventory efficiently.

Objective
To create a simple inventory management system with CRUD operations, input validation, and basic reporting features.

**Features**
 Product Management
1. Create, Read, Update, and Delete (CRUD) operations for products.
2.  Each product has the following fields:
  a) Id: Auto-incremented identifier.
  b) Name: String, required.
  c) Price: Decimal, must be greater than 0.
  d) Stock: Integer, must be greater than or equal to 0.

**Order Management**
1. Place an order for a product.
2. Automatically reduce the product's stock after an order.
3. Prevent orders if the stock is insufficient.
   
**Validation Rules**
1. Inputs are validated to ensure:
  a) Product name is required.
  b) Price is greater than 0.
  c) Stock level is greater than or equal to 0.
2. Handles errors gracefully with appropriate messages.

