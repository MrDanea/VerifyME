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
using System.Windows.Media.Imaging;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Models;
using VerifyME_Desktop.Views;

namespace VerifyME_Desktop.ViewModels
{
    public class FNReconciliationViewModel : INotifyPCME
    {
        private readonly INavigationService _navigationService;
        public ObservableCollection<FNReconciliationModel> Items { get; set; }
        private BitmapImage _resizedImage;
        public BitmapImage ResizedImage { get => _resizedImage; set => SetProperty(ref _resizedImage, value); }
        public ICommand TestCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public FNReconciliationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Items = new ObservableCollection<FNReconciliationModel>();
            LoadDirectory(Memory.MemoryManage.Storage);
            ResizeImage(Path.Combine(Memory.MemoryManage.Images, "Screenshot 2024-09-20 000158.png"));
            TestCommand = new RelayCommand(ExecuteOpenCommand);
        }
        private void ExecuteOpenCommand(object parameter) 
        {
            string? content = parameter as string;
            MessageBox.Show(content);
        }
        private void ExecuteDeleteCommand(object parameter)
        {

        }
        public void ResizeImage(string imagePath)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            bitmap.Freeze();
            ResizedImage = bitmap;
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
