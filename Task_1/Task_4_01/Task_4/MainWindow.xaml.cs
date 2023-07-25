using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Task_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            var result = string.Empty;
            richTextBox.Text = string.Empty;
            result = await DownloadStringAsync();
            richTextBox.Text = result;
        }

        private void btnWarning_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Проверка", "Проверка", MessageBoxButton.OK);
        }

        private async Task<string> DownloadStringAsync()
        {
            return await Task.Run(async () =>
            {
                Thread.Sleep(5000);
                using (var webClient = new WebClient())
                {
                    return await webClient.DownloadStringTaskAsync("http://government.ru");
                };
            });
        }
    }
}
