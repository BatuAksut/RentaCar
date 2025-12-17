# RentaCar 🚗

RentaCar is a **.NET Web API** based **car rental backend project** built with a clean, layered architecture (N-Tier Architecture). The project is designed to demonstrate **enterprise-level backend practices**, SOLID principles, and Aspect-Oriented Programming (AOP).

This repository represents a realistic car rental system **backend**, created mainly for learning purposes and as a **portfolio project**.

---

## 🧱 Architecture

The project follows a layered architecture:

* **Entities** → Database entities
* **DataAccess** → Entity Framework Core CRUD operations
* **Business** → Business rules, services, validations
* **Core** → Cross-cutting concerns (AOP, JWT, Results, IoC, Cache, etc.)
* **WebAPI** → RESTful API endpoints

Dependencies between layers are managed using **Dependency Injection**.

---

## ⚙️ Technologies & Patterns

* **.NET / ASP.NET Core Web API**
* **Entity Framework Core**
* **Autofac** (IoC Container)
* **FluentValidation**
* **JWT (JSON Web Token)** Authentication
* **Aspect Oriented Programming (AOP)**

  * Validation Aspect
  * Cache Aspect
  * Performance Aspect
  * Transaction Aspect
* **CORS** configuration
* **DTO Pattern**
* **Result Pattern** (Success / Error responses)

---

## 🔐 Authentication & Authorization

* User **Register / Login** operations
* JWT-based authentication
* Token-based authorization

---

## 📦 Features

* 🚘 Car CRUD operations
* 🏷️ Brand and Color management
* 👤 User & Customer management
* 📅 Rental operations
* 🖼️ Car image upload
* 📡 RESTful API standards
* 🧠 Business rules & validations

---

## 🗄️ Database

* Entity Framework Core **Code First** approach
* Migration-supported structure

---

## 🚀 Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/BatuAksut/RentaCar.git
   ```
2. Configure the **ConnectionString** in `appsettings.json`.
3. Run database migrations:

   ```bash
   Update-Database
   ```
4. Run the project:

   ```bash
   dotnet run
   ```

---

## 🎯 Purpose

This project aims to:

* Practice enterprise-level backend architecture
* Gain hands-on experience with the .NET Web API ecosystem
* Build a realistic backend portfolio project

---


