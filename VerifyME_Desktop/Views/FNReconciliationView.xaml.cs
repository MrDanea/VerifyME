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
            FNReconciliationViewModel fNReconciliationViewModel = new FNReconciliationViewModel(ServiceLocator.NavigationService);
            this.DataContext = fNReconciliationViewModel;
        }
        private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as FrameworkElement;

            if (clickedElement?.DataContext is Models.FNReconciliationModel item)
            {
                if (DataContext is FNReconciliationViewModel vm)
                {
                    vm.OpenCommand.Execute(item.Name);
                }
            }
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            if (key == Key.Delete) 
            {
                var clickedElement = e.OriginalSource as FrameworkElement;

                if (clickedElement?.DataContext is Models.FNReconciliationModel item)
                {
                    if (DataContext is FNReconciliationViewModel vm)
                    {
                        vm.DeleteCommand.Execute(item.Name);
                    }
                }
            }
        }
    }
}
