using Microsoft.EntityFrameworkCore;
using Student_Management_System.Service.AutoMapperProfile;
using Student_Management_System.Service.Interface;
using Student_Management_System.Service.Services;
using Studnet_Management_System.Model;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Student_Management_System.Service.DTO;
using Microsoft.OpenApi.Models;
using Student_Management_System.Service.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigureJwtAuthService(builder.Services);


// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp",
      policy =>
      {
          policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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
builder.Services.AddScoped<ITeacherService,TeacherService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAttendenceRepository, AttendanceRepository>();
builder.Services.AddScoped<IGradeBookRepository, GradeBookRepository>();
builder.Services.AddScoped<IMailsService, MailsService>();

builder.Services.AddSingleton(builder.Configuration.GetSection("MailConfig").Get<EmailConfiguration>());

builder.Services.Configure<JWTConfigDTO>(builder.Configuration.GetSection("JWTConfig"));

builder.Services.AddScoped<IRTokenRepository, RTokenRepository>();
builder.Services.AddScoped<IRTokenService, RTokenService>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagementSystem", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
                    Type=ReferenceType.SecurityScheme,
                    Id ="Bearer",
            },
            Scheme="oauth2",
            Name="Bearer",
            In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
void ConfigureJwtAuthService(IServiceCollection services)
{
    var audienceConfig = builder.Configuration.GetSection("JWTConfig");
    var symmetricKeyAsBase64 = audienceConfig["SecretKey"];
    var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
    var signingKey = new SymmetricSecurityKey(keyByteArray);

    var TokenvalidationParameter = new TokenValidationParameters
    {
        // The signing key must match!
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        // Validate the JWT Issuer (iss) claim
        ValidateIssuer = true,
        ValidIssuer = audienceConfig["Issuer"],

        //Validate the JWT Audiance (Audo) claim
        ValidateAudience = true,
        ValidAudience = audienceConfig["Aud"],

        //validate the token expire
        ValidateLifetime = true,
        RoleClaimType = "Role",
        ClockSkew = TimeSpan.Zero
    };

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = audienceConfig["Issuer"],
            ValidateAudience = false,
            ValidAudience = audienceConfig["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            RoleClaimType = "Role",
            ClockSkew = TimeSpan.Zero
        };
    });
}
