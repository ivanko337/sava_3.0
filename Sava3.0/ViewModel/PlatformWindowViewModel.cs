using Sava3._0.Infrastructure;
using Sava3._0.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class PlatformWindowViewModel : ViewModelBase
    {
        public Platform Platform { get; set; }

        public ICommand SavePlatformCommand { get; set; }

        public PlatformWindowViewModel()
        {
            Platform = new Platform();
            SavePlatformCommand = new Command(CreatePlatform, CanSavePlatform);
        }

        public PlatformWindowViewModel(Platform platform)
        {
            Platform = platform;
            SavePlatformCommand = new Command(UpdatePlatform, CanSavePlatform);
        }

        private void CreatePlatform(object param)
        {
            if (string.IsNullOrEmpty(Platform.Name))
            {
                return;
            }

            using (var context = new DBContext())
            {
                DBService.AddNewEntity(param as Window, Platform, context, context.Platforms);
            }
        }

        private void UpdatePlatform(object param)
        {
            if (string.IsNullOrEmpty(Platform.Name))
            {
                return;
            }

            using (var context = new DBContext())
            {
                DBService.UpdateEntity(param as Window, Platform, context);
            }
        }

        private bool CanSavePlatform(object param)
        {
            return Platform != null && !string.IsNullOrEmpty(Platform.Name);
        }
    }
}
