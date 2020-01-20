using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Include;

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
                    var set = from e in context.Employees
                              select new SelectedEmployee
                              {
                                  Id = e.Id,
                                  Name = e.Name,
                                  Surname = e.Surname,
                                  Salary = e.Position.Salary,
                                  Position = e.Position.Name,
                                  PositionId = e.Position.Id
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
    }
}
