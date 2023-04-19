using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fitness_terem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

public partial class MainViewModel: ObservableObject
{
    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    [RelayCommand]
    async Task LogIn()
    {
        if( await App.FitnessRepo.CheckLogin(Email, Password))
        {
            
            if(await App.FitnessRepo.ChechIfAdmin(Email))
            {
                await Shell.Current.GoToAsync(nameof(AdminPage));
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(UserPage));
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Incorrerct email or password", "Incorrerct email or password", "OK");
        }
        

    }

}
