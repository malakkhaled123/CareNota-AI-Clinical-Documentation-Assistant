# CareNota Backend

ASP.NET Core Web API for authentication, authorization, patient management, appointments, visits, audio records, AI summaries, and related clinical workflows.

## Configuration

Real credentials are intentionally not included in this repository. Configure secrets through ASP.NET Core User Secrets or environment variables.

Required values include:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`
- `AzureBlob:ConnectionString`
- `EmailSettings:SenderEmail`
- `EmailSettings:AppPassword`
- `CARENOTA_ADMIN_PASSWORD`

For local development, `appsettings.Local.example.json` shows the expected structure.
