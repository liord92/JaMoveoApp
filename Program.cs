


using JaMoveoApp.Extensions;
using JaMoveoApp.Interfaces;
using JaMoveoApp.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISongsService,SongsService>();

var app = builder.Build();

app.UseCors(policy => policy
  .WithOrigins("http://localhost:4200")
  .AllowAnyHeader()
  .AllowAnyMethod()
  .AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapAllHubs();
});

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
