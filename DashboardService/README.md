# DashboardService Solution

## Overview
This solution demonstrates a microservices-based architecture for a license management system with:
- ASP.NET MVC frontend (role-based dashboards)
- Microservices: License, Document, Notification
- SQL Server with multi-tenancy
- JWT authentication/authorization
- CQRS in LicenseService
- Background job processing (Hangfire)
- RESTful APIs
- AWS deployment (Docker file)
- API Gateway integration (Ocelot)

## Structure
```
DashboardService/
 db/
 DashboardDb/ # SQL Server database scripts
 src/
 ApiGateway/ # API Gateway using Ocelot
 Dashboard.Web/ # ASP.NET MVC frontend
 LicenseService/ # License microservice (CQRS, background jobs)
 DocumentService/ # Document microservice
 NotificationService/ # Notification microservice
 DAL/ # Data Access Layer for shared entities
```

## Setup Instructions
1. Prerequisites: .NET6+, SQL Server, Docker (for AWS deployment)
2. Build all projects: `dotnet build DashboardService.sln`
3. Run services: `dotnet run --project src/LicenseService/LicenseService.csproj` (repeat for other services)
4. Run frontend: `dotnet run --project src/Dashboard.Web/Dashboard.Web.csproj`
5. Configure JWT settings in each service
6. For AWS: Build Docker images and deploy to ECS/EKS or Lambda

## Design Rationale
- **Microservices**: Scalability, independent deployment
- **CQRS**: Separation of read/write in LicenseService for performance
- **Multi-tenancy**: TenantId in all tables, middleware for isolation
- **JWT**: Secure, stateless authentication
- **Background Jobs**: License renewals handled asynchronously
- **API Gateway**: Centralized routing and security
