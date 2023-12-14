using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Repositories;
using MS.SearchSolution.BE.Repositories.Interfaces;
using MS.SearchSolution.BE.Services;
using MS.SearchSolution.BE.Services.Interfaces;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPersonsRepository, PersonsRepository>();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var singleErrorMessage = string.Join(" ", context.ModelState
        .Select(keyValuePair => keyValuePair.Value?.Errors)
        .OfType<ModelErrorCollection>()
        .SelectMany(modelErrorCollection => modelErrorCollection)
        .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage));

        return new BadRequestObjectResult(new ErrorResponse(StatusCodes.Status400BadRequest, singleErrorMessage));
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
