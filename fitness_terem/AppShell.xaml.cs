using fitness_terem.View;

namespace fitness_terem;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
		Routing.RegisterRoute(nameof(AddClient), typeof(AddClient));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
    }
}
