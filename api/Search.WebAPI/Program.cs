using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Search.Core;
using Search.Core.Interfaces;
using System.Text;

namespace Search.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Addition of services to the container.
            //Configuring web api client
            builder.Services.AddHttpClient(HttpClient_Name.Default, config => ConfigureHttpClient(config, builder.Configuration));            
            builder.Services.AddScoped<IWebAPIClient, WebAPIClient>();
            //Enabling response cashing
            builder.Services.AddResponseCaching();
            //Configuing cache profiles for controllers
            builder.Services.AddControllers((config) => ConfigureControllers(config, builder.Configuration));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle                        
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
            if (builder.Configuration.GetSection("AllowedOrigins").GetChildren() != null)
            {
                StringBuilder origins = new StringBuilder();
                foreach (IConfigurationSection sec in builder.Configuration.GetSection("AllowedOrigins").GetChildren())
                {
                    origins.Append(sec.Value);
                }
                app.UseCors(x => x
                          .WithOrigins(origins.ToString())
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader());
            }
            app.UseAuthorization();

            //Adding to the pipeline the ability to caching responses
            app.UseResponseCaching();

            app.MapControllers();

            app.Run();
        }

        /// <summary>
        /// Adding the cache profiles configured at appsettings for controllers
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public static void ConfigureControllers(MvcOptions options, IConfiguration configuration)
        {
            var cacheProfiles = configuration.GetSection("CacheProfiles").GetChildren();
            foreach (var profile in cacheProfiles)
            {
                string? locationValue = profile.GetSection("Location").Value;
                if (int.TryParse(profile.GetSection("Duration").Value, out int duration)
                   && !string.IsNullOrEmpty(locationValue) && (locationValue.Equals("Any") || locationValue.Equals("Client"))
                    )
                {
                    ResponseCacheLocation location = (locationValue.Equals("Client") ? ResponseCacheLocation.Client : ResponseCacheLocation.Any);
                    var qsKeys = profile.GetSection("VaryByQueryKeys").GetChildren();
                    List<string> queryStringKeys = new List<string>();
                    foreach (var qs in qsKeys)
                    {
                        string? queryParam = qs.Value;
                        if (!string.IsNullOrEmpty(queryParam))
                            queryStringKeys.Add(queryParam);
                    }
                    options.CacheProfiles.Add(profile.Key, new CacheProfile()
                    {
                        Duration = duration,
                        Location = location,
                        VaryByQueryKeys = (queryStringKeys.Count() > 0 ? queryStringKeys.ToArray() : null)
                    });
                }
            }
        }
        public static void ConfigureHttpClient(HttpClient options, IConfiguration configuration)
        {
            double timeOut = 2;
            if (!string.IsNullOrEmpty(configuration.GetSection("General:HttpClient:TimeOutInMinutes").Value)
                && double.TryParse(configuration.GetSection("General:HttpClient:TimeOutInMinutes").Value, out double time)
                )
                timeOut = time;
            options.Timeout = TimeSpan.FromMinutes(timeOut);
        }
    }
}