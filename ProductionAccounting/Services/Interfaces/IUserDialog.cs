using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialog
    {
        bool Edit(EmployeeModel employee);

        bool ConfirmOperation(string info, string caption);
    }
}
