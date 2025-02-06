using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Views;

namespace VerifyME_Desktop.ViewModels
{
    public class TarbarViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand NewButtonCommand { get; private set; }
        public ICommand OpenButtonCommand { get; private set; }
        public ICommand MemoryButtonCommand { get; private set; }
        public ICommand ManageButtonCommand { get; private set; }
        public ICommand FNDButtonCommand { get; private set; }
        public ICommand BBRButtonCommand { get; private set; }
        public ICommand KPRButtonCommand { get; private set; }

        public TarbarViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            NewButtonCommand = new RelayCommand(ExecuteNewButtonCommand);
            OpenButtonCommand = new RelayCommand(ExecuteOpenButtonCommand);
            MemoryButtonCommand = new RelayCommand(ExecuteMemoryButtonCommand);
            ManageButtonCommand = new RelayCommand(ExecuteManageButtonCommand);
            FNDButtonCommand = new RelayCommand(ExecuteFNDButtonCommand);
            BBRButtonCommand = new RelayCommand(ExecuteBBRButtonCommand);
            KPRButtonCommand = new RelayCommand(ExecuteKPRButtonCommand);
        }
        private void ExecuteNewButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
        private void ExecuteOpenButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
        private void ExecuteMemoryButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
        private void ExecuteManageButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
        private void ExecuteFNDButtonCommand(object parameter)
        {
            _navigationService.Navigate(new FNReconciliationView());
        }
        private void ExecuteBBRButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
        private void ExecuteKPRButtonCommand(object parameter)
        {
            MessageBox.Show("Dang trong qua trinh phat trien");
        }
    }
}
