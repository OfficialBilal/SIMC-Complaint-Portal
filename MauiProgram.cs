using Complaint_Portal.MVVM.ViewModel;
using Complaint_Portal.Pages;
using Firebase.Database;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace Complaint_Portal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("harmony_regular.ttf", "Harmony");
                    fonts.AddMaterialSymbolsFonts();
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton(new FirebaseClient("https://complaint-portal-b639e-default-rtdb.asia-southeast1.firebasedatabase.app/"));

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ViewComplaints>();
            builder.Services.AddSingleton<ComplainerDashboard>();
            builder.Services.AddSingleton<MainPage>();
#endif

            return builder.Build();
        }
    }
}
