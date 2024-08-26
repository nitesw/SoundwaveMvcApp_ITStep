using Core.MapperProfiles;
using Data.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Services;
using Microsoft.AspNetCore.Identity;
using Data.Entities;
using Core.Interfaces;
using SoundwaveMvcApp_ITStep.SeedExtensions;
using Core.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SoundwaveMvcApp_ITStep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // For local use
            string? connectionString = builder.Configuration.GetConnectionString("LocalDb");

            // For hosting use
            //string? connectionString = builder.Configuration.GetConnectionString("SomeeComDb");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<SoundwaveDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<SoundwaveDbContext>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAutoMapper(typeof(AppProfile));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddScoped<ILikesService, LikesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IMusicService, MusicService>();
            builder.Services.AddScoped<IPlaylistsService, PlaylistsService>();
            builder.Services.AddScoped<IFilesService, FilesService>();
            builder.Services.AddScoped<IEmailSender, EmailService>();

            var app = builder.Build();

            // Seed Initial Data
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.SeedRoles().Wait();
                scope.ServiceProvider.SeedAdmin().Wait();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
