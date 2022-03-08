using LibraryPortal.Services;
using LibraryPortal.Services.Interfaces;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TelemetryKitchenSink;

namespace LibraryPortal
{
	public class Startup
	{
        private const string ServiceName = "library-portal";
        public const string ActivitySourceName = "frontend";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();

            services.AddHttpClient("BookService", config =>
            {
                config.BaseAddress = new Uri(Configuration["ApiSettings:CatalogServiceAddress"]);
            });

            services.AddControllers();

            services.AddOpenTelemetryTracing(
                (builder) => builder
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.Enrich = TelemetryAspNetCoreEnricher.EnrichHttpRequests;
                    })
                    .AddHttpClientInstrumentation()           
                    .AddSource(ActivitySourceName)
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}