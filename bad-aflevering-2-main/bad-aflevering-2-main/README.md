# Aarhus Space Program API

## Run
1. Start SQL Server Docker container
2. Start MongoDB Docker container
3. Run:
   dotnet ef database update --project .\AarhusSpaceProgram.Api --startup-project .\AarhusSpaceProgram.Api
4. Run:
   dotnet run --project .\AarhusSpaceProgram.Api

## URLs
- API base URL: http://localhost:5092
- OpenAPI JSON: http://localhost:5092/openapi/v1.json
- Scalar UI: http://localhost:5092/scalar

## Group Contribution

| Name | Studentnumber | Contribution |
|:------|:---------------|:------------------|
| Danjal Ljosheim | 202207910 | DbContext, Seed data, DTO's, CRUD(Services, Controllers), Logging |
| Lasse Breuer | 202405825 | ER diagram, Models, Controller, Migration |
| William Andreas Vinther | 202406891 | ER diagram, DTO's, Queries(Services, Controllers) |