using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Models;
using VerifyME_Desktop.Views;

namespace VerifyME_Desktop.ViewModels
{
    public class FNReconciliationViewModel : INotifyPCME
    {
        private readonly INavigationService _navigationService;
        public ICommand ImportLabelsCommand { get; private set; }
        public ICommand ImportImagesCommand { get; private set; }
        public ObservableCollection<FNReconciliationModel> Items { get; set; }

        public FNReconciliationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImportLabelsCommand = new RelayCommand(ExecuteImportLabelsCommand);
            ImportImagesCommand = new RelayCommand(ExecuteImportImagesCommand);
            Items = new ObservableCollection<FNReconciliationModel>();
            LoadDirectory(Memory.MemoryManage.Storage);
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
        private void LoadDirectory(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);

                foreach (var directory in dirInfo.GetDirectories())
                {
                    var item = new FNReconciliationModel
                    {
                        Name = directory.Name,
                        FullPath = directory.FullName,
                        IsDirectory = true
                    };
                    Items.Add(item);
                    LoadSubDirectories(item);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    var item = new FNReconciliationModel
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        IsDirectory = false
                    };
                    Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Xử lý ngoại lệ nếu không có quyền truy cập
            }
        }
        private void LoadSubDirectories(FNReconciliationModel item)
        {
            try
            {
                var dirInfo = new DirectoryInfo(item.FullPath);

                foreach (var directory in dirInfo.GetDirectories())
                {
                    var subItem = new FNReconciliationModel
                    {
                        Name = directory.Name,
                        FullPath = directory.FullName,
                        IsDirectory = true
                    };
                    item.Children.Add(subItem);
                    LoadSubDirectories(subItem);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    var subItem = new FNReconciliationModel
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        IsDirectory = false
                    };
                    item.Children.Add(subItem);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Xử lý ngoại lệ nếu không có quyền truy cập
            }
        }
    }
}
