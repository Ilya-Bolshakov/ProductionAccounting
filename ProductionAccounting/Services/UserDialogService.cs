using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services
{
    public class UserDialogService : IUserDialog
    {
        public bool Edit(EmployeeModel employee)
        {
            return false;
        }
    }
}
