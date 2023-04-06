using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;
using System;

namespace ProductionAccounting.Models
{
    public class CoefficientModel : Model
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public decimal _coefficientValue;
        public decimal CoefficientValue
        {
            get { return _coefficientValue; }
            set { _coefficientValue = value; OnPropertyChanged(nameof(CoefficientValue)); }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            var o = obj as CoefficientModel;
            return Name == o.Name && CoefficientValue == o.CoefficientValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.CoefficientValue);
        }

        public CoefficientModel()
        { }

        public CoefficientModel(OperationСoefficient orm)
        {
            Name = orm.Name;
            Id = orm.Id;
            CoefficientValue = orm.CoefficientValue;
        }

        public OperationСoefficient MapToOrm()
        {
            OperationСoefficient c = new OperationСoefficient();
            c.Id = Id;
            c.Name = Name;
            c.CoefficientValue = CoefficientValue;
            return c;
        }

        public override object Clone()
        {
            CoefficientModel model = new CoefficientModel();
            model.Name = Name;
            model.CoefficientValue = CoefficientValue;
            model.Id = Id;
            return model;
        }

        public override string ToString()
        {
            return $"{Name}, {string.Format("{0:F2}", CoefficientValue)}";
        }
    }
}
