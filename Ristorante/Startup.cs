using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ristorante.Data;
using Ristorante.EmailSender;
using Ristorante.Repository;

namespace Ristorante
{
    public class Startup
    {
       
        public static int Conferma = 0;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RistoranteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<RistoranteContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<SignInManager<IdentityUser>>();
            services.AddScoped<UserManager<IdentityUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IRistoranteRepository, RistoranteRepository>();

            services.AddFluentEmail(Configuration["FluentEmail:FromEmail"],
                                    Configuration["FluentEmail:FromName"])
                    .AddRazorRenderer()
                    .AddSmtpSender(Configuration["FluentEmail:SmtpSender:Host"],
                         int.Parse(Configuration["FluentEmail:SmtpSender:Port"]),
                                   Configuration["FluentEmail:SmtpSender:Username"],
                                   Configuration["FluentEmail:SmtpSender:Password"]);
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            services.AddControllersWithViews();
            services.AddScoped<RistoranteRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
