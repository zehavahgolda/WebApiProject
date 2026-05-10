# API Controllers — usage, conventions & guidance

Purpose
- Describe controller responsibilities, standard patterns and testing guidance for controllers in this repository.
- Keep controllers thin: orchestrate validation, mapping and calls to Services; business logic belongs in Services.

Routing & signatures
- Use attribute routing: [Route("api/[controller]")] on controller and descriptive action routes where needed.
- Prefer async ActionResult<T> signatures:
  - GET: Task<ActionResult<TDto>> GetById(int id)
  - LIST/PAGED: Task<ActionResult<PagedResult<TDto>>> Get([FromQuery] FilterDto filter)
  - POST: Task<ActionResult<TDto>> Post([FromBody] CreateDto dto)
  - PUT: Task<IActionResult> Put(int id, [FromBody] UpdateDto dto)
  - DELETE: Task<IActionResult> Delete(int id)
- Return correct status codes:
  - 200 OK with payload for successful GETs,
  - 201 Created + Location header for POST (CreatedAtAction),
  - 204 No Content for successful PUT/DELETE,
  - 400 Bad Request for validation errors,
  - 404 Not Found when resource is missing,
  - 401/403 only if authorization is enabled.

Model binding & validation
- Use DTOs for controller inputs/outputs. Do not expose EF entities directly.
- Validate inputs with data annotations and ModelState:
  - if (!ModelState.IsValid) return BadRequest(ModelState);
- Use [FromQuery], [FromBody], [FromRoute], [FromForm] explicitly when ambiguity may exist.

Mapping
- Centralize mapping in AutoMapper profiles. Controllers should call IMapper.Map between DTOs and domain models; mapping logic should not live in controller methods.

Dependency injection
- Inject only required services (IService, IMapper, ILogger<T>) via constructor.
- Keep DbContext out of controllers — use services/repositories.

Paging, filtering & sorting
- Standardize a small PagedRequest DTO: Page (1-based), PageSize.
- Services should expose methods returning (IEnumerable<T>, int totalCount) or a PagedResult<TDto> type encapsulating Items and TotalCount.
- Use IQueryable in repository layer; perform filtering before Count/Skip/Take.

Error handling & logging
- Use centralized error middleware to convert exceptions to consistent HTTP responses.
- Controllers should catch expected domain errors if they need to map to specific status codes; otherwise let middleware handle unexpected exceptions.
- Inject ILogger<T> and add contextual logs (user id, request id, key inputs).

File uploads
- For uploads use IFormFile and save under wwwroot/products (controllers already follow this pattern).
- Validate file size/type and use unique filenames (Guid + extension).
- Return relative URL or 201 Created with resource location.

Authorization
- This repository currently does not enforce JWT by default. If you add [Authorize], ensure Program.cs registers authentication and required Jwt configuration keys are present.
- For public endpoints omit [Authorize] or add clear comment describing expected auth behavior.

Testing controllers
- Unit tests: mock dependencies (services, mapper, logger) with Moq and assert ActionResult status and payload.
- Integration tests: use WebApplicationFactory<TEntryPoint> and an in-memory or test database; seed known data and assert end-to-end behavior.
- Prefer test names: MethodName_StateUnderTest_ExpectedBehavior.

Examples (patterns)
- Created response:
  - return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
- Not found:
  - if (resource == null) return NotFound();
- No content on success:
  - return NoContent();




