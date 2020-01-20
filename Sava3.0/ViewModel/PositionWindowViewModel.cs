using Sava3._0.Infrastructure;
using Sava3._0.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class PositionWindowViewModel : ViewModelBase
    {
        public Position Position { get; set; }

        public ICommand SavePositionCommand { get; set; }

        public PositionWindowViewModel()
        {
            Position = new Position();
            SavePositionCommand = new Command(CreatePosition, CanSavePosition);
        }

        public PositionWindowViewModel(Position position)
        {
            Position = position;
            SavePositionCommand = new Command(UpdatePosition, CanSavePosition);
        }

        private void CreatePosition(object param)
        {
            using (var context = new DBContext())
            {
                DBService.AddNewEntity(param as Window, Position, context, context.Positions);
            }
        }

        private void UpdatePosition(object param)
        {
            using (var context = new DBContext())
            {
                DBService.UpdateEntity(param as Window, Position, context);
            }
        }

        private bool CanSavePosition(object param)
        {
            return Position != null && !string.IsNullOrEmpty(Position.Name) && Position.Salary > 0;
        }
    }
}
