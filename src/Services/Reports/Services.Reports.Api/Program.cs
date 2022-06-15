using Microsoft.OpenApi.Models;
using Services.Reports.Application.DependencyInjection;
using Services.Reports.Infrastructure.DependencyInjection;
using Shared.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Phone Directory Reporting API", Version = "v1"});
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneDirectory.Reporting.Api v1"));
app.UseShared();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();