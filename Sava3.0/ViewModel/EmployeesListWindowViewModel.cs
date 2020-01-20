﻿using Sava3._0.Infrastructure.Commands;
using Sava3._0.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sava3._0.View;

namespace Sava3._0.ViewModel
{
    public class EmployeesListWindowViewModel : ViewModelBase
    {
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
                                  Address = e.Address,
                                  Project = context.ProjectEmployees.FirstOrDefault(pe => pe.Employee.Id == e.Id)  == null ? "" : context.ProjectEmployees.FirstOrDefault(pe => pe.Employee.Id == e.Id).Project.Name
                              };
                    var res = new ObservableCollection<SelectedEmployee>(set);
                    return res;
                }
            }
        }

        public ICommand CreateReportCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    File.Create("%HOMEPATH%\\EmployeesReport.xlsx");

                    ExcelService service = new ExcelService();

                    try
                    {
                        service.CreateEmployeesReport(Employees, "%HOMEPATH%\\EmployeesReport.xlsx");

                        System.Windows.MessageBox.Show("All done");
                        System.Windows.MessageBox.Show("Report save in home directory");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        public ICommand CreateEmployeeCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    EmployeeWindow wnd = new EmployeeWindow();
                    if (wnd.ShowDialog().Value)
                    {
                        OnProperyChanged(nameof(Employees));
                    }
                });
            }
        }
    }
}
