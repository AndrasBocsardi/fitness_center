namespace fitness_terem;

public partial class App : Application
{
	public static FitnessRepository FitnessRepo;
	
	public App(FitnessRepository repo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		FitnessRepo= repo;
		
		
	}
}
