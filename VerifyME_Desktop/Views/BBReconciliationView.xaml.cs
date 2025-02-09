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

namespace VerifyME_Desktop.Views
{
    /// <summary>
    /// Interaction logic for BBReconciliationView.xaml
    /// </summary>
    public partial class BBReconciliationView : UserControl
    {
        public BBReconciliationView()
        {
            InitializeComponent();
            DataContext = new ViewModels.BBReconciliationViewModel(ServiceLocator.NavigationService);
        }
    }
}
