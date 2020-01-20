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
    /// Interaction logic for ProjectTypeWindow.xaml
    /// </summary>
    public partial class ProjectTypeWindow : Window
    {
        public ProjectTypeWindow(ProjectType projType = null)
        {
            if (projType != null)
            {
                DataContext = new ProjectTypeWindowViewModel(projType);
            }
            else
            {
                DataContext = new ProjectTypeWindowViewModel();
            }

            InitializeComponent();
        }
    }
}
