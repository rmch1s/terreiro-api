using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Terreiro.Persistence.Data;
using Terreiro.Presentation.Configuration;
using Terreiro.Presentation.Filter;
using Terreiro.Presentation.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationDependencies();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseWrapperFilter>();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Terreiro.Application"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

WebApplication app = builder.Build();

using var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<TerreiroDbContext>();
context.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
