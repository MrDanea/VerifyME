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
    public partial class FNReconciliationView : UserControl
    {
        public FNReconciliationView()
        {
            InitializeComponent();
            DataContext = new FNReconciliationViewModel(ServiceLocator.NavigationService);
        }
    }
}
