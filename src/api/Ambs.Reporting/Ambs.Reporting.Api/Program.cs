using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ambs.Reporting.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CORS policy added
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType));
});

builder.Services.AddDatabase(builder.Configuration)
    .AddRepositories()
    .AddServices()
    .AddLogics();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(int.Parse(builder.Configuration["Api:Version:Major"]), int.Parse(builder.Configuration["Api:Version:Minor"]));
    config.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
