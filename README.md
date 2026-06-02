# 📝 Task Management App (Full Stack To-Do System)

A modern full-stack task management application built with **Vue 3 (Composition API)** on the frontend and **ASP.NET Core Web API + Entity Framework Core** on the backend.  
The project demonstrates real-world concepts such as authentication, JWT security, relational database design, and user-specific data isolation.

---

## 🚀 Features

### 🔐 Authentication & Security
- JWT-based authentication
- Secure login & registration system
- User-specific data isolation
- Protected API endpoints

### 📋 Task Management
- Create, update, delete tasks
- Mark tasks as completed / active
- Set priority levels (Low / Medium / High)
- Add optional due dates
- Overdue task detection

### 📁 Category System
- Each user can create personal categories
- Tasks can be assigned to categories
- Category-based organization per user
- One-to-many relationship (User → Categories → Tasks)

### 📊 Dashboard Features
- Task filtering (All / Active / Completed / Overdue)
- Live counters for task status
- Search functionality
- Clean sidebar navigation

### 🎨 Frontend UI
- Modern Vue 3 Composition API structure
- Reactive state management
- Toast notifications (success/error feedback)
- Responsive and minimal UI design
- Sidebar-based navigation system

---

## 🧠 Tech Stack

### Frontend
- Vue 3
- TypeScript
- Pinia (state management)
- Axios
- Vue Toastification

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- LINQ / LINQ projections (DTO usage)

### Database
- Relational DB (SQL Server / MySQL compatible)
- Code-first approach with EF Core

---

## 🔐 Key Backend Logic

- User-specific filtering using `UserId`
- Category validation per user
- DTO usage to avoid circular references
- LINQ projection for optimized API responses
- Secure JWT claim extraction from token


## 👨‍💻 Author

Built by **Ulaş Aylar**  
Focused on full-stack development and modern web architecture.

---

## 📜 License

This project is open-source for learning purposes.
