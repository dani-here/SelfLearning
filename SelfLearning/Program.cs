using Microsoft.AspNetCore.RateLimiting;
using SelfLearning.Configurations;
using SelfLearning.Middlewares;

namespace SelfLearning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitializeApp(args);
        }

        public static void InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning();

            builder.Services.AddRateLimiter(rateLimiterOptions => {
                rateLimiterOptions.AddConcurrencyLimiter("concurrency", options =>
                {
                    options.QueueLimit = 5;
                    options.PermitLimit = 5;
                    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                });
            });

            // Adding it for the application DI
            builder.Services.Configure<RateLimitingConfiguration>(builder.Configuration.GetSection(RateLimitingConfiguration.ConfigSectionName));

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
            app.UseMiddleware<LoggingMiddleware>();

            var rateLimitingOptions = builder.Configuration.GetSection(RateLimitingConfiguration.ConfigSectionName);
            app.UseRateLimiter();

            app.Run();
        }
    }

}

