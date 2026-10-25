# 🏥 Hospital Appointment System — RESTful Web API

A scalable, clean-architecture **Hospital Appointment Management System** built with **ASP.NET Core Web API**, **Entity Framework Core (Database-First)**, and **SQL Server**, strictly following the **3-Tier Architecture Pattern** (Presentation / Business Logic / Data Access Layer).

---

## 🏛️ Architecture & Design Patterns

The solution is divided into 3 decoupled layers to ensure separation of concerns and maintainability:

```
HospitalAppointmentSystem/
├── 🌐 AppLayerAPI (Presentation Layer)
│   ├── Controllers (DoctorController, PatientController, AppointmentController)
│   └── Dependency Injection & Middleware configuration (Program.cs)
│
├── ⚙️ BLL (Business Logic Layer)
│   ├── Services (DoctorService, PatientService, AppointmentService)
│   ├── DTO / ViewModels (DoctorModel, DoctorInfoModel, PatientModel, AppointmentModel)
│   └── AutoMapper Profile (MappingProfile.cs)
│
└── 🗄️ DAL (Data Access Layer)
    ├── EF Core DbContext (HospitalContext.cs)
    ├── Entities (Doctor, Patient, Appointment)
    └── Repositories (DoctorRepo, PatientRepo, AppointmentRepo)
```

### Key Design Patterns Implemented:
* **3-Tier Architecture:** Complete decoupling between API controllers, business logic, and database operations.
* **Repository Pattern:** Encapsulates data access and queries (CRUD, complex LINQ filters, eager loading with `.Include()`).
* **Dependency Injection (DI):** All repositories and services are registered in the DI container with scoped lifetimes.
* **DTO & AutoMapper:** Strict mapping between database entities and API models using AutoMapper with custom projection (`ForMember`).
* **LINQ Queries:** Expressive data retrieval for filtering appointments by date, doctor, patient, and status.

---

## 🛠️ Tech Stack

* **Framework:** .NET 10 (C#) / ASP.NET Core Web API
* **ORM:** Entity Framework Core 10 (SQL Server Provider)
* **Database:** Microsoft SQL Server
* **Mapping:** AutoMapper 14
* **Documentation / Spec:** Microsoft.AspNetCore.OpenApi

---

## 🗃️ Database Schema

```
Doctors (Id [PK], Name, Speciality, Phone, Email)
   │
   └──< Appointments (Id [PK], DoctorId [FK], PatientId [FK], AppointmentDate, Status, Notes) >── Patients (Id [PK], Name, Age, Gender, Phone, Email)
```

---

## 🚀 API Endpoints

### 🩺 Doctor Endpoints (`/api/doctor`)
| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/doctor/all` | Get all registered doctors |
| `GET` | `/api/doctor/{id}` | Get doctor by primary key |
| `GET` | `/api/doctor/all/appointments` | Get doctors along with their scheduled appointments |
| `GET` | `/api/doctor/all/info` | Get doctor overview with computed appointment count |
| `POST` | `/api/doctor/create` | Register a new doctor |
| `PUT` | `/api/doctor/update/{id}` | Update doctor details |
| `DELETE` | `/api/doctor/delete/{id}` | Remove doctor |

### 🧑‍🤝‍🧑 Patient Endpoints (`/api/patient`)
| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/patient/all` | Get all registered patients |
| `GET` | `/api/patient/{id}` | Get patient by primary key |
| `POST` | `/api/patient/create` | Register a new patient |
| `PUT` | `/api/patient/update/{id}` | Update patient details |
| `DELETE` | `/api/patient/delete/{id}` | Remove patient |

### 📅 Appointment Endpoints (`/api/appointment`)
| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/appointment/all` | Get all appointments with Doctor & Patient info |
| `GET` | `/api/appointment/{id}` | Get appointment details by ID |
| `GET` | `/api/appointment/pending` | Filter appointments with `Pending` status |
| `GET` | `/api/appointment/today` | Filter appointments scheduled for today |
| `GET` | `/api/appointment/doctor/{doctorId}` | Get appointments for a specific doctor |
| `GET` | `/api/appointment/patient/{patientId}` | Get appointments for a specific patient |
| `POST` | `/api/appointment/create` | Book a new appointment |
| `PUT` | `/api/appointment/update/{id}` | Update appointment date/notes |
| `PUT` | `/api/appointment/status/{id}` | Update appointment status (Pending/Confirmed/Completed/Cancelled) |
| `DELETE` | `/api/appointment/delete/{id}` | Cancel/delete an appointment |

---

## 💻 Getting Started

### 1. Database Setup
Run the following script in SQL Server Management Studio (SSMS):
```sql
CREATE DATABASE HospitalDB;
GO
USE HospitalDB;

CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    Speciality VARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

CREATE TABLE Appointments (
    Id INT PRIMARY KEY IDENTITY,
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    Notes VARCHAR(300) NULL,
    CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
    CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientId) REFERENCES Patients(Id)
);
```

### 2. Configure Connection String
In `AppLayerAPI/appsettings.json`, set your SQL Server instance name:
```json
"ConnectionStrings": {
  "DbConn": "server=YOUR_SERVER_NAME; initial catalog=HospitalDB; TrustServerCertificate=True; Integrated Security=True;"
}
```

### 3. Run the Project
Open `HospitalAppointmentSystem.slnx` in Visual Studio 2022, set `AppLayerAPI` as the Startup Project, and press **F5** (or run `dotnet run --project AppLayerAPI`).
