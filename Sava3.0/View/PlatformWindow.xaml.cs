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
using System.Windows.Shapes;
using Sava3._0.ViewModel;

namespace Sava3._0.View
{
    /// <summary>
    /// Interaction logic for PlatformWindow.xaml
    /// </summary>
    public partial class PlatformWindow : Window
    {
        public PlatformWindow(Platform platform = null)
        {
            if (platform != null)
            {
                this.DataContext = new PlatformWindowViewModel(platform);
            }
            else
            {
                this.DataContext = new PlatformWindowViewModel();
            }

            InitializeComponent();
        }
    }
}
