using DemoSession2_Paypal.Converters;
using DuAnTruongTim.Middleware;
using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSession();
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<CheckQlgiaoVuContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddScoped<AccountService, AccountServiceImpl>();
builder.Services.AddScoped<DepartmentService, DepartmentServiceImpl>();
builder.Services.AddScoped<RequestService, RequestServiceImpl>();
builder.Services.AddScoped<RoleClaimService, RoleClaimServiceImpl>();
builder.Services.AddScoped<RoleService, RoleServiceImpl>();
builder.Services.AddScoped<RequestFileServicecs, RequestFileServiceImpl>();
builder.Services.AddScoped<AuthorizeAccount>();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.MaxDepth = 64;
//});

var app = builder.Build();
app.MapControllers();
app.UseCors(builder => builder .AllowAnyHeader() .AllowAnyMethod() .SetIsOriginAllowed((host) => true) .AllowCredentials() );
app.UseStaticFiles();
app.UseMiddleware<AddAccountLoginMiddleware>();
//app.UseSession();
app.Run();




