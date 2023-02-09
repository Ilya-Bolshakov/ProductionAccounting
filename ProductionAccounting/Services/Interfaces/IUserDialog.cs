using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserDialog<T> where T : Model
    {
        bool Edit(T model);

        bool ConfirmOperation(string info, string caption);
    }
}
