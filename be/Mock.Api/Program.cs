using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Mock.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//definition end points

app.MapGet("/api/sensor-data", (Guid ecosystemId) =>
{
    return MockService.GetSensorData(ecosystemId);
})
.WithName("GetSensorData")
.WithOpenApi();

//TODO: lets discuss how to structure aggregate data endpoints
//Option 1 - one endpoint with bit complex query parameters (POST)
//Option 2 - multiple endpoints (one for each use case)

app.MapGet("/api/aggregate-data", (Guid ecosystemId) => //TODO: add parameters for day, week, month, year - from and to
{
    return MockService.GetAggregateData(ecosystemId);
})
.WithName("GetAggregateData")
.WithOpenApi();

app.Run();
