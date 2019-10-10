using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RimmuTraining.WebApp.Areas.Identity;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Domain.Members;
using RimmuTraining.WebApp.Infrastructure;


namespace RimmuTraining.WebApp
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
            var connectionString = Configuration["prodDb"];
            services.AddDbContext<RimmuDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDefaultIdentity<RimmuUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<RimmuDbContext>();
            services.AddAuthorization(config =>
            {
                config.AddPolicy("IsTrainer", policy => policy.RequireRole("Fighting Trainer", "Archery Trainer"));
            });
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<RimmuUser>>();
            services.AddScoped<IQueryHandler<GetMembers, List<RimmuTraining.WebApp.Domain.Members.Member>>, GetMembersQueryHandler>();
            services.AddScoped<IQueryHandler<GetUnconnectedUsers, List<User>>, GetUnconnectedUsersQueryHandler>();
            services.AddScoped<ICommandHandler<MakeTrainer>, MakeTrainerCommandHandler>();
            services.AddScoped<ICommandHandler<ConnectMemberToUser>, ConnectMemberToUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateMember>, CreateMemberCommandHandler>();
            services.AddScoped<MembersService>();

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            builder.Build();
        }
    }
}
