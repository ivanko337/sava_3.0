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
                    var query = from e in context.Employees
                                join pe in Project.ProjectEmployees on e.Id equals pe.EmployeeId
                                select e;

                    return new ObservableCollection<Employee>(query);
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

                    MessageBox.Show(string.Format("Итоговая стоимость проекта: {0}", res));
                });
            }
        }

        public ICommand AddEmployeeCommand
        {
            get
            {
                return new Command((obj) =>
                {

                });
            }
        }

        public ICommand SaveProjectCommand { get; set; }

        public ProjectWindowViewModel()
        {
            Project = new Project();
            SaveProjectCommand = new Command(CreateProject, CanSaveProject);
        }

        private void CreateProject(object param)
        {
            using (var context = new DBContext())
            {
                context.Entry(Project.Customer).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.Platform).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Project.ProjectType).State = System.Data.Entity.EntityState.Modified;

                DBService.AddNewEntity(param as Window, Project, context, context.Projects);
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
