﻿using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IUserPrintDialog
    {
        public bool ShowPrintDialog(TabelModel model);
    }
}
