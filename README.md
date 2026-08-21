# CleanArchitecture

An opinionated reference template for building modular monoliths with Clean Architecture, CQRS, explicit module boundaries, and reusable technical capabilities.

The repository is intended as an architectural guide rather than a production-ready application. It demonstrates how recurring application and infrastructure concerns can be solved once, validated in the template, and reused consistently in future projects.

## Architecture

![Clean Architecture execution flow](docs/CleanArchitecture-Execution-Flow.PNG)

[PDF](docs/CleanArchitecture-Execution-Flow.pdf) · [Editable PowerPoint](docs/CleanArchitecture-Execution-Flow.pptx)

### Execution model

1. A request starts from an application entry point such as HTTP, a scheduled job, or a message subscriber.
2. The corresponding inbound adapter translates the transport-specific input into an application request.
3. Most requests enter a module directly. Requests that coordinate multiple use cases may first pass through a Process Manager, while cross-module read models are handled by the Querying component.
4. A module's `Runtime` is its execution boundary. It dispatches Commands and Queries and applies middleware such as authorization, validation, auditing, and transaction management.
5. Command and Query handlers follow separate execution paths:
   - Commands coordinate domain behavior through Domain Services and the Domain Model.
   - Queries produce read models through query-specific data access.
6. Persistence, external-system adapters, and messaging adapters implement the required infrastructure concerns.
7. `Framework.*` projects provide the approved abstractions and technical solutions used by the execution layers.

The diagram describes runtime execution flow, not compile-time dependency direction.

## Architectural principles

- Business modules own their models, use cases, persistence, infrastructure adapters, and public entry points.
- Domain code does not depend on infrastructure or application hosts.
- Ports are defined by the layer that needs the capability; outer adapters implement them.
- Commands and Queries are separate boundaries because their dependencies and execution requirements differ.
- Cross-module orchestration and cross-module read models live above individual business modules.
- Hosts compose modules and technical capabilities but contain no business logic.
- Shared code is extracted only when it represents a stable abstraction or an approved reusable solution.
- Technology choices are isolated behind focused projects so they can be evaluated, upgraded, and replaced deliberately.

## Ordering module

`Ordering` is the primary reference module and demonstrates the complete modular structure.

| Project | Responsibility |
| --- | --- |
| `CleanArchitecture.Ordering.Domain` | Entities, domain rules, repository contracts, and domain-service contracts |
| `CleanArchitecture.Ordering.Domain.Services` | Domain policies and services involving multiple domain concepts |
| `CleanArchitecture.Ordering.Commands` | Write use cases, handlers, validation, authorization, and application ports |
| `CleanArchitecture.Ordering.Queries` | Read use cases, handlers, filters, and query models |
| `CleanArchitecture.Ordering.Persistence` | EF Core context, repository implementations, mappings, and migrations |
| `CleanArchitecture.Ordering.Infrastructure` | Adapters for external systems and implementations of module-owned ports |
| `CleanArchitecture.Ordering.IntegrationEvents` | Contracts published across process or module boundaries |
| `CleanArchitecture.Ordering.Messaging.*` | CAP subscribers and MassTransit consumers that translate messages into application requests |
| `CleanArchitecture.Ordering.Endpoints` | HTTP endpoints exposing module Commands and Queries |
| `CleanArchitecture.Ordering.Runtime` | Command/Query services, execution pipelines, middleware, transactions, and module registration |

### Why Runtime is separate

`Ordering.Runtime` is not another use-case layer. Commands and Queries define the requests and handlers; Runtime provides the environment in which they execute. It connects the module's handlers, middleware, persistence, domain services, and infrastructure adapters and exposes the module registration entry point:

```csharp
services.AddOrderingModule(connectionString);
```

This keeps execution policy separate from both business use cases and the application host.

## Cross-module capabilities

Some behavior does not belong to a single business module:

