using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Application.UseCases.Tag.EnableTag;
using Finance.Application.UseCases.Tag.GetTag;
using Finance.Application.UseCases.Tag.SerachTags;
using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Repositories;
using Finance.Presentation.API.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiGlobalExceptionFilter)))
    .AddJsonOptions(jsonOptions =>
    {
        //jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCasePolicy();
    });

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FinanceContext>(options =>
{
    options.UseInMemoryDatabase("memory");
});

builder.Services.AddScoped<IUnitOfWork, FinanceContext>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ILimitRepository, LimitRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICreateTagHandler, CreateTagHandler>();
builder.Services.AddScoped<IDisableTagHandler, DisableTagHandler>();
builder.Services.AddScoped<IEnableTagHandler, EnableTagHandler>();
builder.Services.AddScoped<IGetTagHandler, GetTagHandler>();
builder.Services.AddScoped<ISearchTagsHandler, SearchTagsHandler>();
builder.Services.AddScoped<IUpdateTagHandler, UpdateTagHandler>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


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
