namespace SerilogExperiments
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Serilog;
    using SerilogExperiments.Filters;
    using SerilogExperiments.Middleware;
    using SerilogExperiments.Models;
    using SerilogExperiments.Repository;
    using SerilogExperiments.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1", 
                    new OpenApiInfo 
                    { 
                        Title = "Serilog Experiments", 
                        Version = "v1",                        
                    });
                c.OperationFilter<HeaderOperationFilter>();
            });

            services.AddSingleton<IRepository<ToDoItem>, ToDoRepository>();
            services.AddTransient<ISafeCallService, SafeCallService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serilog Experiments v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseMiddleware<HeaderLoggingMiddlewareV2>();
            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
