using Finance.Application.Services;
using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using Finance.Presentation.API.Filters;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateTagHandler).Assembly));

builder.Services.AddDbContext<FinanceContext>(options =>
{
    options.UseInMemoryDatabase("memory");
});

builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<ITokenService, JwtBearerAdapter>();

builder.Services.AddScoped<IUnitOfWork, FinanceContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ILimitRepository, LimitRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

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
