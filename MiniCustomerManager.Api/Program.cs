using MiniCustomerManager.Api.Models;
using MiniCustomerManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CustomerService>();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new { message = "Mini Customer Management API is running" }));
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapGet("/env", () => Results.Ok(new { 
    Environment = app.Environment.EnvironmentName 
}));

app.MapGet("/config", (IConfiguration config) => Results.Ok(new { 
    AppName = config["AppSettings:AppName"],
    BaseUrl = config["AppSettings:BaseUrl"] 
}));

app.MapControllers();

app.Run();