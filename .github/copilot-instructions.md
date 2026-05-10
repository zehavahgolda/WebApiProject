# Copilot onboarding — repository inventory & instructions

Summary
- Purpose: RESTful Web API for an e-commerce/store backend (products, categories, orders, users, ratings).
- Surface: ASP.NET Core Web API (WebApiShop) exposing controllers for Products, Users, Orders, Password utilities, etc.
- Current status notes: solution targets .NET 9 and C# 13. There are scaffolded EF Core model files with some duplicate/typo issues and a hard-coded connection string in the generated DbContext.

Tech stack
- .NET: net9.0, C# 13
- Web: ASP.NET Core minimal/hosted API (Program.cs + Controllers)
- Data: Entity Framework Core 9 (DbContext + scaffolded entity classes)
- Mapping: AutoMapper
- Security: JWT support appears in some branches but the active Program.cs shown does not use it
- Tests: xUnit, Moq, Moq.EntityFrameworkCore, coverlet
- Utilities: zxcvbn-core for password strength
- Dev environment: Visual Studio 2022 or dotnet CLI

Project structure (high-level)
- WebApiShop/ — API entrypoint, Program.cs, Controllers/, static web root (wwwroot) handling file uploads
- Repository/ — Store_329391924Context (DbContext), repository implementations (ProductRepository, OrderrRepository, UserRepository, etc.), repository interfaces
- Services/ — business logic layer and service interfaces
- Entity/ — EF model classes (Product, Category, Order, OrdeItem, User)
- DTOs/ — data transfer objects used by controllers/services
- Tests/ — unit tests (xUnit + Moq)
- .github/ — CI and instructions folder (create if missing)

Important findings & risks
- Hard-coded connection string in Repository\Store329391924contextContext.cs — move to configuration before committing or publishing.
- Several auto-generated entity files show duplicates, typos (Catogery, OrdeItem) and inconsistent nullability; tidy these to avoid runtime/compile-time issues.
- Multiple Program.cs variants exist in the tree — confirm the authoritative one.
- Some code references JWT configuration; remove unused authentication bits or standardize how authentication is applied across the project.
- Avoid committing secrets. Use appsettings.* and environment variables or __Secret Manager__.

Coding & repository guidelines (concise, broadly applicable)
- Nullability: keep <Nullable>enable</Nullable> and respect nullable annotations.
- Async: prefer EF Core async APIs (ToListAsync, FirstOrDefaultAsync, CountAsync) for I/O operations.
- DI lifetime: register repositories/services as scoped for DbContext lifetime (current pattern uses scoped).
- Separation of concerns: controllers should be thin — forward work to Services which use Repositories.
- Mapping: centralize AutoMapper profiles in Services or a dedicated folder; avoid mapping logic in controllers.
- Logging & errors: use ILogger<T> in controllers/services and a centralized error handling middleware for consistent responses.
- Configuration & secrets: keep connection strings, JWT keys and other secrets out of source. Use appsettings.Development.json and environment variables; prefer __Secret Manager__ or CI secrets.
- Tests: write unit tests targeting service and repository behavior; mock DbContext / DbSet with Moq or use in-memory providers for integration-style tests.
- Naming: prefer consistent, descriptive names (Category vs Catogery, OrderItem vs OrdeItem). Fix generated typos early.
- Code style: small methods, explicit parameter validation, and simple readable LINQ expressions. Keep single responsibility per class.

Build & run (commands)
- dotnet build
- dotnet run --project WebApiShop
- dotnet test

Visual Studio
- Open the solution, use __Solution Explorer__.
- Build via __Build > Build Solution__.
- Run/debug via __Debug > Start Debugging__ or __Debug > Start Without Debugging__.
- Manage NuGet via __Tools > NuGet Package Manager > Manage NuGet Packages for Solution__.
- Run tests via __Test Explorer__.

Recommended immediate tasks (prioritized)
1. Move the connection string from DbContext to configuration (appsettings.Development.json) and remove the literal from Repository\Store329391924contextContext.cs.
2. Consolidate and fix Entity model files (remove duplicates, correct nullability and typos).
3. Run a full build and fix any interface/implementation mismatches.
4. Add a top-level README.md with run/test instructions and example appsettings.Development.json.
5. Add a CI workflow (GitHub Actions) to build and run tests on PRs.





