using System.Configuration;
using System.Data;
using System.Windows;
using VerifyME_Desktop.Core;
using VerifyME_Desktop.Memory;

namespace VerifyME_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Memory.MemoryManage.Create();
        }
    }
}
