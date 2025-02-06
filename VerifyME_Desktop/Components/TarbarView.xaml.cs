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

namespace VerifyME_Desktop.Components
{
    /// <summary>
    /// Interaction logic for TarbarView.xaml
    /// </summary>
    public partial class TarbarView : UserControl
    {
        public TarbarView()
        {
            InitializeComponent();
            DataContext = new ViewModels.TarbarViewModel(ServiceLocator.NavigationService);
        }
    }
}
