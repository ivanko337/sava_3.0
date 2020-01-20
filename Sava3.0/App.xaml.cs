using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Sava3._0.View;

namespace Sava3._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var context = new DBContext())
            {
                ProjectWindow wnd = new ProjectWindow();
                wnd.ShowDialog();
            }
        }
    }
}
