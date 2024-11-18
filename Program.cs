using Microsoft.EntityFrameworkCore;
using RZA_OMwebsite.Components;
using RZA_OMwebsite.Models;
using MudBlazor.Services;
using MudBlazor;
using RZA_OMwebsite.Services;
using RZA_OMwebsite.Utilities;
using RZA_OMwebsite.Pages;

namespace RZA_OMwebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });



            builder.Services.AddDbContext<TlS2303831RzaContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
                new MySqlServerVersion(new Version(8, 0, 29))));
            #region hidden
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<UserSession>();
            builder.Services.AddScoped<PageService>();
            builder.Services.AddScoped<AttractionService>();
            builder.Services.AddScoped<RoombookingService>();
            builder.Services.AddScoped<RoomService>();


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }

    }
}
