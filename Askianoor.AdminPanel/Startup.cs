using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Askianoor.AdminPanel.Data;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Toast;
using Microsoft.CodeAnalysis.Options;

namespace Askianoor.AdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredToast();

            services.AddScoped<GeneralService>();
            services.AddScoped<DashboardSettingService>();
            services.AddScoped<SocialNetworkService>();
            services.AddScoped<SkillService>();
            services.AddScoped<NavbarService>();
            services.AddScoped<EducationService>();
            services.AddScoped<ExperienceService>();
            services.AddScoped<PortfolioService>();
            services.AddScoped<PortfolioCategoryService>();

            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddCors();
            services.AddBlazoredSessionStorage();

            services.AddBlazorise(options => { options.ChangeTextOnKeyPress = false; })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services.AddAuthorizationCore();
            
            //services.AddHttpClient<AuthenticationStateProvider, AskianoorAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, AskianoorAuthenticationStateProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(builder =>
                builder.WithOrigins(Configuration["ApplicationSettings:ServerURL"].ToString())
                .AllowAnyHeader()
                .AllowAnyMethod()
                );

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ApplicationServices
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
