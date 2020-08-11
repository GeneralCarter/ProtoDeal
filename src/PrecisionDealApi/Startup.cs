using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using PrecisionDealApi.Models;
using PrecisionDealApi.Services;
using PrecisionDealApi.Utilities;

namespace PrecisionDealApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BsonClassMap.RegisterClassMap<IdentityUser<string>>(cm => 
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            BsonClassMap.RegisterClassMap<Property>(cm => 
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(p => p.UserId).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigurationSettings>(
            Configuration.GetSection(nameof(ConfigurationSettings)));

            services.AddSingleton<IConfigurationSettings>(sp =>
            sp.GetRequiredService<IOptions<ConfigurationSettings>>().Value);

            services.AddHttpContextAccessor();
            services.AddTransient<UserService>();
            services.AddTransient<PropertyService>();
            services.AddScoped<UserContext>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader();;
                    });
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new StringToDoubleConverter())
                );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:6565/";
                    options.ApiName = "pd_api";

                    options.RequireHttpsMetadata = false;
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10); // that's the default

                    options.JwtBearerEvents.OnTokenValidated = (context) =>
                    {
                        return Task.CompletedTask;
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IdentityModelEventSource.ShowPII = true;

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
