﻿using VerificaApp.ViewModels;
using VerificaApp.Views;
using Microsoft.Extensions.Logging;
using VerificaApp.Helpers;
using VerificaApp.VieModels;
using CommunityToolkit.Maui;

namespace VerificaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesome6FreeSolid900.otf", "FontSolid");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<SmsHandlerPage>();
            builder.Services.AddTransient<SmsHandlerViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<IVerificaAppService, VerificaAppService>();
#if ANDROID
            builder.Services.AddSingleton<ISMSHandler, Platforms.Android.SMSHandler>();
#endif
            return builder.Build();
        }
    }
}