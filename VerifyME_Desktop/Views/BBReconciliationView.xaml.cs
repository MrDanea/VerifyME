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
    public partial class BBReconciliationView : UserControl
    {
        public BBReconciliationViewModel viewmodel { get; set; }
        public BBReconciliationView()
        {
            InitializeComponent();
            viewmodel = new ViewModels.BBReconciliationViewModel(ServiceLocator.NavigationService);
            DataContext = viewmodel;
        }


        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as FrameworkElement;

            if (DataContext is BBReconciliationViewModel vm)
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
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null)
            {
                // Thay đổi con trỏ chuột thành mũi tên 2 chiều khi di chuột vào
                rectangle.Cursor = Cursors.SizeAll;
            }
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null)
            {
                // Trở lại con trỏ mặc định khi chuột rời khỏi
                rectangle.Cursor = Cursors.Arrow;
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                // Kiểm tra vị trí chuột để thay đổi con trỏ tại các viền
                Point position = e.GetPosition(rectangle);
                double borderThickness = rectangle.StrokeThickness;

                bool isOnLeftOrRightEdge = position.X <= borderThickness || position.X >= rectangle.Width - borderThickness;
                bool isOnTopOrBottomEdge = position.Y <= borderThickness || position.Y >= rectangle.Height - borderThickness;

                if (isOnLeftOrRightEdge)
                {
                    rectangle.Cursor = Cursors.SizeWE; // Mũi tên ngang (resize trái/phải)
                }
                else if (isOnTopOrBottomEdge)
                {
                    rectangle.Cursor = Cursors.SizeNS; // Mũi tên dọc (resize trên/dưới)
                }
                else
                {
                    rectangle.Cursor = Cursors.Arrow; // Trở về con trỏ bình thường
                }
            }
        }
    }
}
