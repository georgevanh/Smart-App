using Asp.Versioning;
using SmartExercise.Server.Data;
using Microsoft.EntityFrameworkCore;
using SmartExercise.Server.Repositories;
using SmartExercise.Server.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartExercise.Server.Models;

using SmartExercise.Server.Utilities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SmartExercise.Server.Configuration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Read API key from configuration
            string apiKey = Configuration.GetSection("APIKey")?.Value ?? String.Empty;
            
            string trustedDomainsString = Configuration.GetSection("TrustedDomains")?.Value ?? string.Empty;

            string[] trustedDomainsArr = trustedDomainsString
                .Split(',')
                .Select(domain => domain.Replace("\\", "").Replace("\"", "").Trim('\"').Trim()) // Remove escape characters and single quotes
                .ToArray();

            services.AddAuthentication(ApiKeyAuthenticationHandler.AuthenticationScheme)
                .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                    ApiKeyAuthenticationHandler.AuthenticationScheme,
                    options => options.ApiKey = apiKey);
            services.AddControllers();
            
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddEndpointsApiExplorer();
            

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddTransient<IValidator<CustomerDto>, CustomerDtoValidator>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart API", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "API key needed to access the endpoints. Enter your API key in the header.",
                    Name = "Api-Key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            // Configure CORS to allow sharing with specific domains
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(trustedDomainsArr)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("/index.html");
            });

        }
    }

}
