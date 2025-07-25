﻿using GEO_DROID.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;

using Microsoft.Maui.LifecycleEvents;
using GEO_DROID.Services.SincroService;
using GeoDroid.Data.SQL;
using Fluxor;
using Microsoft.FluentUI.AspNetCore.Components;


#if ANDROID
using GEO_DROID.Platforms.Android.Handlers;
#endif

namespace GEO_DROID
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            try
            {
                var builder = MauiApp.CreateBuilder();
                builder.UseMauiApp<App>().ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                }).ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<BlazorWebView, MauiBlazorWebViewHandler>();
#endif                
                }).ConfigureLifecycleEvents(events =>
                {
                    static void LogEvent(string eventName, string type = null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                    }
                });
                builder.Services.AddMauiBlazorWebView();

                builder.Services.AddBlazorWebViewDeveloperTools();
                builder.Services.AddLocalization();
                var currentAssembly = typeof(MauiProgram).Assembly;
                builder.Services.AddFluxor(options =>
                    options.ScanAssemblies(currentAssembly)
                );
                builder.Services.AddHttpClient();

                builder.Services.AddFluentUIComponents();
#if ANDROID
                builder.Services.AddTransient<BluetoothServiceBroker>();
#endif
                builder.Services.AddTransient<TokenService>();
                builder.Services.AddTransient<GeolineOdataService>();
                builder.Services.AddTransient<AveriasService>();
                builder.Services.AddTransient<TicketsService>();

                builder.Services.AddSingleton<BluetoothServiceManager>();
                builder.Services.AddSingleton<CustomNavigationManager>();
                builder.Services.AddSingleton<BluetoothServiceManager>();

#if ANDROID
                builder.Services.AddSingleton<SyncProcess>();
#endif
                //Creacion Base de datos
                builder.Services.AddSingleton<GeoDroidDatabase>();

                builder.Logging.AddDebug();
                return builder.Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}