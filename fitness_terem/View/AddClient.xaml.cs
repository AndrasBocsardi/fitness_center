using fitness_terem.ViewModel;

namespace fitness_terem.View;

public partial class AddClient : ContentPage
{
	public AddClient(AddClientViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}