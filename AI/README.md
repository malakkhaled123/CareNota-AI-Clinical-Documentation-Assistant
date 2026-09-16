# CareNota AI

FastAPI service responsible for clinical audio processing and AI-assisted report generation.

## Pipeline

1. Audio preprocessing and normalization
2. Whisper speech-to-text transcription
3. Transcript cleaning and correction
4. Gemini-based structured extraction
5. Doctor SOAP summary generation
6. Patient-friendly summary generation

## Setup

Create a virtual environment, install `requirements.txt`, copy `.env.example` to `.env`, and provide:

```text
GEMINI_API_KEY=your_gemini_api_key_here
```

Never commit `.env` or API keys.

Run locally with:

```bash
uvicorn main:app --reload
```
