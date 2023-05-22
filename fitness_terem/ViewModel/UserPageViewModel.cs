using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace fitness_terem.ViewModel;
[QueryProperty(nameof(Client), "Client")]
public partial class UserPageViewModel : ObservableObject
{

    [ObservableProperty]
    Client client;
    [ObservableProperty]
    string email;
    [ObservableProperty]
    int clientId;
    
    //string gymName;
    //public string GymName
    //{
    //    get { return gymName; }
    //    set { SetProperty(ref gymName, value); }
    //}

    public ObservableCollection<ClientTicketWithGymName> ClientTickets { get; set; } = new();
    public UserPageViewModel()
    {
        email = MainViewModel.LoggedInUserEmail;
        ShowClient();
        ShowTickets();
       
    }
    [RelayCommand]
    async void ShowClient()
    {
        client = await App.FitnessRepo.GetClient(email);
        OnPropertyChanged(nameof(Client));
       
    }
    async void ShowTickets()
    {
        clientId = await App.FitnessRepo.GetUserIdByEmail(email);
        List<ClientTicket> tickets = await App.FitnessRepo.GetClientTickets(clientId);
        foreach (ClientTicket ticket in tickets)
        {
            Gym gym = await App.FitnessRepo.GetGymName(ticket.Gym_id);
            //gymName= gym.Name;
            ClientTicketWithGymName ticketWithGymName = new ClientTicketWithGymName
            {
                ClientTicket = ticket,
                GymName = gym.Name
            };
            Debug.WriteLine(gym.Name);
            ClientTickets.Add(ticketWithGymName);
        }

    }
   
}
public class ClientTicketWithGymName
{
    public ClientTicket ClientTicket { get; set; }
    public string GymName { get; set; }
}
