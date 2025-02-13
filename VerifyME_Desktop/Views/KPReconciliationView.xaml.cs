using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.ViewModels;

namespace VerifyME_Desktop.Views
{
    /// <summary>
    /// Interaction logic for KPReconciliationView.xaml
    /// </summary>
    public partial class KPReconciliationView : UserControl
    {
        public ViewModels.KPReconciliationViewModel viewmodel { get; set; }
        public KPReconciliationView()
        {
            InitializeComponent();
            viewmodel = new ViewModels.KPReconciliationViewModel(ServiceLocator.NavigationService);
            DataContext = viewmodel;
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as FrameworkElement;

            if (DataContext is KPReconciliationViewModel vm)
            {
                vm.OpenCommand.Execute(null);
            }
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var image = sender as Image;
            if (image != null && viewmodel != null)
            {
                var width = image.ActualWidth;
                var height = image.ActualHeight;

                viewmodel.ImageWidth = width;
                viewmodel.ImageHeight = height;
            }
        }
    }
}
