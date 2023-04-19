using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

public partial class AssignTicketPageViewModel : ObservableObject
{
    [ObservableProperty]
    string ticketName;

    [ObservableProperty]
    int ticketPrice;

    [ObservableProperty]
    int nrOfDaysValid;

    [ObservableProperty]
    string nrOfEntryValid;

    [ObservableProperty]
    string startTimeOfDay;

    [ObservableProperty]
    string endTimeOfDay;

    /*[RelayCommand]
    async void AssignTicket()
    {
        
    }*/
}
