using Api.Data;
using Api.Extensions;
using Api.Middleware;
using Api.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();
builder.Services.AddDbContext<ApplicationDbContext>(i=> i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<TenantDbContext>(i=> i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.MigrateAll(builder.Configuration);
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddFastEndpoints();
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
app.UseMiddleware<TenantResolver>();
//app.UseMiddleware<TenantRouteResolver>();
app.UseFastEndpoints();
app.Run();

