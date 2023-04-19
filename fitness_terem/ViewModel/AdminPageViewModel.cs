using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.Model;
using fitness_terem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

public partial class AdminPageViewModel :ObservableObject
{
    public ObservableCollection<Client> ClientList { get; set; } = new();

    [ObservableProperty]
    string filterText;
    

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
