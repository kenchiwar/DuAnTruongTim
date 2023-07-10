using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<CheckQlgiaoVuContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddScoped<AccountService, AccountServiceImpl>();
builder.Services.AddScoped<DepartmentService, AccountServiceImpl>();
var app = builder.Build();
app.MapControllers();
app.UseCors(builder => builder .AllowAnyHeader() .AllowAnyMethod() .SetIsOriginAllowed((host) => true) .AllowCredentials() );
app.UseStaticFiles();
app.Run();




