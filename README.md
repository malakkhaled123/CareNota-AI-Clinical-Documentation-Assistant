<div align="center">
  <img width="475" height="475" alt="Blue Tosca Simple Medical Center Logo (1)" src="https://github.com/user-attachments/assets/586d44b0-e221-403c-8ab2-ddf0b04b922c" />
</div>

# CareNota

### AI-Assisted Clinical Documentation & Clinic Management System

CareNota is an AI-assisted clinical documentation and clinic management system designed to reduce the administrative workload of healthcare professionals. It combines consultation recording, speech-to-text, AI-powered clinical documentation, patient management, appointments, and visit records in one platform.

## 🎥 Demo

**[▶ Watch the CareNota Demo - Youtube](https://www.youtube.com/watch?v=kApm0344Oc8)**

The demo walks through the main workflow from consultation recording and AI processing to structured clinical documentation and patient records.

## Key Features

### 🤖 AI Clinical Documentation

* 🎙️ Records and processes clinical consultations
* 📝 Converts medical speech to text using **Whisper**
* 🧠 Reconstructs and corrects transcripts using **Google Gemini**
* 📋 Generates structured **SOAP-style doctor documentation**
* 👤 Generates **patient-friendly summaries**
* 🌍 Supports **Arabic, including Egyptian Arabic, and English**

### 🏥 Clinic Management

* 🗂️ Patient and visit management
* 📅 Appointment scheduling
* 💊 Medication and prescription records
* 🧪 Diagnosis and laboratory test records
* 🔐 Authentication, authorization, JWT, and role-based access
* 📧 Email-based system notifications
* ☁️ Temporary clinical audio storage using **Azure Blob Storage**

---

## System Architecture

CareNota uses a modular architecture with separate frontend, backend, and AI services.

```text
                         ┌─────────────────────┐
                         │   Angular Frontend  │
                         └──────────┬──────────┘
                                    │ HTTP / REST
                                    ▼
                         ┌─────────────────────┐
                         │  ASP.NET Core API   │
                         │ Authentication/RBAC │
                         └───────┬───────┬─────┘
                                 │       │
                      ┌──────────┘       └──────────┐
                      ▼                             ▼
             ┌─────────────────┐          ┌──────────────────┐
             │    SQL Server   │          │ Azure Blob       │
             │ Patient & Visit │          │ Temporary Audio  │
             │ Data            │          │ Storage          │
             └─────────────────┘          └────────┬─────────┘
                                                   │
                                                   ▼
                                        ┌────────────────────┐
                                        │   CareNota AI      │
                                        │      FastAPI       │
                                        └─────────┬──────────┘
                                                  │
                                      ┌───────────┴───────────┐
                                      ▼                       ▼
                             ┌────────────────┐      ┌───────────────┐
                             │ Whisper        │      │ Gemini        │
                             │ Speech-to-Text │      │ AI Processing │
                             └────────────────┘      └───────────────┘
```

The AI service is separated from the main API to isolate speech-processing and language-model workloads from the core clinic management system.

---

## AI Processing Pipeline

```text
Clinical Audio
      ↓
Audio Preprocessing
      ↓
Whisper Transcription
      ↓
Transcript Reconstruction & Correction
      ↓
Gemini Structured Extraction
      ↓
Doctor SOAP Documentation
+
Patient-Friendly Summary
```

### Two-Stage AI Processing

1. **Whisper transcription** converts the consultation audio into text.
2. **Gemini reconstruction** corrects transcription errors and reconstructs the intended medical text while preserving the physician's content.
3. **Gemini structured extraction** converts the reconstructed transcript into structured JSON.
4. The resulting information is used to generate doctor-facing SOAP documentation and a patient-friendly summary.

The AI output is designed to document information stated during the consultation, including the physician's stated medications, tests, and follow-up instructions, rather than generate independent clinical recommendations.

---

## Technology Stack

| Layer            | Technologies                            |
| ---------------- | --------------------------------------- |
| Frontend         | Angular, TypeScript, Tailwind CSS       |
| Backend          | ASP.NET Core, C#, Entity Framework Core |
| Database         | Microsoft SQL Server                    |
| AI Service       | Python, FastAPI                         |
| Speech-to-Text   | Whisper                                 |
| Generative AI    | Google Gemini API                       |
| Audio Processing | pydub, PyTorch                          |
| Cloud Storage    | Azure Blob Storage                      |
| Authentication   | ASP.NET Core Identity, JWT              |

---

## Repository Structure

```text
CareNota/
├── AI/             # FastAPI + Whisper + Gemini processing service
├── Backend/        # ASP.NET Core Web API and database integration
├── Frontend/       # Angular application
├── Documentation/  # Architecture and project documentation
├── .gitignore
└── README.md
```

Each major component contains its own README with more detailed implementation and setup information.

---

## Local Setup

### 1. Clone the repository

```bash
git clone https://github.com/malakkhaled123/CareNota-AI-Clinical-Documentation-Assistant.git
cd CareNota-AI-Clinical-Documentation-Assistant
```

### 2. Backend

Open `Backend/CareNota.slnx` in Visual Studio or run the project using the .NET CLI.

Configure the required local settings using secure configuration such as **ASP.NET Core User Secrets** or environment variables.

Required configuration includes:

* SQL Server connection string
* JWT signing key
* Azure Blob Storage credentials
* Email configuration
* Local administrator credentials

See `Backend/README.md` for backend-specific setup details.

### 3. AI Service

```bash
cd AI
python -m venv .venv
```

**Windows:**

```bash
.venv\Scripts\activate
```

**macOS/Linux:**

```bash
source .venv/bin/activate
```

Install dependencies:

```bash
pip install -r requirements.txt
```

Copy `.env.example` to `.env` and provide your Gemini API key:

```text
GEMINI_API_KEY=your_gemini_api_key_here
```

Run the service:

```bash
uvicorn main:app --reload
```

See `AI/README.md` for more details about the AI pipeline.

### 4. Frontend

```bash
cd Frontend
npm install
ng serve
```

Then open the local Angular development URL shown by the CLI.

---

## Security & Privacy

This repository intentionally excludes production secrets, local credentials, generated build files, IDE state, and deployment-specific publish profiles.

Clinical and patient data should **never** be committed to source control.

API keys, database credentials, storage keys, email passwords, and JWT signing keys must be supplied through secure configuration mechanisms.

---

## Project Team

**Team Leader & AI Developer:** Malak Khaled

**Team Members:**

* Nadeen Ahmed
* Malak Badawy
* Eman Ahmed
* Somia Tarek
* Amir Mohamed
* Safie El Din Waleed

**Field:** Healthcare Informatics & Data Analytics

---

## Academic Project

**Graduation Project II — Spring 2026**

Faculty of Computers and Data Sciences, Alexandria University

**Field:** Healthcare Informatics & Data Analytics

---

## License

This project was developed as a graduation project. Unless a license is added by the project owners, all rights are reserved.
