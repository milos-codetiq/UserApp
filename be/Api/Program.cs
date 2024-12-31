using Api.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.AddInfrastructure();
builder.AddProjectServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors();

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = errorFeature?.Error;
        await ExceptionMiddleware.HandleException<Program>(exception!, context);
    });
});

//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();
