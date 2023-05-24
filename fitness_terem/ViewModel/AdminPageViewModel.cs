using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.Model;
using fitness_terem.View;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

public partial class AdminPageViewModel :ObservableObject
{
    [ObservableProperty]
    Client client;

    public ObservableCollection<Client> ClientList { get; set; } = new();

    private ObservableCollection<Client> filteredClientList;
    public ObservableCollection<Client> FilteredClientList
    {
        get => filteredClientList;
        set => SetProperty(ref filteredClientList, value);
    }
   
    string filterText;
    public string FilterText
    {
        get => filterText;
        set
        {
            SetProperty(ref filterText, value);
            FilterClients();
        }
    }

    void FilterClients()
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            FilteredClientList = ClientList;
        }
        else
        {
            string filter = FilterText.ToLowerInvariant();
            FilteredClientList = new ObservableCollection<Client>(ClientList.Where(client => client.Name.ToLowerInvariant().Contains(filter)));
        }
    }

    [RelayCommand]
    async void ListClients()
    {
        List<Client> clients = await App.FitnessRepo.GetAllClients();

        if(ClientList.Count != 0)
        {
            ClientList.Clear();
        }

        foreach(Client client in clients)
        {
            ClientList.Add(client);
        }
        FilterClients();
    }

    [RelayCommand]
    async void NavigateToAddClient()
    {
        await Shell.Current.GoToAsync(nameof(AddClient));
    }


    [RelayCommand]
    async void NavigateToClientDetails(Client client)
    {
        await Shell.Current.GoToAsync(nameof(ClientDetails), true, new Dictionary<string, object>
        {
            {"Client", client }
        });

    }



}
