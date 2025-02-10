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

namespace VerifyME_Desktop.ViewModels
{
    public class BBReconciliationViewModel: INotifyPCME, IViewTypeResolver
    {
        private readonly INavigationService _navigationService;
        private readonly IViewTypeResolver _resolver;
        private ObservableCollection<string> _listofValidFileNames;
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
        public ICommand OpenCommand;
        public BBReconciliationViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            _resolver = new ViewTypeResolver(this);
            OpenCommand = new RelayCommand(ExecuteOpenCommand);

            this.ListofValidFileNames = new ObservableCollection<string>();
            var file = File.ReadAllLines(Path.Combine(Memory.MemoryManage.ListofValidFileNames, "ListofValidFileNames.txt"));
            foreach (var item in file)
            {
                this.ListofValidFileNames.Add(item);
            }
        }
        private void ExecuteOpenCommand(object parameter)
        {
            string path = Path.Combine(Memory.MemoryManage.Images, $"{SelectItem}.png");
            if (!File.Exists(path))
            {
                path = Path.Combine(Memory.MemoryManage.Images, $"{SelectItem}.jpg");
            }
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            bitmap.Freeze();
            Image = bitmap;
            ImageHeight = bitmap.Height * 0.1;
            ImageWidth = bitmap.Width * 0.1;
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
