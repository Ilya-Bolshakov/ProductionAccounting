using System;

namespace ProductionAccounting.Models.Base
{
    public abstract class Model : ICloneable
    {
        public int Id { get; set; }

        public abstract object Clone();
    }
}
