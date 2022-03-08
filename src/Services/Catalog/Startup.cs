using Catalog.DB;
using Catalog.Repository;
using Catalog.Repository.Interfaces;
using Catalog.Services;
using Catalog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TelemetryKitchenSink;

namespace Catalog
{
    public class Startup
    {
        private const string ServiceName = "catalog-api";
        public const string ActivitySourceName = "catalog";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookService, BookService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<BookDbContext>(options =>
                options.UseInMemoryDatabase("Books"));

            var logger = services.BuildServiceProvider().GetRequiredService<ILogger<TelemetryBaggageLogger>>();

            services.AddOpenTelemetryTracing(
                (builder) => builder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource(ActivitySourceName)
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
                    .AddProcessor(new TelemetryBaggageLogger(logger))
                    .AddConsoleExporter()
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}