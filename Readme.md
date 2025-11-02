# VisiarAssesment

**VisiarAssesment** is a full-stack medical appointment platform built with **.NET 9 (C#)** on the backend and **Next.js 14 (React + TypeScript)** on the frontend.  
It lets users **register**, **log in**, **book doctor appointments**, and **view their bookings** — all in one clean, responsive interface.  
An **AI-powered recommendation system** helps suggest the right doctor based on user symptoms.

---

## Tech Stack

**Backend**
- ASP.NET Core 9 (C#)
- Entity Framework Core (SQLite)
- JWT Authentication
- Swagger for API documentation

**Frontend**
- Next.js 14 (App Router)
- TypeScript
- TailwindCSS
- ShadCN/UI

---

## Project Structure

VisiarAssesment/
│
├── src/
│ ├── Assesment.Api/ # ASP.NET Core Web API
│ ├── Assesment.Application/ # Application logic (DTOs)
│ ├── Assesment.Domain/ # Entities and domain models
│ └── Assesment.Infrastructure/ # Data context, repositories, identity, services
│ └── Assesment.tests/ # tests, test for appointmentsController only for now
│
└── frontend/ # Next.js frontend app

---

##  Run with Docker

The easiest way to start the full app is using Docker.

### Clone the repository
```bash
git clone https://github.com/yourusername/VisiarAssesment.git
cd VisiarAssesment

Make sure Docker Desktop is running, then execute:

docker-compose up --build

---

Access the App

Frontend: http://localhost:3000

API (Swagger): http://localhost:5149/swagger/index.html

---

AI Feature

The platform includes an AI Doctor Recommendation system — it analyzes the symptoms a user enters and recommends the best matching doctor, even when the specialty isn’t mentioned directly.

Author: Diar Fonda