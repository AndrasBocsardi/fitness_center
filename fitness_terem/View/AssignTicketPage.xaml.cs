using fitness_terem.ViewModel;

namespace fitness_terem.View;

public partial class AssignTicketPage : ContentPage
{
	public AssignTicketPage(AssignTicketPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}