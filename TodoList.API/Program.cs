using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TodoList.API.Data;
using TodoList.API.Extensions;
using TodoList.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TodoListDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITaskRepository, TaskRepository>();

var app = builder.Build();

app.MigrateDbContext<TodoListDbContext>(async (context, services) =>
{
    var logger = services.GetService<ILogger<TodoListDbContextSeed>>();
    new TodoListDbContextSeed().SeedAsync(context, logger).Wait();
});

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
