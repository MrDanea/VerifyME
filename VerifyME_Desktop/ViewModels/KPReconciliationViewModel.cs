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
using System.Windows.Media;

namespace VerifyME_Desktop.ViewModels
{
    public class KPReconciliationViewModel : INotifyPCME, IViewTypeResolver
    {
        private readonly INavigationService _navigationService;
        private readonly IViewTypeResolver _resolver;
        private ObservableCollection<string> _listofValidFileNames;
        private string[] _listFileName;
        private BitmapImage _image;
        private string _selectItem;
        private double _imageheigth;
        private double _imagewidth;
        public ObservableCollection<string> ListofValidFileNames { get => _listofValidFileNames; set { SetProperty(ref _listofValidFileNames, value); } }
        public BitmapImage Image { get => _image; set { SetProperty(ref _image, value); } }
        public string SelectItem { get => _selectItem; set { SetProperty(ref _selectItem, value); } }
        public ObservableCollection<Circle> Circles { get; set; }
        public double ImageHeight { get => _imageheigth; set { SetProperty(ref _imageheigth, value); } }
        public double ImageWidth { get => _imagewidth; set { SetProperty(ref _imagewidth, value); } }
        public string[] ListFileName { get => _listFileName; set => _listFileName = value; }
        public ICommand OpenCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public KPReconciliationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _resolver = new ViewTypeResolver(this);
            OpenCommand = new RelayCommand(ExecuteOpenCommand);
            NextCommand = new RelayCommand(ExecuteNextCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            _imageheigth = 0; _imagewidth = 0;
            var file = File.ReadAllLines(Path.Combine(Memory.MemoryManage.ListofValidFileNames, "ListofValidFileNames.txt"));
            this.ListofValidFileNames = [.. file];
            this.ListFileName = ListofValidFileNames.ToArray<string>();
            Circles = [];
            SelectItem = ListFileName[0];
            Open(SelectItem);
        }
        private void ExecuteOpenCommand(object parameter) { Open(SelectItem); }
        private void Open(string name)
        {
            Circles.Clear();
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
                if(content == null) { return; }
                int i = 5;
                while (i < content.Length)
                {
                    Circles.Add(new Circle
                    {
                        X = content[i] * bitmap.Width,
                        Y = content[i + 1] * bitmap.Height,
                        Radius = 55,
                        Color = new SolidColorBrush(Colors.Red)
                    });
                    i+=3;
                }
            }
        }
        private void ExecuteNextCommand(object parameter)
        {
            int currentIndex = Array.IndexOf(ListFileName, SelectItem);
            if (currentIndex == -1) { return; }
            string? next = (currentIndex < ListFileName.Length - 1) ? ListFileName[currentIndex + 1] : null;
            if (next != null) { Open(next); SelectItem = next; }
        }
        private void ExecuteBackCommand(object parameter)
        {
            int currentIndex = Array.IndexOf(ListFileName, SelectItem);
            if (currentIndex == -1) { return; }
            string? previous = (currentIndex > 0) ? ListFileName[currentIndex - 1] : null;
            if (previous != null) { Open(previous); SelectItem = previous; }
        }
        public Type GetViewType()
        {
            return _resolver.GetViewType();
        }
    }

    public class Circle
    {
        private SolidColorBrush? _color;
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public SolidColorBrush Color 
        { 
            get => _color ??= new SolidColorBrush(Colors.Red);
            set => _color = value;
        }
    }
}
