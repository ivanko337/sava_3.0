using Sava3._0.Infrastructure.Commands;
using Sava3._0.Infrastructure.Services;
using Sava3._0.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class SelectedProject
    {
        public int Id { get; set; }
        public string ProjName { get; set; }
        public string PlatformName { get; set; }
        public string TypeName { get; set; }
        public string CustomerName { get; set; }
        public string TaskDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class ProjectListWindowViewModel : ViewModelBase
    {
        public ObservableCollection<SelectedProject> Projects
        {
            get
            {
                using (var context = new DBContext())
                {
                    var query = from prj in context.Projects
                                select new SelectedProject
                                {
                                    Id = prj.Id,
                                    CustomerName = prj.Customer.Name,
                                    EndDate = prj.EndDate.ToString(),
                                    PlatformName = prj.Platform.Name,
                                    ProjName = prj.Name,
                                    StartDate = prj.StartDate.ToString(),
                                    TaskDescription = prj.TaskDescription,
                                    TypeName = prj.ProjectType.Name
                                };
                    var gg = new ObservableCollection<SelectedProject>(query);
                    return gg;
                }
            }
        }

        public ICommand CreateReportCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    dialog.RestoreDirectory = true;
                    string path = "";

                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        path = dialog.FileName;
                    }
                    else
                    {
                        return;
                    }

                    ExcelService service = new ExcelService();

                    try
                    {
                        service.CreateProjectsReport(Projects, path);

                        System.Windows.MessageBox.Show("All done");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        public ICommand OpenEmployeesListCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    EmployeesListWindow wnd = new EmployeesListWindow();
                    wnd.ShowDialog();
                });
            }
        }

        public ICommand CreateProjectCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    ProjectWindow wnd = new ProjectWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Projects));
                    }
                });
            }
        }
    }
}
