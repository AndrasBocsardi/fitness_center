using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_terem.ViewModel;

[QueryProperty (nameof(Email), nameof(Email))]
public partial class UserPageViewModel : ObservableObject
{
    [ObservableProperty]
    string email;
}
