using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VerifyME_Desktop.Core
{
    // INavigationService.cs
    public interface INavigationService
    {
        void Navigate(UserControl view);
        void NavigateToDefault();
        void Reload(Type? viewType);
    }
    public class NavigationService : INavigationService
    {
        private readonly Border _mainContent;

        public NavigationService(Border mainContent)
        {
            _mainContent = mainContent;
            NavigateToDefault();
        }

        public void Navigate(UserControl view)
        {
            if (view != null)
            {
                _mainContent.Child = view;
            }
        }

        public void NavigateToDefault()
        {
            var welcomeText = new TextBlock()
            {
                Text = "Welcome to ME APP",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 25
            };
            _mainContent.Child = welcomeText;
        }
        public void Reload(Type? viewType)
        {
            if (viewType != null)
            {
                UserControl? viewInstance = Activator.CreateInstance(viewType) as UserControl;
                if (viewInstance != null)
                {
                    Navigate(viewInstance);
                }
                else
                {
                    MessageBox.Show("khong th khoi tao");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy lớp {viewName} trong namespace.");
            }
        }
    }
}
