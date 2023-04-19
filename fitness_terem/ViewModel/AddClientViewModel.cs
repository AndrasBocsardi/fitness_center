using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

public partial class AddClientViewModel: ObservableObject
{
    [ObservableProperty]
    string name;

    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string address;

    [ObservableProperty]
    string phone_nr;



    [RelayCommand]
    async void AddClient()
    {
        if(await App.FitnessRepo.AddNewClient(Name, Email, Password, Address, Phone_nr) > 0)
        {
            await Shell.Current.DisplayAlert("New client was created", "You will be redirected", "OK");
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }

    }

}
