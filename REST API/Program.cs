using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Application.UseCases;
using Application.Validators;
using DataMapper;
using Domain.Workout;
using FluentValidation;
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
builder.Services.AddScoped<IAdminUseCase, AdminUseCase>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddTransient<IDataMapper<IWorkout>, WorkoutDataMapper>();
builder.Services.AddTransient<IValidator<IWorkout>, WorkoutValidator>();

builder.Services.AddSingleton<Persistence.IConfigurationProvider, AppConfigurationProvider>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UpdateExercise", policy =>
        policy.RequireClaim("permissions", "update:exerciseactive"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://traenings-app.eu.auth0.com/";
    options.Audience = "https://traenings-app-backend.com";
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
