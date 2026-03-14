# FlowDesk Portfolio App

A portfolio-quality full-stack application with React + ASP.NET Core Web API that demonstrates professional architecture, JWT auth, role-based access, and clean responsive UI.

## 1) Project architecture

```text
FlowDesk/
├─ backend/
│  ├─ Authentication/
│  ├─ Controllers/
│  ├─ DTOs/
│  ├─ Data/
│  ├─ Models/
│  ├─ Repositories/
│  ├─ Services/
│  ├─ Program.cs
│  └─ appsettings.json
└─ frontend/
   ├─ src/
   │  ├─ components/
   │  ├─ context/
   │  ├─ layouts/
   │  ├─ pages/
   │  ├─ routes/
   │  └─ services/
   └─ package.json
```

## 2) Backend code (ASP.NET Core)

- ASP.NET Core Web API with Identity + EF Core + SQLite.
- Layered organization using Controllers, Services, and Repositories.
- JWT authentication with role claims.
- Seeded data for users and content.

## 3) Frontend code (React)

- React + Vite architecture with React Router.
- Axios service layer for API integration.
- Context API for auth/session state.
- Role-aware protected routes for Admin/User.

## 4) Database models

- `User` (Identity user) with `FullName`, `CreatedAtUtc`.
- `ContentItem` with `Title`, `Description`, publication state, timestamps.

## 5) Authentication implementation

- Login API validates credentials with ASP.NET Identity.
- Password hashing is handled by Identity.
- JWT contains user id, email, and role claims.
- Frontend persists token and automatically sends bearer token via Axios interceptor.

## 6) Example API endpoints

- `POST /api/auth/login` – login and receive JWT.
- `GET /api/content` – authenticated content list.
- `POST /api/content` – Admin only create.
- `PUT /api/content/{id}` – Admin only update.
- `DELETE /api/content/{id}` – Admin only delete.
- `GET /api/users/me` – authenticated profile.
- `GET /api/users` – Admin only list all users.

## 7) Instructions to run locally

### Backend

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

API runs at `https://localhost:5001` (or `http://localhost:5000` depending on launch profile).

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Open `http://localhost:5173`.

## 8) Screens that should exist in the UI

- Login page (includes demo credentials hint text)
- Dashboard page
- Admin management page
- User profile page
- Not authorized page
- Responsive navigation bar

## Seeded test login credentials

Admin:
- email: `admin@test.com`
- password: `Admin123!`

User:
- email: `user@test.com`
- password: `User123!`