- **Process Manager** coordinates multi-step workflows and invokes module Commands or Queries. Durable Task provides the orchestration runtime.
- **Querying** owns read models that span module boundaries and reads directly from query infrastructure rather than routing through a module's use cases.
- **Scheduling** uses hosted services to translate Quartz jobs into application requests.
- **Messaging** uses CAP by default, with a MassTransit implementation included as an alternative adapter.

These components may coordinate modules, but they do not own module business rules.

## Framework projects

`Framework.*` projects form the template's internal technical platform. They are not a single general-purpose utility library. Each project represents a focused abstraction or an approved solution to a recurring engineering problem.

Examples include:

- `Framework.Queries` provides query-side abstractions such as `PaginatedItems<T>` without depending on a database technology.
- `Framework.Persistence` provides the accepted EF Core and SQL Server persistence conventions.
- `Framework.Scheduling` standardizes scheduling through Quartz and exposes the template's job contract.
- `Framework.Cap` and `Framework.MassTransit` provide alternative messaging integrations.
- `Framework.DurableTask` provides the selected orchestration infrastructure.
- `Framework.WebApi` contains reusable ASP.NET Core endpoint and result conventions.

The size of a Framework project is not the deciding factor for keeping it separate. A project boundary is justified when it protects dependency direction, isolates a technology, or represents a capability that can evolve independently.

## Repository structure

```text
src/
├── Administration/   Migration, debugging, and maintenance applications
├── Configuration/    Environment, options, secrets, and application composition
├── Framework/        Reusable abstractions and approved technical capabilities
├── Infrastructure/   Integrations shared outside a single business module
├── Integration/      Cross-module querying, scheduling, and process managers
├── Modules/          Business modules and their internal layers
├── Shared/           Stable application-wide concepts
└── WebApi/           ASP.NET Core host and shared web configuration

test/
└── CleanArchitecture.IntegrationTests/
```

## Included technologies

- .NET 10 and ASP.NET Core
- Entity Framework Core with SQL Server
- Minimal APIs, Controllers, OpenAPI, Swagger UI, Scalar, and OData
- FluentValidation
- CAP and MassTransit messaging adapters
- Quartz scheduling
- Durable Task orchestration
- Seq logging
- MSTest, Bogus, and Moq

These integrations are examples of accepted solutions in the template. Applications can enable, replace, or omit them according to their requirements.

## Getting started

### Prerequisites

- .NET 10 SDK
- SQL Server

### Configuration

Development configuration is stored under:

```text
src/Configuration/CleanArchitecture.Options/Options/
src/Configuration/CleanArchitecture.Secrets/Secrets/
```

Update the development connection string and other local settings before running the applications.

### Restore and build

```powershell
dotnet restore CleanArchitecture.slnx
dotnet build CleanArchitecture.slnx
```

### Apply migrations and seed sample data

```powershell
dotnet run --project src/Administration/CleanArchitecture.Administration.DbMigrationApp
```

The migration application updates the configured module, audit, messaging, and orchestration databases and seeds the sample administrator account.

### Run the Web API

```powershell
dotnet run --project src/WebApi/CleanArchitecture.WebApi --launch-profile https
```

In the Development environment, Swagger UI is available at the application root. The default HTTPS profile uses `https://localhost:7266`.

### Run the sample tests

```powershell
dotnet test test/CleanArchitecture.IntegrationTests
```

The included tests demonstrate the integration-test setup and are not intended as comprehensive coverage.

## Adding a module

When introducing a new business module:

1. Define its Domain Model and module-owned contracts.
2. Add Command and Query use cases with their required ports.
3. Implement persistence and external-system adapters outside the use-case layers.
4. Add a Runtime project that composes the module and owns its execution pipelines.
5. Expose transport-specific entry points through an Endpoints or messaging adapter project.
6. Register the module in application composition and add its endpoint module to the relevant host.
7. Keep orchestration or read models spanning multiple modules in the Integration area.

Use the `Ordering` module as the detailed reference implementation.

## Scope

This repository intentionally favors explicit examples over minimal project count. Individual applications should select only the capabilities they need and must complete their own security, operational, deployment, and testing requirements.
