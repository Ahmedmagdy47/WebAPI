using Hangfire;
using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;
namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);

            //builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
            );

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization =
                [
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = app.Configuration.GetValue<string>("HangFireSettings:Username"),
                        Pass = app.Configuration.GetValue<string>("HangFireSettings:Password")
                    }
                ],
                DashboardTitle = "WebAPI Dashboard",
            });

            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            //RecurringJob.AddOrUpdate("SendNewPollNotification", () => notificationService.SendNewPollNotificationAsync(null), Cron.Daily);

            //CORS middleware should be placed before any other middleware that handles requests, such as authentication or authorization middleware.
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler();

            app.UseRateLimiter();

            app.MapHealthChecks("health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
