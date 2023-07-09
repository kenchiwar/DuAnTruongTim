var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors();
var app = builder.Build();
app.MapControllers();
app.UseCors(builder => builder .AllowAnyHeader() .AllowAnyMethod() .SetIsOriginAllowed((host) => true) .AllowCredentials() );
app.UseStaticFiles();
app.MapGet("/", () => "Hello World!");

app.Run();




