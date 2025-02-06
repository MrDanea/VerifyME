using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Views;

namespace VerifyME_Desktop.ViewModels
{
    public class FNReconciliationViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand ImportLabelsCommand { get; private set; }
        public ICommand ImportImagesCommand { get; private set; }

        public FNReconciliationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImportLabelsCommand = new RelayCommand(ExecuteImportLabelsCommand);
            ImportImagesCommand = new RelayCommand(ExecuteImportImagesCommand);

        }
        private void ExecuteImportLabelsCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file để import",
                Filter = "Text Files (*.txt)|*.txt",
                Multiselect = true 
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string destinationFolder = Memory.MemoryManage.Labels; 
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
        private void ExecuteImportImagesCommand(object parameter) 
        {

        }
        private void ProcessFile(string filePath)
        {
            try
            {
                // Ví dụ xử lý file: đọc nội dung file text
                if (Path.GetExtension(filePath).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    string fileContent = File.ReadAllText(filePath);
                    MessageBox.Show($"Nội dung file:\n{fileContent}", "Nội dung File", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không hỗ trợ loại file này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý file: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
