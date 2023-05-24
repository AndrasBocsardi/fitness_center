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
using Entry = fitness_terem.Model.Entry;

namespace fitness_terem.ViewModel;

[QueryProperty(nameof(Client), "Client")]
public partial class ClientDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    Client client;

    public ObservableCollection<ClientTicketWithGymName> ClientTickets { get; set; } = new();

    private ClientTicketWithGymName selectedTicket;
    public ClientTicketWithGymName SelectedTicket
    {
        get { return selectedTicket; }
        set { selectedTicket = value; OnPropertyChanged(nameof(SelectedTicket)); }
    }

    private bool isTicketSectionVisible;
    public bool IsTicketSectionVisible
    {
        get => isTicketSectionVisible;
        set => SetProperty(ref isTicketSectionVisible, value);
    }

    [RelayCommand]
    async void NavigateToAssignTicket()
    {
        await Shell.Current.GoToAsync(nameof(AssignTicketPage), true, new Dictionary<string, object>
        {
            {"Client", client }
        });
    }

    [RelayCommand]
    async void ShowTickets()
    {
        int clientId = client?.Client_id ?? 0; //Client?.Client_id ?? 0;
        List<ClientTicket> tickets = await App.FitnessRepo.GetClientTickets(clientId);
        foreach (ClientTicket ticket in tickets)
        {
            Gym gym = await App.FitnessRepo.GetGymName(ticket.Gym_id);
            ClientTicketWithGymName ticketWithGymName = new ClientTicketWithGymName
            {
                ClientTicket = ticket,
                GymName = gym.Name
            };
            ClientTickets.Add(ticketWithGymName);
        }
        IsTicketSectionVisible = true;
    }

    [RelayCommand]
    void SelectATicket(ClientTicketWithGymName ticket)
    {
        SelectedTicket = ticket;
    }


    [RelayCommand]

    async void AddEntry()
    {
        int entryCount = SelectedTicket.ClientTicket.Entry_count + 1;
        int ticketId = SelectedTicket.ClientTicket.Ticket_id;
        int clientTicketId = SelectedTicket.ClientTicket.Client_ticket_id;
        Debug.WriteLine(SelectedTicket.ClientTicket.Entry_count);
        int maxEntry = await App.FitnessRepo.GetMaxNrOfEntries(ticketId);
        int daysValid = await App.FitnessRepo.GetNrOfDaysValid(ticketId);
        DateTime purchaseDate = SelectedTicket.ClientTicket.Purchase_date;
        DateTime currentDate = DateTime.Now;
        if (entryCount > maxEntry && maxEntry > 0)
        {
            await App.FitnessRepo.UpdateValidity(clientTicketId);
            await Shell.Current.DisplayAlert("Invalid ticket", "You reached the max number of entries.", "OK");
        }
        else if (daysValid > 0 && currentDate > purchaseDate.AddDays(daysValid))
        {
            await App.FitnessRepo.UpdateValidity(clientTicketId);
            await Shell.Current.DisplayAlert("Invalid ticket", "You reached the max number of entries.", "OK");
        }
        else
        {
            Entry newEntry = new()
            {
                Client_id = SelectedTicket.ClientTicket.Client_id,
                Client_ticket_id = SelectedTicket.ClientTicket.Ticket_id,
                Date = DateTime.Now,
                Gym_id = SelectedTicket.ClientTicket.Gym_id
            };
            await App.FitnessRepo.InsertEntry(newEntry);

            await App.FitnessRepo.UpdateEntryCount(clientTicketId, entryCount);
            Debug.WriteLine(ticketId);
            Debug.WriteLine(SelectedTicket.ClientTicket.Entry_count);
            string message;
            if (maxEntry > 0)
            {
                int entriesLeft = maxEntry - entryCount;
                message = $"Entries left: {entriesLeft}";
            }
            else 
            {
                DateTime validUntil = purchaseDate.AddDays(daysValid);
                message = $"Valid until: {validUntil.ToShortDateString()}";
            }
            await Shell.Current.DisplayAlert("Successful admission", message, "OK");
        }

        await Shell.Current.GoToAsync(nameof(ClientDetails), true, new Dictionary<string, object>
        {
            {"Client", client }
        });
    }
    [RelayCommand]
    async void DeleteClient()
    {
        int clientId = client?.Client_id ?? 0;
        List<ClientTicket> tickets = await App.FitnessRepo.GetClientTickets(clientId);
        if (tickets != null && tickets.Count > 0)
        {
            await Shell.Current.DisplayAlert("Client can't be deleted", "Client has tickets", "OK");
            return;
        }
        else
        {
            await App.FitnessRepo.DeleteClient(clientId);
            await Shell.Current.DisplayAlert("Client deleted", "You will be redirected", "OK");
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }
    }

    [RelayCommand]
    async void DeleteTicket()
    {
        int ticketId = SelectedTicket.ClientTicket.Client_ticket_id;
        Debug.WriteLine(ticketId);
        bool isValid = SelectedTicket.ClientTicket.Is_valid;
        if (!isValid)
        {
            await App.FitnessRepo.DeleteTicket(ticketId);
            await Shell.Current.DisplayAlert("Ticket deleted", "", "OK");
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }
        else
        {
            await Shell.Current.DisplayAlert("Valid ticket can't be deleted", "", "OK");
        }

    }
   
}