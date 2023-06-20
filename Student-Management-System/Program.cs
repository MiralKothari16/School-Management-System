using Microsoft.EntityFrameworkCore;
using Student_Management_System.Service.AuoMapperProfile;
using Student_Management_System.Service.Interface;
using Student_Management_System.Service.Services;
using Studnet_Management_System.Model;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<StudMgtContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLAuth"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//builder.Services.AddScoped<IUserService, UserService>();  
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


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