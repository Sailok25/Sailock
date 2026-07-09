using System.Windows;
using Sailock.Services;

namespace Sailock
{
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ThemeService.ApplyTheme(true);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}