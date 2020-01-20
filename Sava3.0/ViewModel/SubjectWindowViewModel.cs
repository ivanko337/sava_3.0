using Sava3._0.Infrastructure;
using Sava3._0.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class SubjectWindowViewModel : ViewModelBase
    {
        public Subject Subject { get; set; }

        public ICommand SaveSubjectCommand { get; set; }

        public SubjectWindowViewModel()
        {
            Subject = new Subject();
            SaveSubjectCommand = new Command(CreateSubject, CanSaveSubject);
        }

        public SubjectWindowViewModel(Subject subject)
        {
            Subject = subject;
            SaveSubjectCommand = new Command(UpdateSubject, CanSaveSubject);
        }

        private void CreateSubject(object param)
        {
            using (var context = new DBContext())
            {
                DBService.AddNewEntity(param as Window, Subject, context, context.Subjects);
            }
        }

        private void UpdateSubject(object param)
        {
            using (var context = new DBContext())
            {
                DBService.UpdateEntity(param as Window, Subject, context);
            }
        }

        private bool CanSaveSubject(object param)
        {
            return Subject != null && !string.IsNullOrEmpty(Subject.Name);
        }
    }
}
