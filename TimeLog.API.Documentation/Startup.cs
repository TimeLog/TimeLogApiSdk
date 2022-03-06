using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation;

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
        services.AddSingleton<IReportingManager, ReportingManager>();
        services.AddSingleton<ITransactionalManager, TransactionalManager>();
        services.AddSingleton<IRestManager, RestManager>();
        services.AddRazorPages().AddRazorRuntimeCompilation();
        services.Configure<RouteOptions>(config =>
        {
            config.LowercaseUrls = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
        AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}