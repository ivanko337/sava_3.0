using Microsoft.Office.Interop.Excel;
using Sava3._0.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava3._0.Infrastructure.Services
{
    public class ExcelService
    {
        public void CreateProjectsReport(IEnumerable<SelectedProject> projects, string path)
        {
            if (string.IsNullOrEmpty(path) && File.Exists(path))
            {
                System.Windows.MessageBox.Show("Неверный путь");
                return;
            }

            var excel = new Application();

            excel.Visible = false;
            excel.DisplayAlerts = false;

            _Workbook workBook = excel.Workbooks.Open(path);
            _Worksheet workSheet = (_Worksheet)workBook.ActiveSheet;
            workSheet.Name = "Projects";

            workSheet.Cells.ClearContents();

            workSheet.Cells[1, 1] = "Id";
            workSheet.Cells[1, 2] = "ProjName";
            workSheet.Cells[1, 3] = "PlatformName";
            workSheet.Cells[1, 4] = "TypeName";
            workSheet.Cells[1, 5] = "CustomerName";
            workSheet.Cells[1, 6] = "TaskDescription";
            workSheet.Cells[1, 7] = "StartDate";
            workSheet.Cells[1, 8] = "EndDate";

            int index = 2;
            foreach (var proj in projects)
            {
                workSheet.Cells[index, 1] = proj.Id.ToString();
                workSheet.Cells[index, 2] = proj.ProjName;
                workSheet.Cells[index, 3] = proj.PlatformName;
                workSheet.Cells[index, 4] = proj.TypeName;
                workSheet.Cells[index, 5] = proj.CustomerName;
                workSheet.Cells[index, 6] = proj.TaskDescription;
                workSheet.Cells[index, 7] = proj.StartDate;
                workSheet.Cells[index, 8] = proj.EndDate;

                ++index;
            }

            workSheet.Columns.AutoFit();

            workBook.Save();

            workBook?.Close();
        }

        public void CreateEmployeesReport(IEnumerable<SelectedEmployee> employees, string path)
        {
            if (string.IsNullOrEmpty(path) && File.Exists(path))
            {
                System.Windows.MessageBox.Show("Неверный путь");
                return;
            }

            var excel = new Application();

            excel.Visible = false;
            excel.DisplayAlerts = false;

            _Workbook workBook = excel.Workbooks.Open(path);
            _Worksheet workSheet = (_Worksheet)workBook.ActiveSheet;
            workSheet.Name = "Employees";

            workSheet.Cells.ClearContents();

            workSheet.Cells[1, 1] = "Id";
            workSheet.Cells[1, 2] = "Name";
            workSheet.Cells[1, 3] = "Surname";
            workSheet.Cells[1, 4] = "Salary";
            workSheet.Cells[1, 5] = "Position";
            workSheet.Cells[1, 6] = "PassportNum";
            workSheet.Cells[1, 7] = "SubjectName";
            workSheet.Cells[1, 8] = "Address";

            int index = 2;
            foreach (var employee in employees)
            {
                workSheet.Cells[index, 1] = employee.id.ToString();
                workSheet.Cells[index, 2] = employee.name;
                workSheet.Cells[index, 3] = employee.surname;
                workSheet.Cells[index, 4] = employee.positionName;
                workSheet.Cells[index, 5] = employee.salary.ToString();
                workSheet.Cells[index, 6] = employee.address;
                workSheet.Cells[index, 7] = employee.proj_name;
                workSheet.Cells[index, 8] = employee.group_name;

                ++index;
            }

            workSheet.Columns.AutoFit();

            workBook.Save();

            workBook?.Close();
        }
    }
}
