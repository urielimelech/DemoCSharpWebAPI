global using Microsoft.EntityFrameworkCore;
using DemoWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace DemoWebApi
{
    internal class DemoWebApi
    {
        private readonly WebApplicationBuilder _builder;
        public DemoWebApi(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
            _builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowPolicy",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:4200/");
                    });
            });
            _builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization header using Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            _builder.Services.AddHttpContextAccessor();

            // JWT authenticator
            _builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(_builder.Configuration.GetSection("AppSettings:JWT_Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // DB connection
            _builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
        private static void Main(string[] args)
        {
            DemoWebApi demo = new DemoWebApi(args);

            var app = demo._builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}