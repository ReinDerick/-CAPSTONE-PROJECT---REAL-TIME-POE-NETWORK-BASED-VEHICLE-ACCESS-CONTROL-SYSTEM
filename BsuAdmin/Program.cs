using BsuAdmin.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://192.168.0.103:5000",   // your frontend IP
            "http://192.168.1.176:5000",
            "http://192.168.0.106:5000",
            "http://192.168.0.102:5000",
            "http://172.20.10.2:5000",
            "http://172.16.246.94:5000",
            "http://192.168.0.100:5000",
            "http://192.168.0.108:5000",
            "http://172.16.214.46:5000",// optional: another frontend device
            "http://192.168.0.101:5000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();


app.MapHub<LogHub>("/loghub");

app.Run();
