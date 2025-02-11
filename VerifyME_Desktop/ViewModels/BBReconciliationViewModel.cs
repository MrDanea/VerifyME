using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using VerifyME_Desktop.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VerifyME_Desktop.ViewModels
{
    public class BBReconciliationViewModel: INotifyPCME, IViewTypeResolver
    {
        private readonly INavigationService _navigationService;
        private readonly IViewTypeResolver _resolver;
        private ObservableCollection<string> _listofValidFileNames;
        private string[] _listFileName;
        private BitmapImage _image;
        private string _selectItem;
        private double _rectangleHeight;
        private double _rectangleWidth;
        private double _rectangleX;
        private double _rectangleY;
        private double _imageheigth;
        private double _imagewidth;
        public ObservableCollection<string> ListofValidFileNames { get => _listofValidFileNames; set { SetProperty(ref _listofValidFileNames, value); } }
        public BitmapImage Image { get => _image; set { SetProperty(ref _image, value); } }
        public string SelectItem { get => _selectItem; set { SetProperty(ref _selectItem, value); } }
        public double RectangleHeight { get => _rectangleHeight; set { SetProperty(ref _rectangleHeight, value); } }
        public double RectangleWidth { get => _rectangleWidth; set { SetProperty(ref _rectangleWidth, value); } }
        public double RectangleX { get => _rectangleX; set { SetProperty(ref _rectangleX, value); } }
        public double RectangleY { get => _rectangleY; set { SetProperty(ref _rectangleY, value); } }
        public double ImageHeight { get => _imageheigth; set { SetProperty(ref _imageheigth, value); } }
        public double ImageWidth { get => _imagewidth; set { SetProperty(ref _imagewidth, value); } }
        public string[] ListFileName { get => _listFileName; set => _listFileName = value; }
        public ICommand OpenCommand;
        public ICommand NextCommand;
        public ICommand BackCommand;
        public BBReconciliationViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            _resolver = new ViewTypeResolver(this);
            OpenCommand = new RelayCommand(ExecuteOpenCommand);
            _imageheigth = 0; _imagewidth = 0;
            var file = File.ReadAllLines(Path.Combine(Memory.MemoryManage.ListofValidFileNames, "ListofValidFileNames.txt"));
            this.ListofValidFileNames = new ObservableCollection<string>(file);
            this.ListFileName = ListofValidFileNames.ToArray<string>();
            NextCommand = new RelayCommand(ExecuteNextCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
        }
        private void ExecuteOpenCommand(object parameter) { Open(SelectItem); }
        private void Open(string name) {
            string imgpath = Path.Combine(Memory.MemoryManage.Images, $"{name}.png");
            string txtpath = Path.Combine(Memory.MemoryManage.Labels, $"{name}.txt");
            double[]? content = null;
            if (!File.Exists(imgpath))
            {
                imgpath = Path.Combine(Memory.MemoryManage.Images, $"{name}.jpg");
            }
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imgpath);
            bitmap.EndInit();
            bitmap.Freeze();
            Image = bitmap;
            if (Memory.IFileIO.GetContentLabels(txtpath, out content))
            {
                RectangleX = (content[1] - content[3] / 2) * bitmap.Width;
                RectangleY = (content[2] - content[4] / 2) * bitmap.Height;
                RectangleWidth = content[3] * bitmap.Width;
                RectangleHeight = content[4] * bitmap.Height;
            };
        }
        private void ExecuteNextCommand(object parameter) 
        {
            int currentIndex = Array.IndexOf(ListFileName, SelectItem);
            if (currentIndex == -1) { return; }
            string? next = (currentIndex < ListFileName.Length - 1) ? ListFileName[currentIndex + 1] : null;
            if(next != null) { Open(next); }
        }
        private void ExecuteBackCommand (object parameter)
        {

        }
        private void DrawRectangle(string name)
        {
            var writeableBitmap = new WriteableBitmap(new BitmapImage() 
            { 
                UriSource = new Uri(Path.Combine(Memory.MemoryManage.Images, $"{name}.png"))
            });

        }
        public Type GetViewType()
        {
            return _resolver.GetViewType();
        }
    }
}
