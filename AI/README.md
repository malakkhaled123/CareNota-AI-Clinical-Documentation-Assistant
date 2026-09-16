# CareNota AI

FastAPI service responsible for processing clinical audio recordings and generating structured clinical documentation.

## Overview

The AI service converts a doctor's recorded consultation into structured documentation through a multi-stage processing pipeline.

It supports **Arabic, including Egyptian Arabic, and English**.

## Pipeline

```text
Clinical Audio
      ↓
Audio Preprocessing
      ↓
Whisper Transcription
      ↓
Transcript Reconstruction & Correction
      ↓
Structured JSON Extraction
      ↓
Doctor SOAP Documentation
      ↓
Patient-Friendly Summary
```

### 1. Audio Preprocessing

Audio is prepared for speech recognition by:

* Converting audio to **16 kHz, mono**
* Normalizing audio levels
* Preparing the audio in WAV format

Implemented in `ai/preprocessing.py`.

### 2. Speech-to-Text

The processed audio is transcribed using the **Whisper Base model**.

The raw transcript is used as an intermediate processing result rather than being stored as the final clinical documentation.

Implemented in `ai/whisper_model.py`.

### 3. Transcript Reconstruction

A first Gemini processing stage cleans and reconstructs the Whisper transcript.

For Arabic consultations, this includes correcting common speech-recognition errors, separating incorrectly connected words, and improving medical terminology while preserving the physician's intended content.

Implemented in `ai/gemini.py`.

### 4. Structured Extraction

A second Gemini processing stage converts the reconstructed transcript into structured JSON.

The output includes:

* **Doctor documentation** in SOAP format
* **Patient-friendly summary**
* Information explicitly stated during the consultation, such as medications, tests, and follow-up instructions

The system is designed to document the physician's stated plan rather than generate independent medical recommendations.

### 5. JSON Processing

The AI response is parsed and validated before being returned by the API.

## Tech Stack

| Component            | Technology    |
| -------------------- | ------------- |
| Language             | Python        |
| API Framework        | FastAPI       |
| Speech Recognition   | Whisper       |
| Large Language Model | Google Gemini |
| Audio Processing     | pydub         |
| Deep Learning        | PyTorch       |
| API Communication    | Requests      |

## Project Structure

```text
AI/
├── main.py
├── requirements.txt
├── Dockerfile
├── .env.example
└── ai/
    ├── __init__.py
    ├── pipeline.py
    ├── preprocessing.py
    ├── whisper_model.py
    └── gemini.py
```

## Setup

### 1. Create a virtual environment

```bash
python -m venv venv
```

Activate it on Windows:

```bash
venv\Scripts\activate
```

On macOS/Linux:

```bash
source venv/bin/activate
```

### 2. Install dependencies

```bash
pip install -r requirements.txt
```

### 3. Configure the Gemini API key

Create a `.env` file based on `.env.example`:

```text
GEMINI_API_KEY=your_gemini_api_key_here
```

**Never commit `.env` or API keys to the repository.**

### 4. Run the API

From the `AI` directory:

```bash
uvicorn main:app --reload
```

The API will be available locally at:

```text
http://localhost:8000
```

## API Endpoints

### `GET /`

Health check endpoint.

### `POST /process-audio`

Processes a clinical audio recording and returns structured clinical documentation.

The endpoint accepts the audio URL together with relevant patient context used during documentation, such as:

* Age
* Gender
* Chronic conditions
* Allergies
* Previous summary

The resulting JSON contains the generated doctor documentation and patient-friendly summary.

## Environment Variables

The service requires:

```text
GEMINI_API_KEY=your_gemini_api_key_here
```

See `.env.example` for the expected configuration.

## Notes

* The service supports Arabic, Egyptian Arabic, and English.
* Audio is processed temporarily during the pipeline.
* Raw transcripts are not used as the final stored documentation.
* The AI output is intended to reflect information stated during the consultation, not to provide independent clinical decision support.
* The AI service is one component of the larger **CareNota** clinic system.
