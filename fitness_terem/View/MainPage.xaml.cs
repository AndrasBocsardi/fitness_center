using fitness_terem.ViewModel;

namespace fitness_terem;

public partial class MainPage : ContentPage
{
	

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}

