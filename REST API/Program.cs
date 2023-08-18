using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Application.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using REST_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IExerciseUseCase, ExerciseUseCase>();
builder.Services.AddScoped<IWorkoutUseCase, WorkoutUseCase>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddSingleton<Persistence.IConfigurationProvider, AppConfigurationProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://VORES_AUTH0_DOMAIN/";
    options.Audience = "VORES_API_IDENTIFIER(AUDIENCE_VALUE_PÅ_AUTH0_API_DASHBOARD";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
