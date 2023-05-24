using fitness_terem.View;
using fitness_terem.ViewModel;

namespace fitness_terem;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = FileAccessHelper.GetLocalFilePath("fitness.db3");
		builder.Services.AddSingleton<FitnessRepository>(s => ActivatorUtilities.CreateInstance<FitnessRepository>(s, dbPath));

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<AdminPage>();
        builder.Services.AddSingleton<AdminPageViewModel>();

		builder.Services.AddSingleton<AddClient>();
		builder.Services.AddSingleton<AddClientViewModel>();

        builder.Services.AddSingleton<UserPage>();
        builder.Services.AddSingleton<UserPageViewModel>();

        builder.Services.AddTransient<ClientDetails>();
        builder.Services.AddTransient<ClientDetailsViewModel>();

        builder.Services.AddTransient<AssignTicketPage>();
        builder.Services.AddTransient<AssignTicketPageViewModel>();


        return builder.Build();
	}
}
