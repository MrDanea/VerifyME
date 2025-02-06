using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VerifyME_Desktop.Views;

namespace VerifyME_Desktop.ViewModels
{
    public class HomeViewModel
    {
        private readonly Core.INavigationService _navigationService;

        public HomeViewModel(Core.INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
