using Sava3._0.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sava3._0.ViewModel
{
    public class CustomerWindowViewModel : ViewModelBase
    {
        public Customer Customer { get; set; }

        public ICommand SaveCustomerCommand
        {
            get
            {
                return new Command(CreateCustomer, CanCreateCustomer);
            }
        }

        private void CreateCustomer(object parameter)
        {

        }

        private bool CanCreateCustomer(object parameter)
        {
            return Customer != null
                && !string.IsNullOrEmpty(Customer.Name)
                && !string.IsNullOrEmpty(Customer.Address);
        }
    }
}
