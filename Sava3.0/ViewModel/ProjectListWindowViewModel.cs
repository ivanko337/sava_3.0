using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava3._0.ViewModel
{
    public class SelectedProject
    {
        public int Id { get; set; }
        public string ProjName { get; set; }
        public string PlatformName { get; set; }
        public string TypeName { get; set; }
        public string CustomerName { get; set; }
        public string TaskDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class ProjectListWindowViewModel : ViewModelBase
    {
        public ObservableCollection<SelectedProject> Projects
        {
            get
            {
                using (var context = new DBContext())
                {
                    var query = from prj in context.Projects
                                select new SelectedProject
                                {

                                };

                    //return new ObservableCollection<SelectedProject>(context.Projects);
                }
            }
        }
    }
}
