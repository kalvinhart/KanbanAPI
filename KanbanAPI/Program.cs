using KanbanAPI.DataAccess;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using KanbanAPI.Extensions.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KanbanDbContext>(
    opts =>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMappers();

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

app.UseExceptionHandler("/errors");

app.UseAuthorization();

app.MapControllers();

app.Run();