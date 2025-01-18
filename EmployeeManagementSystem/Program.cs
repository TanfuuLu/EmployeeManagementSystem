using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Mapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.RepoPattern;
using EmployeeManagementSystem.Services.RepoPattern.EmployeeServices;
using EmployeeManagementSystem.Services.RepoPattern.HRServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => {
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
	options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<EmsDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("EmsDatabase")));
builder.Services.AddScoped<TokenJWTGenerator>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IHRServiceRepository, HRServiceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//Config cho identity entity 
builder.Services.AddIdentity<Employee, IdentityRole>(options => {
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<EmsDataContext>()
	.AddDefaultTokenProviders();

//Config for (Jwt token)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings["Issuer"],
			ValidAudience = jwtSettings["Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
		};
		options.Events = new JwtBearerEvents {
			OnMessageReceived = context => {
				context.Token = context.HttpContext.Request.Cookies["AuthToken"];
				return Task.CompletedTask;
			}
		};
	});


builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.MapScalarApiReference();
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
