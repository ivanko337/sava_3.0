using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava3._0.ViewModel
{
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

        private Subject subject;
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;

                OnProperyChanged(nameof(Subject));
                OnProperyChanged(nameof(Employees));
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                using (var context = new DBContext())
                {
                    var set = context.Employees.Include("Position").AsNoTracking().AsQueryable();

                    if (SalaryLess > 0)
                    {
                        set = set.Where(e => e.Position.Salary < SalaryLess);
                    }
                    if (SalaryMore > 0)
                    {
                        set = set.Where(e => e.Position.Salary > SalaryMore);
                    }
                    if (Subject != null)
                    {
                        set = set.Where(e => e.Subject.Id == Subject.Id);
                    }

                    return new ObservableCollection<Employee>(set);
                }
            }
        }
    }
}
