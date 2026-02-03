using API.Data;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

//Exception won't occur everytime, so we use Transient, by this we make sure that service instance is created only when exception occurs.
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(opt =>
{
    //AllowCredentials() allows the client to send credentials (cookies, auth headers, TLS certs) with cross-origin requests.
    //Without AllowCredentials() 
    //1) Cookies will NOT be sent
    //2) Session-based auth will fail
    //3) fetch(..., { credentials: 'include' }) won’t work
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:3000"); 
});

app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
