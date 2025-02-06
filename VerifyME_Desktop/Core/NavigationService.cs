using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
