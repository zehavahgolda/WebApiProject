# Microservices plan — governance file

Service Division: You (the Agent) must analyze the current codebase and propose a logical division into microservices based on your understanding.

Technology Choice: For each microservice, you are free to choose any programming language or tech stack you deem most appropriate.

Approval Gate: You are strictly forbidden from creating files, modifying code, or proceeding with the implementation until you present the full plan (services + languages) and receive my explicit approval.


---

Proposed microservice division (summary)

1. API Gateway
   - Purpose: single entrypoint; routing, CORS, TLS, basic auth checks, rate limiting.
   - Tech options: YARP/Ocelot (dotnet) or Envoy/Kong.

2. Product / Catalog Service
   - Purpose: products, categories, search, paging/filtering.
   - Tech: .NET 9, EF Core, SQL Server/Postgres. Expose REST + OpenAPI.

3. User & Auth Service
   - Purpose: user registration, login, password rules, token issuance.
   - Tech: .NET 9 (can reuse existing services) or Node.js (Express) for auth libraries. Use JWT/OAuth2 as required.

4. Order Service
   - Purpose: orders, order items, lifecycle and payment orchestration.
   - Tech: .NET 9, EF Core. Emit async events (order-created) for downstream consumers.

5. Rating / Review Service
   - Purpose: product ratings and reviews; aggregation logic.
   - Tech: .NET 9 or Node.js; store in SQL or document DB (Mongo) if flexible schema preferred.

6. Media (Image) Service
   - Purpose: file uploads, thumbnails, storage (S3/Azure Blob), CDN-ready URLs.
   - Tech: Node.js or Go for efficient file I/O; store in cloud object storage.

7. Notification Service (optional)
   - Purpose: emails, push, async jobs triggered by events.
   - Tech: .NET worker or Node.js background worker; use message queue.

8. Reporting / Analytics Service (optional)
   - Purpose: reporting and heavy queries; separate analytical datastore.
   - Tech: Python or .NET; use OLAP-friendly store.

Cross-cutting components
- Shared contracts: OpenAPI/DTO package or gRPC proto for internal contracts.
- Event bus: RabbitMQ or Kafka for async events.
- Observability: centralized logging (ELK/Loki), Prometheus/Grafana, distributed tracing (Jaeger).
- CI/CD: GitHub Actions per service, Docker images, deploy to Kubernetes or compose for dev.
- Secrets/config: environment variables, Secret Manager, or k8s Secrets. No hard-coded secrets.

Phased migration (high level)
- Phase 0: governance, shared contracts, CI templates, Dockerfile examples.
- Phase 1: extract Product/Catalog service (lowest coupling).
- Phase 2: extract User & Auth service.
- Phase 3: extract Order service and introduce events.
- Phase 4: extract Media, Ratings, Notifications.
- Phase 5: cutover and decommission monolith endpoints, harden infra.

