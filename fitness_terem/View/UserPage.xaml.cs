using fitness_terem.ViewModel;

namespace fitness_terem.View;

public partial class UserPage : ContentPage
{
	public UserPage(UserPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}