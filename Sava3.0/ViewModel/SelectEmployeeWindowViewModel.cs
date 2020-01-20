using Sava3._0.Infrastructure.Commands;
using Sava3._0.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class SelectedEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Salary { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public string PassportNum { get; set; }
        public string SubjectName { get; set; }
        public string Address { get; set; }
        public string Project { get; set; }
    }

    public class SelectEmployeeWindowViewModel : ViewModelBase
    {
        private decimal salaryMore;
        public decimal SalaryMore
        {
            get => salaryMore;
            set
            {
                salaryMore = value;

                OnProperyChanged(nameof(SalaryMore));
                OnProperyChanged(nameof(Employees));
            }
        }

        private decimal salaryLess;
        public decimal SalaryLess
        {
            get => salaryLess;
            set
            {
                salaryLess = value;

                OnProperyChanged(nameof(SalaryLess));
                OnProperyChanged(nameof(Employees));
            }
        }

        public ObservableCollection<SelectedEmployee> Employees
        {
            get
            {
                using (var context = new DBContext())
                {
                    var set = from e in context.Employees.AsNoTracking()
                              select new SelectedEmployee
                              {
                                  Id = e.Id,
                                  Name = e.Name,
                                  Surname = e.Surname,
                                  Salary = e.Position.Salary,
                                  Position = e.Position.Name,
                                  PositionId = e.Position.Id,
                                  PassportNum = e.PassportNum,
                                  SubjectName = e.Subject.Name,
                                  Address = e.Address
                              };

                    if (SalaryLess > 0)
                    {
                        set = set.Where(e => e.Salary < SalaryLess);
                    }
                    if (SalaryMore > 0)
                    {
                        set = set.Where(e => e.Salary > SalaryMore);
                    }

                    return new ObservableCollection<SelectedEmployee>(set);
                }
            }
        }

        public ICommand SelectEmployeeCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    SelectEmplyeeWindow wnd = obj as SelectEmplyeeWindow;
                    ListView lstView = wnd.listView;
                    SelectedEmployee selectedEmployee = lstView.SelectedItem as SelectedEmployee;

                    using (var context = new DBContext())
                    {
                        wnd.SelectedEmployee = context.Employees.AsNoTracking().FirstOrDefault(e => e.Id == selectedEmployee.Id);
                    }

                    wnd.DialogResult = wnd.SelectedEmployee != null;
                });
            }
        }
    }
}
