using fitness_terem.ViewModel;

namespace fitness_terem.View;

public partial class ClientDetails : ContentPage
{
	public ClientDetails(ClientDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}