using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Sava3._0.Infrastructure;
using Sava3._0.Infrastructure.Commands;
using Sava3._0.View;

namespace Sava3._0.ViewModel
{
    public class EmployeeWindowViewModel : ViewModelBase
    {
        public Employee Employee { get; set; }

        public ICommand SaveEmployeeCommand { get; set; }

        public ObservableCollection<Position> Positions
        {
            get
            {
                using (var context = new DBContext())
                {
                    return new ObservableCollection<Position>(context.Positions);
                }
            }
        }

        public Position Position
        {
            get => Employee.Position;
            set
            {
                Employee.Position = value;

                OnProperyChanged(nameof(Position));
                OnProperyChanged(nameof(Employee));
            }
        }

        public ObservableCollection<Subject> Subjects
        {
            get
            {
                using (var context = new DBContext())
                {
                    return new ObservableCollection<Subject>(context.Subjects);
                }
            }
        }

        public Subject Subject
        {
            get => Employee.Subject;
            set
            {
                Employee.Subject = value;

                OnProperyChanged(nameof(Subject));
                OnProperyChanged(nameof(Employee));
            }
        }

        public ICommand CreatePositionCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    PositionWindow wnd = new PositionWindow();

                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Positions));
                    }
                });
            }
        }

        public ICommand CreateSubjectCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    SubjectWindow wnd = new SubjectWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Subjects));
                    }
                });
            }
        }

        public EmployeeWindowViewModel()
        {
            Employee = new Employee();
            SaveEmployeeCommand = new Command(CreateEmployee, CanSaveEmployee);
        }

        public EmployeeWindowViewModel(Employee employee)
        {
            Employee = employee;
            SaveEmployeeCommand = new Command(UpdateEmployee, CanSaveEmployee);
        }

        private void CreateEmployee(object param)
        {
            using (var context = new DBContext())
            {
                context.Entry(Employee.Subject).State = System.Data.Entity.EntityState.Modified;
                context.Entry(Employee.Position).State = System.Data.Entity.EntityState.Modified;

                DBService.AddNewEntity(param as Window, Employee, context, context.Employees);
            }
        }

        private void UpdateEmployee(object param)
        {
            using (var context = new DBContext())
            {
                DBService.UpdateEntity(param as Window, Employee, context);
            }
        }

        private bool CanSaveEmployee(object param)
        {
            Regex regex = new Regex(@"\d{7}\S{1}\d{3}\S{2}\d{1}");

            return Employee != null
                    && !string.IsNullOrEmpty(Employee.Address)
                    && !string.IsNullOrEmpty(Employee.Name)
                    && !string.IsNullOrEmpty(Employee.PassportNum)
                    && regex.IsMatch(Employee.PassportNum)
                    && Employee.Position != null
                    && Employee.Subject != null
                    && !string.IsNullOrEmpty(Employee.Surname);
        }
    }
}
