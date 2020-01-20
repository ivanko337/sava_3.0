using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Sava3._0.Infrastructure;
using Sava3._0.Infrastructure.Commands;
using Sava3._0.View;

namespace Sava3._0.ViewModel
{
    public class ProjectWindowViewModel : ViewModelBase
    {
        public Project Project { get; set; }

        public ObservableCollection<Platform> Platforms
        {
            get
            {
                using (var context = new DBContext())
                {
                    return new ObservableCollection<Platform>(context.Platforms.AsNoTracking());
                }
            }
        }

        public ObservableCollection<ProjectType> ProjectTypes
        {
            get
            {
                using (var context = new DBContext())
                {
                    return new ObservableCollection<ProjectType>(context.ProjectTypes.AsNoTracking());
                }
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                using (var context = new DBContext())
                {
                    return new ObservableCollection<Customer>(context.Customers.AsNoTracking());
                }
            }
        }

        public ObservableCollection<Employee> ProjectEmployees
        {
            get
            {
                using (var context = new DBContext())
                {
                    var query = from pe in Project.ProjectEmployees
                                where pe.Employee.Position.Salary == pe.Employee.Position.Salary
                                select pe.Employee;

                    var gg = new ObservableCollection<Employee>(query);
                    return gg;
                }
            }
        }

        public ICommand CalcCostCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    if (Project.StartDate == default(DateTime)
                        || Project.EndDate == default(DateTime))
                    {
                        MessageBox.Show("Корректно заполните даты");
                        return;
                    }

                    int projLen = (Project.EndDate - Project.StartDate).Days;

                    decimal res = 0;

                    foreach (var projEmployee in Project.ProjectEmployees)
                    {
                        Employee employee = projEmployee.Employee;
                        decimal daySalary = employee.Position.Salary / 30;
                        res += projLen * daySalary;
                    }

                    var factor = Convert.ToDecimal(Math.Pow(10, 2));
                    res = Math.Ceiling(res * factor) / factor;

                    MessageBox.Show(string.Format("Итоговая стоимость проекта: {0}BYN", res));
                });
            }
        }

        public ICommand AddEmployeeCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    SelectEmplyeeWindow wnd = new SelectEmplyeeWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        ProjectEmployee pe = new ProjectEmployee();

                        //context.Entry(pe.Employee).State = System.Data.Entity.EntityState.Modified;
                        //context.Employees.Attach(wnd.SelectedEmployee);

                        pe.Employee = wnd.SelectedEmployee;

                        Project.ProjectEmployees.Add(pe);
                        //context.Entry(pe).State = System.Data.Entity.EntityState.Added;


                        OnProperyChanged(nameof(Project));
                        OnProperyChanged(nameof(ProjectEmployees));
                    }
                });
            }
        }

        public ICommand RemoveEmployeeCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    if (obj == null)
                    {
                        return;
                    }

                    var pe = Project.ProjectEmployees.FirstOrDefault(p => p.Employee.Id == (obj as Employee).Id);
                    Project.ProjectEmployees.Remove(pe);

                    OnProperyChanged(nameof(Project));
                    OnProperyChanged(nameof(ProjectEmployees));
                });
            }
        }

        public ICommand SaveProjectCommand { get; set; }

        public ICommand CreatePlatformCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    PlatformWindow wnd = new PlatformWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Platforms));
                    }
                });
            }
        }

        public ICommand CreateCustomerCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    CustomerWindow wnd = new CustomerWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Customers));
                    }
                });
            }
        }

        public ICommand CreateProjectTypeCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    ProjectTypeWindow wnd = new ProjectTypeWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(ProjectTypes));
                    }
                });
            }
        }

        public ProjectWindowViewModel()
        {
            Project = new Project();
            SaveProjectCommand = new Command(CreateProject, CanSaveProject);
        }

        public ProjectWindowViewModel(Project project)
        {
            Project = project;
            SaveProjectCommand = new Command(UpdateProject, CanSaveProject);
        }

        private void CreateProject(object param)
        {
            using (var context = new DBContext())
            {
                context.Entry(Project.Customer).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.Platform).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.ProjectType).State = System.Data.Entity.EntityState.Modified;

                foreach (var item in Project.ProjectEmployees)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    context.Entry(item.Employee).State = System.Data.Entity.EntityState.Modified;
                }

                DBService.AddNewEntity(param as Window, Project, context, context.Projects);

                //context.Employees.Remove(context.Employees.OrderByDescending(e => e.Id).FirstOrDefault());
            }
        }

        private void UpdateProject(object param)
        {
            using (var context = new DBContext())
            {
                context.Entry(Project.Customer).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.Platform).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.ProjectType).State = System.Data.Entity.EntityState.Modified;

                DBService.UpdateEntity(param as Window, Project, context);
            }
        }

        private bool CanSaveProject(object param)
        {
            return Project != null
                && Project.Customer != null
                && !string.IsNullOrEmpty(Project.Name)
                && Project.Platform != null
                && Project.ProjectType != null
                && !string.IsNullOrEmpty(Project.TaskDescription);
        }
    }
}
