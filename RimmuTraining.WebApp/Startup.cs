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
using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
using RimmuTraining.WebApp.Domain.Practices;

namespace RimmuTraining.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsEnvironment("DevelopmentLocal"))
            {
                // Arrange dev data
                services.AddDbContext<RimmuDbContext>(options =>
                    options.UseInMemoryDatabase("RimmuDb"));
            }
            else
            {
                var connectionString = Configuration["prodDb"];
                services.AddDbContext<RimmuDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            
            services.AddDefaultIdentity<RimmuUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddRoles<RimmuRole>()
                .AddEntityFrameworkStores<RimmuDbContext>();
            services.AddAuthorization(config =>
            {
                config.AddPolicy("IsTrainer", policy => policy.RequireRole("Fighting Trainer", "Archery Trainer", "Admin"));
                config.AddPolicy("IsAdmin", policy => policy.RequireRole("Admin"));
            });
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
            services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                }) // from v0.6.0-preview4
                .AddBulmaProviders()
                .AddFontAwesomeIcons();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<RimmuUser>>();
            services.AddScoped<IQueryHandler<GetMembers, List<Domain.Members.Member>>, GetMembersQueryHandler>();
            services.AddScoped<IQueryHandler<GetUnconnectedUsers, List<User>>, GetUnconnectedUsersQueryHandler>();
            services.AddScoped<IQueryHandler<GetMemberById, Domain.Members.Member>, GetMemberByIdQueryHandler>();
            services.AddScoped<ICommandHandler<MakeTrainer>, MakeTrainerCommandHandler>();
            services.AddScoped<ICommandHandler<ConnectMemberToUser>, ConnectMemberToUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateMember>, CreateMemberCommandHandler>();
            services.AddScoped<ICommandHandler<AddToRole>, AddToRoleCommandHandler>();
            services.AddScoped<ICommandHandler<StartPractice>, StartPracticeCommandHandler>();
            services.AddScoped<IQueryHandler<GetPractices, List<PracticeListItem>>, GetPracticesQueryHandler>();
            services.AddScoped<IQueryHandler<GetPracticeById, PracticeDetailItem>, GetPracticeByIdQueryHandler>();
            services.AddScoped<ICommandHandler<RegisterAttendance>, RegisterAttendanceCommandHandler>();
            services.AddScoped<ICommandHandler<RemoveAttendance>, RemoveAttendanceCommandHandler>();
            services.AddScoped<MembersService>();
            services.AddScoped<PracticesService>();

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

            app.ApplicationServices.UseBulmaProviders().UseFontAwesomeIcons();

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
        //private void AddRoles(IApplicationBuilder app)
        //{
        //    using (var scope = app.ApplicationServices.CreateScope())
        //    {
        //        var roleManager = scope.ServiceProvider.GetService<RoleManager<RimmuRole>>();
        //        var result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Admin",
        //            NormalizedName = "ADMIN"
        //        }).Result;
        //        result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Admin",
        //            NormalizedName = "ADMIN"
        //        }).Result;
        //        result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Archer",
        //            NormalizedName = "ARCHER"
        //        }).Result;
        //        result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Archery Trainer",
        //            NormalizedName = "ARCHERY TRAINER"
        //        }).Result;
        //        result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Fighter",
        //            NormalizedName = "Fighter"
        //        }).Result;

        //        result = roleManager.CreateAsync(new RimmuRole
        //        {
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            Id = Guid.NewGuid(),
        //            Name = "Fighting",
        //            NormalizedName = "Fighting Trainer"
        //        }).Result;
        //    }
        //}
    }
    
}
