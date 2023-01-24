global using AlbarWebAPI.Data;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class AlbarWebApi
{
    private readonly WebApplicationBuilder _builder;
    public AlbarWebApi(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
        _builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();

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
        AlbarWebApi albar = new AlbarWebApi(args);

        var app = albar._builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}