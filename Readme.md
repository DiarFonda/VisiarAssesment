VisiarAssesment

VisiarAssesment is a full-stack medical appointment booking platform built with .NET 9 (C#) for the backend and Next.js 14 (React, TypeScript) for the frontend.
It allows users to register, log in, browse doctors, create bookings, and view their appointments — all in a modern, secure, and responsive interface.

Tech Stack

Backend:

ASP.NET Core 9 (C#)

Entity Framework Core (SQLite / InMemory DB)

JWT Authentication

Swagger (API Documentation)

Frontend:

Next.js 14 (App Router)

TypeScript

TailwindCSS

ShadCN/UI Components

Other:

Docker (optional for containerization)

Project Structure
VisiarAssesment/
?
??? src/
?   ??? Assesment.Api/             # ASP.NET Core Web API
?   ??? Assesment.Application/     # Application logic (DTOs, services)
?   ??? Assesment.Domain/          # Entities and domain models
?   ??? Assesment.Infrastructure/  # Data context and repositories
?
??? frontend/                      # Next.js frontend app

Getting Started (Local Setup)
1?? Backend Setup

Open a terminal in the root folder:

cd src/Assesment.Api


Run the backend:

dotnet run


The API will start at:

http://localhost:5149


Swagger UI (API docs) will be available at:

http://localhost:5149/swagger

2?? Frontend Setup

Open another terminal from the root folder:

cd frontend


Install dependencies:

npm install


Run the Next.js development server:

npm run dev


Visit the app in your browser:

http://localhost:3000

How the App Works

Start the servers:

Backend ? dotnet run

Frontend ? npm run dev

Open the frontend:
Go to http://localhost:3000

Register / Login:

On first load, you’ll see the Login and Register options.

Create a new account to proceed.

Homepage:
After registration, you’ll be redirected to the Home page where you can:

View your current bookings

Or click “Book Now” to make a new one

Booking a Doctor:

Clicking Book Now takes you to /appointments

You’ll see a list of available doctors

Choose a doctor and click Book to open their page

Fill in details and confirm the booking

Viewing Bookings:

Go to /bookings (or click “My Bookings” on the homepage)

You’ll see all your existing appointments listed there

Environment Variables

In the frontend, make sure your .env.local contains:

NEXT_PUBLIC_API_URL=http://localhost:5149


In the backend, if you’re using SQLite or another database, ensure the connection string is configured in appsettings.json.