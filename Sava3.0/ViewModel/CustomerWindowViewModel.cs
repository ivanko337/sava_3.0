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

        public CustomerWindowViewModel()
        {
            Customer = new Customer();
        }

        private void CreateCustomer(object parameter)
        {
            using (var context = new DBContext())
            {
                DBService.AddNewEntity(parameter as Window, Customer, context, context.Customers);
            }
        }

        private bool CanCreateCustomer(object parameter)
        {
            return Customer != null
                && !string.IsNullOrEmpty(Customer.Name)
                && !string.IsNullOrEmpty(Customer.Address);
        }
    }
}
