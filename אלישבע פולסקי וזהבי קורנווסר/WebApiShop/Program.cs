// ═══════════════════════════════════════════════════════════════════════════
// CODE REVIEW — seeandbesharp-code-review (2026-06-17)
// ═══════════════════════════════════════════════════════════════════════════
// CRITICAL (A6):  All business logic is implemented directly in the controller
//                 (UsersController). Extract to IUserService / IUserRepository.
// CRITICAL (B3):  No authentication or authorization configured.
//                 Add AddAuthentication().AddJwtBearer(...) and apply [Authorize].
// CRITICAL (B8):  No global exception handling middleware registered.
//                 Add ErrorHandlingMiddleware or app.UseExceptionHandler().
// CRITICAL (A4):  No database used — data is persisted to a flat text file.
//                 Migrate to SQL Server + EF Core (AddDbContext<T>).
// MEDIUM   (C5):  File I/O in UsersController is synchronous (File.ReadAllLines,
//                 File.AppendAllText). Replace with async equivalents.
// ═══════════════════════════════════════════════════════════════════════════
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
