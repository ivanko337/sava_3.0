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
    public class ProjectTypeWindowViewModel : ViewModelBase
    {
        public ProjectType ProjectType { get; set; }

        public ICommand SaveTypeCommand { get; set; }

        public ProjectTypeWindowViewModel()
        {
            ProjectType = new ProjectType();
            SaveTypeCommand = new Command(CreateType, CanUpdateType);
        }

        public ProjectTypeWindowViewModel(ProjectType projType)
        {
            ProjectType = projType;
            SaveTypeCommand = new Command(UpdateType, CanUpdateType);
        }

        private void CreateType(object param)
        {
            if (string.IsNullOrEmpty(ProjectType.Name))
            {
                return;
            }

            using (var context = new DBContext())
            {
                DBService.AddNewEntity(param as Window, ProjectType, context, context.ProjectTypes);
            }
        }

        private void UpdateType(object param)
        {
            if (string.IsNullOrEmpty(ProjectType.Name))
            {
                return;
            }

            using (var context = new DBContext())
            {
                DBService.UpdateEntity(param as Window, ProjectType, context);
            }
        }

        private bool CanUpdateType(object param)
        {
            return ProjectType != null && !string.IsNullOrEmpty(ProjectType.Name);
        }
    }
}
