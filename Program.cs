using ApiProj.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow requests from the Blazor app's domain
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7106") // Update to match the Blazor app's URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Allow credentials (cookies, authentication headers)
    });
});

// Add services for the application
builder.Services.AddSingleton<PizzaService>();
builder.Services.AddSingleton<ToppingService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Enable CORS for the app
app.UseCors("AllowBlazorApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
