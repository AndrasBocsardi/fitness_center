
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.Model;
using fitness_terem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

[QueryProperty(nameof(Client), "Client")]
public partial class AssignTicketPageViewModel : ObservableObject
{
    public ObservableCollection<TicketType> Tickets { get; set; } = new();
    private bool isEntryVisible;
    public bool IsEntryVisible
    {
        get { return isEntryVisible; }
        set { SetProperty(ref isEntryVisible, value); }
    }

    private bool isDaysVisible;
    public bool IsDaysVisible
    {
        get { return isDaysVisible; }
        set { SetProperty(ref isDaysVisible, value); }
    }

    private bool isEntriesVisible;
    public bool IsEntriesVisible
    {
        get { return isEntriesVisible; }
        set { SetProperty(ref isEntriesVisible, value); }
    }
    [ObservableProperty]
    string ticketName;

    [ObservableProperty]
    Client client;

   
    private TicketType selectedTicket;
    public TicketType SelectedTicket
    {
        get { return selectedTicket; }
        set { SetProperty(ref selectedTicket, value); 
            if(selectedTicket != null)
            {
                UpdateEntryFields();
                IsEntryVisible = true;
            }
            else
            {
                IsEntryVisible= false;
            }
        }
    }
    public string FormattedTicketPrice
    {
        get { return TicketPrice.ToString(); }
        set
        {
            if (int.TryParse(value, out int result))
            {
                TicketPrice = result;
            }
        }
    }
    private int _ticketPrice;
    public int TicketPrice
    {
        get { return _ticketPrice; }
        set
        {
            _ticketPrice = value;
            OnPropertyChanged(nameof(TicketPrice));
        }
    }

    private int nrOfDaysValid;
    public int NrOfDaysValid
    {
        get { return nrOfDaysValid; }
        set
        {
            nrOfDaysValid = value;
            OnPropertyChanged(nameof(NrOfDaysValid));
        }
    }

    private int nrOfEntryValid;
    public int NrOfEntryValid
    {
        get { return nrOfEntryValid; }
        set
        {
            nrOfEntryValid = value;
            OnPropertyChanged(nameof(NrOfEntryValid));
        }
    }
    private string startTimeOfDay;
    public string StartTimeOfDay
    {
        get { return startTimeOfDay; }
        set
        {
            startTimeOfDay = value;
            OnPropertyChanged(nameof(StartTimeOfDay));
        }
    
    }
    private string endTimeOfDay;
    public string EndTimeOfDay
    {
        get { return endTimeOfDay; }
        set
        {
            endTimeOfDay = value;
            OnPropertyChanged(nameof(EndTimeOfDay));
        }

    }
   
    public AssignTicketPageViewModel() {
        LoadTickets();
        IsEntryVisible = false;
    }
   
    [RelayCommand]
    async void AssignTicket()
    {
        if (SelectedTicket != null)
        {

            if (await App.FitnessRepo.AssignTicketToClient(Client.Client_id, SelectedTicket.Ticket_id, SelectedTicket.Price) > 0)
            {

                await Shell.Current.DisplayAlert("The ticket was assigned", "You will be redirected", "OK");
                await Shell.Current.GoToAsync(nameof(AdminPage));
            }
        }
         
    }
    async void LoadTickets()
    {
        List<TicketType> tickets = await App.FitnessRepo.GetAllTycketTypes();
        foreach (TicketType ticket in tickets)
        {
            Tickets.Add(ticket);
        }
    }
    private void UpdateEntryFields()
    {

        if (selectedTicket.Nr_of_days_valid > 0)
        {
            
            IsDaysVisible = true;
            IsEntriesVisible = false;
        }
        else if (selectedTicket.Nr_of_entry_valid > 0)
        {
            IsDaysVisible = false;
            IsEntriesVisible = true;
        }
        else
        {
            IsDaysVisible = false;
            IsEntriesVisible = false;
        }
        TicketPrice = selectedTicket.Price;
        NrOfDaysValid = selectedTicket.Nr_of_days_valid;
        NrOfEntryValid = selectedTicket.Nr_of_entry_valid;
        StartTimeOfDay = selectedTicket.Start_time_of_day;
        EndTimeOfDay = selectedTicket.End_time_of_day;
        
    }
    private bool isSelectTicketVisible;
    public bool IsSelectTicketVisible
    {
        get => isSelectTicketVisible;
        set => SetProperty(ref isSelectTicketVisible, value);
    }

    private bool isEnterNameVisible;
    public bool IsEnterNameVisible
    {
        get => isEnterNameVisible;
        set => SetProperty(ref isEnterNameVisible, value);
    }

    [RelayCommand]
    void AddNewTicketType()
    {
        IsSelectTicketVisible = false;
        IsEnterNameVisible = true;
    }

    [RelayCommand]
    void SelectTicket()
    {
        IsSelectTicketVisible = true;
        IsEnterNameVisible = false;
    }
    [RelayCommand]
    async void AddNewTicket()
    {
        TicketType ticketType = await App.FitnessRepo.AddNewTicket(TicketName, TicketPrice, NrOfDaysValid, NrOfEntryValid, StartTimeOfDay, EndTimeOfDay);

        if (await App.FitnessRepo.AssignTicketToClient(Client.Client_id, ticketType.Ticket_id, ticketType.Price) > 0)
        {
            await Shell.Current.DisplayAlert("The ticket was created", "You will be redirected", "OK");
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }

    }
}

