using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Models;
using VerifyME_Desktop.Views;
namespace VerifyME_Desktop.ViewModels
{
    public class FNReconciliationViewModel : INotifyPCME, IViewTypeResolver
    {
        private readonly INavigationService _navigationService;
        private BitmapImage _resizedImage;
        private string? _textContent;
        private bool _isImage;
        private bool _isText;
        private string? _textResult;
        private HashSet<string> _labelHashsetCache;
        private HashSet<string> _imageHashsetCache;
        private readonly IViewTypeResolver _resolver;
        public BitmapImage ResizedImage { get => _resizedImage; set => SetProperty(ref _resizedImage, value); }
        public ObservableCollection<FNReconciliationModel> Items { get; set; }
        public bool IsText { get => _isText; set { SetProperty(ref _isText, value); } }
        public bool IsImage { get => _isImage; set { SetProperty(ref _isImage, value); } }
        public string? TextResult { get => _textResult; set { SetProperty (ref _textResult, value); } }  
        public string? TextContent { get => _textContent; set { SetProperty(ref _textContent, value); } }
        public HashSet<string> LabelHashsetCache {
            get {
                if (_labelHashsetCache == null) { _labelHashsetCache = new HashSet<string>(); }
                return _labelHashsetCache; 
            }
            set => _labelHashsetCache = value;
        }
        public HashSet<string> ImageHashsetCache {
            get
            {
                if (_imageHashsetCache == null) { _imageHashsetCache = new HashSet<string>(); }
                return _imageHashsetCache;
            }
            set => _imageHashsetCache = value;
        }
        public enum FileType
        {
            Text,
            Image,
            Invalid
        }
        public ICommand OpenCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ReconcButtonCommand { get; private set; }   
        public FNReconciliationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _resolver = new ViewTypeResolver(this);
            Items = new ObservableCollection<FNReconciliationModel>();
            LoadDirectory(Memory.MemoryManage.Storage);
            foreach (var item in Items)
            {
                foreach(var children in item.Children)
                {
                    TryGetFileType(AddNameFiletoHashSet, children.Name);
                }
            }
            OpenCommand = new RelayCommand(ExecuteOpenCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            ReconcButtonCommand = new RelayCommand(ExecuteReconcButtonCommand);
        }
        private void ExecuteOpenCommand(object parameter) 
        {
            string? content = parameter as string;
            if (content == null) { return; }
            else if (GetFileType(content) == FileType.Text)
            {
                IsImage = false;
                IsText = true;
                if (Memory.MemoryManage.TryReadTextFromFile(Path.Combine(Memory.MemoryManage.Labels, content), out var text)) { TextContent = text; }
            }
            else if (GetFileType(content) == FileType.Image)
            {
                IsImage = true;
                IsText = false;
                ResizeImage(Path.Combine(Memory.MemoryManage.Images, content));
            }
            else {
                IsImage = false;
                IsText = false;
            }
        }
        private void ExecuteDeleteCommand(object parameter)
        {
            string? content = parameter as string;
            if (content == null) { MessageBox.Show("File not exist"); return; }
            else if (GetFileType(content) == FileType.Text)
            {
                if (Memory.MemoryManage.DeleteFile(Path.Combine(Memory.MemoryManage.Labels, content))) { 
                    _navigationService.Reload(_resolver.GetViewType()); 
                    MessageBox.Show("Deleted file"); 
                    return; 
                }
                MessageBox.Show("Cannot delete file");
            }
            else if (GetFileType(content) == FileType.Image)
            {
                if (Memory.MemoryManage.DeleteFile(Path.Combine(Memory.MemoryManage.Images, content))) {
                    _navigationService.Reload(_resolver.GetViewType());
                    MessageBox.Show("Deleted file"); 
                    return; 
                }
                MessageBox.Show("Cannot delete file");
            }
        }
        private void ExecuteReconcButtonCommand(object parameter) 
        {
            string cont = ""; 
            string filePath = Path.Combine(Memory.MemoryManage.ListofValidFileNames, "ListofValidFileNames.txt");
            foreach (var content in CompareFiles(LabelHashsetCache, ImageHashsetCache))
            {
                cont += $"{content}\n";
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, cont);
            TextResult = cont;
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
        private FileType GetFileType(string? fileName)
        {
            var textExtensions = new[] { ".txt" };
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            if (string.IsNullOrEmpty(fileName) || !fileName.Contains('.'))
            {
                return FileType.Invalid;
            }
            if (textExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                return FileType.Text;
            }
            else if (imageExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                return FileType.Image;
            }
            else
            {
                return FileType.Invalid;
            }
        }
        private bool TryGetFileType(Func<string, bool> isValidFile, string name)
        {
            return isValidFile(name);
        }
        private bool AddNameFiletoHashSet(string name) 
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (GetFileType(name) == FileType.Text) { LabelHashsetCache.Add(Path.GetFileNameWithoutExtension(name)) ; return true; }
            else if (GetFileType(name) == FileType.Image) { ImageHashsetCache.Add(Path.GetFileNameWithoutExtension(name)); return true; }
            return false;
        }
        private List<string> CompareFiles(HashSet<string> filesA, HashSet<string> filesB)
        {
            var matchedFiles = new List<string>();
            foreach (var fileB in filesB)
            {
                if (filesA.Contains(fileB)) 
                {
                    matchedFiles.Add(fileB); 
                }
            }
            return matchedFiles;
        }
        public Type GetViewType()
        {
            return _resolver.GetViewType();
        }
    }
}
