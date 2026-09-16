<div align="center">
  <img width="475" height="475" alt="Blue Tosca Simple Medical Center Logo (1)" src="https://github.com/user-attachments/assets/586d44b0-e221-403c-8ab2-ddf0b04b922c" />
</div>

# CareNota 

### Your Care, Now in Notes

CareNota is an AI-assisted clinical documentation platform designed to reduce the burden of medical paperwork. It helps healthcare professionals record consultations, convert speech into text, generate structured clinical summaries, and keep patient information organized in one workspace.

## What CareNota Does

- 🎙️ Records and processes clinical conversations
- 📝 Converts medical speech to text using Whisper
- 🧠 Cleans and structures transcripts with Google Gemini
- 📋 Generates SOAP-style doctor summaries
- 👤 Generates patient-friendly summaries
- 🗂️ Manages patients, visits, appointments, diagnoses, medications, and lab tests
- 🔐 Uses authentication, authorization, JWTs, and role-based access
- ☁️ Stores audio temporarily using Azure Blob Storage with time-limited access
- 📧 Supports email-based system notifications

## System Architecture

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
                         │ Speech-to-Text │      │ Structuring   │
                         └────────────────┘      └───────────────┘
```

## AI Processing Pipeline

```text
Audio Recording
      ↓
Audio Preprocessing
      ↓
Whisper Transcription
      ↓
Transcript Cleaning & Medical Term Correction
      ↓
Gemini Structured Extraction
      ↓
Doctor SOAP Summary + Patient-Friendly Summary
```

The AI service is implemented separately from the main API so speech processing and language-model workloads can be maintained independently.

## Repository Structure

```text
CareNota/
├── Backend/       # ASP.NET Core Web API + SQL Server integration
├── AI/            # FastAPI + Whisper + Gemini processing service
├── Frontend/      # Angular application
├── Documentation/ # Architecture and project documentation
├── Screenshots/   # Optional demo screenshots
├── .gitignore
└── README.md
```

## Technology Stack

| Layer | Technologies |
|---|---|
| Frontend | Angular, TypeScript, Tailwind CSS |
| Backend | ASP.NET Core, C#, Entity Framework Core |
| Database | Microsoft SQL Server |
| AI Service | Python, FastAPI |
| Speech-to-Text | OpenAI Whisper |
| Generative AI | Google Gemini API |
| Cloud Storage | Azure Blob Storage |
| Authentication | ASP.NET Core Identity, JWT |

## Local Setup

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd CareNota
```

### 2. Backend

Open `Backend/CareNota.slnx` in Visual Studio or run the project with the .NET CLI.

Configure the required settings locally. **Do not commit real credentials.** You can use ASP.NET Core User Secrets or environment variables.

Required configuration includes:

- SQL Server connection string
- JWT signing key
- Azure Blob Storage connection string
- Email sender credentials
- `CARENOTA_ADMIN_PASSWORD`

Example environment variable naming for nested ASP.NET configuration:

```text
ConnectionStrings__DefaultConnection
Jwt__Key
AzureBlob__ConnectionString
EmailSettings__SenderEmail
EmailSettings__AppPassword
CARENOTA_ADMIN_PASSWORD
```

### 3. AI Service

```bash
cd AI
python -m venv .venv

# Windows
.venv\Scripts\activate

# macOS/Linux
# source .venv/bin/activate

pip install -r requirements.txt
```

Copy `.env.example` to `.env` and add your Gemini API key:

```text
GEMINI_API_KEY=your_gemini_api_key_here
```

Run the service:

```bash
uvicorn main:app --reload
```

### 4. Frontend

```bash
cd Frontend
npm install
ng serve
```

Then open the local Angular development URL shown by the CLI.

## Security & Privacy Notes

This repository intentionally excludes production secrets, local credentials, generated build files, IDE state, and deployment-specific publish profiles.

Clinical and patient data should never be committed to source control. API keys, database passwords, storage keys, email passwords, and JWT signing keys must be supplied through secure configuration mechanisms.

## Project Team

- Malak Khaled
- Nadeen Ahmed
- Malak Badawy
- Eman Ahmed
- Somia Tarek
- Amir Mohamed
- Safie El Din Waleed

**Field:** Healthcare Informatics & Data Analytics

## Academic Project

Graduation Project II — Spring 2026 - Faculty of Computers and Data Sciences Alexandria University 

## License

This project was developed as a graduation project. Unless a license is added by the project owners, all rights are reserved.
