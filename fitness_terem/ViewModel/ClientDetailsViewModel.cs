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
public partial class ClientDetailsViewModel: ObservableObject
{
    [ObservableProperty]
    Client client;

    [RelayCommand]
    async void NavigateToAssignTicket()
    {
        await Shell.Current.GoToAsync(nameof(AssignTicketPage));
    }
}
