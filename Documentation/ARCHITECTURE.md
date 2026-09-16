# CareNota Architecture

## Main Components

### Frontend
Angular provides the user-facing clinical workspace for receptionists, doctors, patients, and administrators.

### Backend
The ASP.NET Core API handles authentication, authorization, business logic, patient and visit management, appointments, audio records, summaries, and database access.

### AI Service
The FastAPI service handles audio preprocessing, Whisper transcription, transcript correction, and Gemini-based structured clinical report generation.

### Data Layer
SQL Server stores application data such as users, patients, visits, appointments, diagnoses, medications, laboratory tests, audio records, and AI summaries.

### Audio Storage
Azure Blob Storage is used for audio files. The application is designed around temporary access through time-limited SAS URLs rather than storing long-lived SAS tokens in the database.

## Clinical Workflow

1. Doctor starts a consultation.
2. Consultation audio is recorded.
3. Audio is uploaded to temporary cloud storage.
4. The backend provides the AI service with access to the recording.
5. Whisper converts speech to text.
6. Gemini cleans recognition errors and structures the consultation.
7. CareNota generates a doctor-facing SOAP summary and a patient-friendly summary.
8. The doctor reviews and edits the generated information.
9. The final information is stored in the patient record.

## Privacy-by-Design Considerations

- Keep credentials outside source control.
- Send only the minimum required information to external AI services.
- Use time-limited access for stored audio.
- Remove temporary audio according to the application's cleanup policy.
- Apply authentication and role-based authorization to clinical resources.
