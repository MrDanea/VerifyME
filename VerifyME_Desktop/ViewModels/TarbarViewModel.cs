using Microsoft.Win32;
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
        public ICommand ImportLabelsCommand { get; private set; }
        public ICommand ImportImagesCommand { get; private set; }

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
            ImportLabelsCommand = new RelayCommand(ExecuteImportLabelsCommand);
            ImportImagesCommand = new RelayCommand(ExecuteImportImagesCommand);
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
        private void ExecuteImportLabelsCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Chọn file để import",
                Filter = $"Text Files (*.txt)|*.txt",
                Multiselect = true
            };
            SaveFile(openFileDialog, Memory.MemoryManage.Labels);
        }
        private void ExecuteImportImagesCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Chọn file để import",
                Filter = $"Image Files (*.png;*.jpg)|*.png;*.jpg",
                Multiselect = true
            };
            SaveFile(openFileDialog, Memory.MemoryManage.Images);
        }
        private void SaveFile(OpenFileDialog openFileDialog, string destinationFolder)
        {
            if (openFileDialog.ShowDialog() == true && destinationFolder != null)
            {
                foreach (string selectedFilePath in openFileDialog.FileNames)
                {
                    try
                    {
                        string fileName = System.IO.Path.GetFileName(selectedFilePath);
                        string destinationFilePath = System.IO.Path.Combine(destinationFolder, fileName);
                        System.IO.File.Copy(selectedFilePath, destinationFilePath, overwrite: true);
                        Console.WriteLine($"Đã sao chép file: {fileName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi sao chép file: {ex.Message}");
                    }
                }
                MessageBox.Show("Đã sao chép tất cả file thành công!");
            }
        }
    }
}
