using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace AdWorksCore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            // initialized in Program.cs
            Configuration = configuration;
            Debug.WriteLine("Connection String: " + Configuration.GetConnectionString("AdWorksConnection"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.AddMvcCore();
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            // order matters here
            // app.UseDefaultFiles(); - only useful when NOT serving from MVC
            app.UseStaticFiles();

            // app.UseMvcWithDefaultRoute();
            app.UseMvc(config =>
            {
                config.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", Action = "Index" });
            });

            
        }
    }
}
