using fitness_terem.ViewModel;

namespace fitness_terem;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel viewModel)
	{
		InitializeComponent();
		
        BindingContext = viewModel;
    }

    
}