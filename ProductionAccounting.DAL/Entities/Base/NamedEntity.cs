﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
