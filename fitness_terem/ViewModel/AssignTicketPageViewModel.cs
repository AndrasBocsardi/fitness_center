using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.Model;
using fitness_terem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

[QueryProperty(nameof(Client), "Client")]
public partial class AssignTicketPageViewModel : ObservableObject
{
    [ObservableProperty]
    Client client;

    [ObservableProperty]
    string ticketName;

    [ObservableProperty]
    int ticketPrice;

    [ObservableProperty]
    int nrOfDaysValid;

    [ObservableProperty]
    int nrOfEntryValid;

    [ObservableProperty]
    string startTimeOfDay;

    [ObservableProperty]
    string endTimeOfDay;

    [RelayCommand]
    async void AssignTicket()
    {
        TicketType ticketType = await App.FitnessRepo.AddNewTicket(TicketName, TicketPrice, NrOfDaysValid, NrOfEntryValid, StartTimeOfDay, EndTimeOfDay);

        if(await App.FitnessRepo.AssignTicketToClient(Client.Client_id, ticketType.Ticket_id, ticketType.Price) > 0){
            await Shell.Current.DisplayAlert("The ticket was assigned", "You will be redirected", "OK");
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }
        
    }
}
